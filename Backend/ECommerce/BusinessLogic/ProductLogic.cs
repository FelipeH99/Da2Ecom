using BusinessLogic.Interface;
using DataAccess.Interface;
using Entities;
using Entities.Enums;
using Exceptions;
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLogic
{
    public class ProductLogic : IProductLogic
    {
        private IProductRepository ProductRepository;
        private IBrandRepository BrandRepository;
        private IColorRepository ColorRepository;
        public ProductLogic(IProductRepository productRepository, IBrandRepository brandRepository, 
            IColorRepository colorRepository)
        {
            this.ProductRepository = productRepository;
            this.BrandRepository = brandRepository;
            this.ColorRepository = colorRepository;
        }
        public ICollection<Product> Get()
        {
            return ProductRepository.Get();
        }
        public Product Get(Guid id)
        {
            return ProductRepository.Get(id);
        }
        public Product Create(Product oneProduct)
        {
            ValidateNullProduct(oneProduct);
            ValidateProduct(oneProduct);
            SetColorAndBrand(oneProduct);
            ValidateRepeatedProduct(oneProduct);
            ValidateImage(oneProduct);
            this.ProductRepository.Add(oneProduct);
            this.ProductRepository.Save();
            return oneProduct;
        }
        public Product Update(Guid id, Product oneProduct)
        {
            Product productToChange = Get(id);
            ValidateNullProduct(productToChange);
            SetColorAndBrand(oneProduct);
            ProductRepository.Update(productToChange, oneProduct);
            ProductRepository.Save();
            return oneProduct;
        }
        public void Remove(Product oneProduct)
        {
            ProductRepository.Remove(oneProduct);
            ProductRepository.Save();
        }
        private void ValidateProduct(Product oneProduct)
        {
            EmptyOrWhiteSpaceName(oneProduct.Name);
            EmptyOrWhiteSpaceDescription(oneProduct.Description);
            ValidatePrice(oneProduct.Price);
            ValidateBrand(oneProduct.Brand);
            ValidateColors(oneProduct.Colors);

        }
        private void ValidateImage(Product oneProduct) 
        {
            if (oneProduct.ImageURL.IsNullOrEmpty()) 
            {
                oneProduct.ImageURL = "https://dummyimage.com/450x300/dee2e6/6c757d.jpg";
            }
        }
        private void SetColorAndBrand(Product oneProduct) 
        {
            SetColor(oneProduct);
            SetBrand(oneProduct);
        }
        private void SetBrand(Product oneProduct) 
        {
            Brand oneBrand = this.BrandRepository.Get(oneProduct.Brand.Id);
            if (oneBrand == null)
            {
                throw new IncorrectBrandForProductException("La marca ingresada para el producto no se encuentra en el sistema.");
            }
            else 
            {
                oneProduct.Brand = oneBrand;
            }

        }
        private void SetColor(Product oneProduct) 
        {
            List<Color> colorsForProduct = new List<Color>();
            foreach (Color color in oneProduct.Colors) 
            {
                var colorInDataBase = this.ColorRepository.Get(color.Id);
                if (colorInDataBase == null)
                {
                    throw new IncorrectColorsForProductException("El color ingresado no se encuentra en el sistema.");
                }
                else 
                {
                    colorsForProduct.Add(colorInDataBase);
                }
            }
            oneProduct.Colors = colorsForProduct;
        }
        private void ValidateBrand(Brand oneBrand)
        {
            if (oneBrand == null)
            {
                throw new IncorrectBrandForProductException("La Marca del producto no puede ser vacia.");
            } else if (!this.BrandRepository.Exists(oneBrand))
            {
                throw new IncorrectBrandForProductException("La Marca del producto no se encuentra registrada en el sistema.");
            }
        }
        private void ValidateColors(List<Color> colorList)
        {
            if (colorList.IsNullOrEmpty())
            {
                throw new IncorrectColorsForProductException("El producto debe tener algun color asociado.");
            }
        }
        private void ValidateRepeatedProduct(Product oneProduct)
        {
            if (ProductRepository.Exists(oneProduct))
            {
                throw new RepeatedObjectException("El producto ya se enceuntra registrado.");
            }
        }
        private void ValidateNullProduct(Product oneProduct)
        {
            if (oneProduct == null)
            {
                throw new InvalidProductException("El Producto no se encuentra en el sistema.");
            }
        }
        private void EmptyOrWhiteSpaceName(String name)
        {
            if (VerifyEmptyOrWhiteSpaceString(name))
            {
                throw new IncorrectNameException("Nombre del producto no puede ser vacīo.");
            }
        }

        private void ValidatePrice(double price)
        {
            if (price <= 0)
            {
                throw new IncorrectRequestException("El precio no puede ser un valor menor ni igual a cero.");
            }
        }
        private void EmptyOrWhiteSpaceDescription(String description)
        {
            if (VerifyEmptyOrWhiteSpaceString(description))
            {
                throw new ArgumentException("La descripción no puede ser vacīa.");
            }
        }
        private bool VerifyEmptyOrWhiteSpaceString(String str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        public SearchResult ConvertProductToSearchResult(Product oneProduct)
        {

            SearchResult result = new SearchResult(oneProduct);
            return result;
        }
        public IEnumerable<Product> GetByBrand(Guid brandId)
        {
            return ProductRepository.Get().Where(p => p.Brand.Id.Equals(brandId)).ToList();
        }
        public IEnumerable<SearchResult> CreateSearchResult(ProductInformation productInformation)
        {
            var filteredProducts = (productInformation.Category == null) ? FilterWithoutCategory(productInformation) 
                : FilterWithCategory(productInformation) ;

            return ConvertProductsToSearchResult(filteredProducts.ToList());
        }
        private IEnumerable<SearchResult> ConvertProductsToSearchResult(List<Product> products) 
        {
            ICollection<SearchResult> searchResults = new List<SearchResult>();

            foreach (Product product in products)
            {
                SearchResult item = ConvertProductToSearchResult(product);
                searchResults.Add(item);
            }
            return searchResults;
        }
        private IEnumerable<Product> FilterWithoutCategory(ProductInformation productInformation) 
        {
            ICollection<Product> filteredProducts = new List<Product>();

            var products = this.ProductRepository.Get();

            var text = (productInformation.Text == null) ? "" : productInformation.Text;
            var brandId = productInformation.BrandId;
            var minPrice = productInformation.minPrice;
            var maxPrice = productInformation.maxPrice;
            var isAvailableForPromotion = productInformation.AvailableForPromotion;

            filteredProducts = products.Where(p =>
            (p.Price >= minPrice && p.Price <= maxPrice) ||
            p.Brand.Id.Equals(brandId) && p.AvailableForPromotion.Equals(isAvailableForPromotion) &&
            (p.Name.Contains(text) )).ToList();

            return filteredProducts;

        }
        private IEnumerable<Product> FilterWithCategory(ProductInformation productInformation) 
        {
            ICollection<Product> filteredProducts = new List<Product>();

            var products = this.ProductRepository.Get();

            var text = (productInformation.Text == null) ? "" : productInformation.Text;
            var brandId = productInformation.BrandId;
            var minPrice = productInformation.minPrice;
            var maxPrice = productInformation.maxPrice;
            var isAvailableForPromotion = productInformation.AvailableForPromotion;
            var category = productInformation.Category;

            filteredProducts = products.Where(p =>
            (p.Price >= minPrice && p.Price <= maxPrice) &&
            p.Brand.Id.Equals(brandId) && p.AvailableForPromotion.Equals(isAvailableForPromotion) &&
            (p.Name.Contains(text.ToLower())) && p.ProductCategory.ToString().Equals(category)).ToList();

            return filteredProducts;
        }
        public IEnumerable<Product> GetProducts(string ids) 
        {
            var splitedIds = ids.Split('*');
            var products = new List<Product>();
            foreach (string id in splitedIds) 
            {
                var productId = Guid.Parse(id);
                var product = this.ProductRepository.Get(productId);
                products.Add(product);
            }
            return products;
        }
    }
}
