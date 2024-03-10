using BusinessLogic.Interface;
using DataAccess.Interface;
using Entities;
using Exceptions;
using Microsoft.IdentityModel.Tokens;
using Reflection;

namespace BusinessLogic
{
    public class PurchaseLogic : IPurchaseLogic
    {
        private IPurchaseRepository PurchaseRepository;
        private IUserRepository UserRepository;

        public PurchaseLogic(IPurchaseRepository purchaseRepository,
    IUserRepository userRepository)
        {
            this.PurchaseRepository = purchaseRepository;
            this.UserRepository = userRepository;
        }

        public ICollection<Purchase> Get()
        {
            return this.PurchaseRepository.Get();
        }
        public Purchase Get(Guid id) 
        {
            return this.PurchaseRepository.Get(id);
        }
        public ICollection<Purchase> GetByUserId(Guid id) 
        {
            return this.PurchaseRepository.GetByUser(id).ToList();
        }
        public Purchase Create(Purchase purchase, IProductLogic productLogic, IPaymentMethodLogic paymentMethodService,
            IReflectionImplementation reflection, IDiscountLogic discountLogic)
        {
            var productsWithoutStock = SetProducts(purchase, productLogic);

            purchase.PurchaseDate = DateTime.Now;

            ValidatePurchase(purchase);
            ValidateRepeatedPurchase(purchase);

            SetPaymentMethod(purchase, paymentMethodService);

            SetDiscountApplied(purchase, reflection,discountLogic);
            SetExtraDiscountPaganza(purchase);

            ValidatePrice(purchase.FinalPrice);
            ValidatePrice(purchase.ProductsPrice);

            this.PurchaseRepository.Add(purchase);
            this.PurchaseRepository.Save();

            NotifyUser(productsWithoutStock);

            return purchase;
        }
        public Purchase Update(Guid id, Purchase purchase)
        {
            Purchase purchaseToChange = Get(id);
            PurchaseRepository.Update(purchaseToChange, purchase);
            PurchaseRepository.Save();
            return purchase;
        }
        public void Remove(Purchase purchase)
        {
            PurchaseRepository.Remove(purchase);
            PurchaseRepository.Save();
        }
        private void SetUser(Purchase onePurchase) 
        {
            User oneUser = this.UserRepository.Get(onePurchase.User.Id);
            if (oneUser != null)
            {
                onePurchase.User = oneUser;
            }
            else 
            {
                throw new IncorrectRequestException("El usuario no se encuentra en la base de datos");
            }
        }
        private List<string> SetProducts(Purchase onePurchase,IProductLogic productLogic) 
        {
            List<string> productNames = new List<string>();
            if (onePurchase == null)
            {
                throw new InvalidPurchaseException("Error al procesar la compra.");
            }
            else 
            {
                List<Product> finalProducts = new List<Product>();
                foreach (Product product in onePurchase.Products)
                {
                    var productDataBase = productLogic.Get(product.Id);
                    if (productDataBase == null)
                    {
                        throw new IncorrectRequestException("El producto ingresado no existe en la base de datos.");
                    }
                    else
                    {
                        if (productDataBase.Stock <= 0)
                        {
                            productNames.Add(productDataBase.Name);
                        }
                        else 
                        {
                            finalProducts.Add(productDataBase);
                            UpdateProductStock(productDataBase.Id, productLogic);
                        }
                    }
                }
                ValidateFinalProductList(finalProducts);
                onePurchase.Products = finalProducts;
                return productNames;
            }
            
        }
        private void ValidateFinalProductList(List<Product> products) 
        {
            if (products.IsNullOrEmpty()) 
            {
                throw new InvalidPurchaseException("No hay productos en la compra dado que los elegidos" +
                    " previamente no se encontraban en stock.");
            }
        }
        private void UpdateProductStock(Guid productId,IProductLogic productService) 
        {
            Product newProduct = productService.Get(productId);
            newProduct.Stock = newProduct.Stock - 1;
            productService.Update(productId, newProduct);
        }
        private void NotifyUser(List<string> products) 
        {
            if (!products.IsNullOrEmpty()) 
            {
                NoStockForProductsException ex = new NoStockForProductsException("Compra realizada! " +
                    " No hay stock para los siguientes productos y por ello no fueron incluidos en la compra" +
                    ": "+ products);
                throw ex;
            }
        }
        private void SetDiscountApplied(Purchase purchase,IReflectionImplementation reflection,IDiscountLogic discountLogic) 
        {
            var discountApplied = discountLogic.CalculateOptimumDiscount(purchase.Products, reflection);
            purchase.ProductsPrice = purchase.Products.AsQueryable().Sum(p => p.Price);
            purchase.FinalPrice = purchase.ProductsPrice - discountApplied.amountDiscounted;

            if (string.IsNullOrWhiteSpace(discountApplied.name) || discountApplied.amountDiscounted == 0)
            {
                purchase.DiscountApplied = "No se aplico ningun descuento";
            }
            else 
            {
                purchase.DiscountApplied = discountApplied.name;
            }
            
        }
        private void SetExtraDiscountPaganza(Purchase purchase) 
        {
            if (purchase.PaymentMethod.Name.Equals("Paganza")) 
            {
                purchase.FinalPrice = purchase.FinalPrice - purchase.FinalPrice*0.10;
            }
        }
        private void ValidatePurchase(Purchase purchase)
        {
            ValidateNullPurchase(purchase);
            ValidateUser(purchase.User);
            SetUser(purchase);
            ValidateProducts(purchase.Products);
        }
        private void ValidateUser(User user)
        {
            if (user == null)
            {
                throw new InvalidUserInPurchaseException("El usuario no puede ser nulo.");
            }
            else if (!this.UserRepository.Exists(user)) 
            {
                throw new InvalidUserInPurchaseException("El usuario no se encuentra registrado en el sistema");
            }
        }
        private void ValidateProducts(List<Product> products)
        {
            if (products.IsNullOrEmpty() || products.Count() < 1)
            {
                throw new IncorrectProductsForPurchaseException("La compra debe tener al menos un producto asociado.");
            }
        }
        private void ValidateRepeatedPurchase(Purchase purchase)
        {
            if (this.PurchaseRepository.Exists(purchase))
            {
                throw new RepeatedObjectException("La compra ya se enceuntra registrada.");
            }
        }
        private void ValidateNullPurchase(Purchase purchase)
        {
            if (purchase == null)
            {
                throw new InvalidPurchaseException("La compra es nula.");
            }
        }
        private void ValidatePrice(double price)
        {
            if (price <= 0)
            {
                throw new IncorrectRequestException("El precio no puede ser un valor menor ni igual a cero.");
            }
        }
        private void SetPaymentMethod(Purchase purchase, IPaymentMethodLogic paymentMethodService) 
        {
            if (purchase == null)
            {
                throw new InvalidPurchaseException("Error al procesar la compra.");
            }
            else 
            {
                var paymentMethodDataBase = paymentMethodService.Get(purchase.PaymentMethod.Id);
                if (paymentMethodDataBase == null)
                {
                    throw new IncorrectRequestException("El metodo de pago ingresado no existe en la base de datos" +
                        ", contacte a un administrador.");
                }
                else 
                {
                    purchase.PaymentMethod = paymentMethodDataBase;
                }
            }
        }
    }
}

