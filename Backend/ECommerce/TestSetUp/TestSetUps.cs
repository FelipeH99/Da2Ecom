using Entities;
using Entities.Enums;
using WebAPI.Models.Read;
using WebAPI.Models.Write;
using WebAPI.Models.Write.Discounts;

namespace TestSetUp
{
    public class TestSetUps
    {
        public Brand InitOneBrandComplete()
        {
            Brand oneBrand = new Brand()
            {
                Name = "Nike"
            };

            return oneBrand;
        }
        public Permission InitOnePermissionComplete()
        {
            Permission onePermission = new Permission()
            {
                Name = "POST/Purchase"
            };

            return onePermission;
        }
        public Permission InitAnotherPermissionComplete()
        {
            Permission onePermission = new Permission()
            {
                Name = "GET/Purchase"
            };

            return onePermission;
        }
        public PaymentMethod InitOnePaymentMethod() 
        {
            PaymentMethod payment = new PaymentMethod()
            {
                Name = "Debito Itau"
            };
            return payment;
        }
        public PaymentMethod InitAnotherPaymentMethod()
        {
            PaymentMethod payment = new PaymentMethod()
            {
                Name = "Debito Santander"
            };
            return payment;
        }
        public Brand InitAnotherBrandComplete()
        {
            Brand oneBrand = new Brand()
            {
                Name = "Columbia"
            };

            return oneBrand;
        }
        public Color InitOneColorComplete()
        {
            Color color = new Color()
            {
                Name = "Red"
            };
            return color;
        }
        public Color InitSecondColorComplete()
        {
            Color color = new Color()
            {
                Name = "Verde"
            };
            return color;
        }
        public List<Color> InitFirstColorListComplete()
        {
            Color color = InitOneColorComplete();
            List<Color> colorList = new List<Color>();
            colorList.Add(color);
            return colorList;
        }
        public List<Color> InitSecondColorListComplete()
        {
            Color color = new Color()
            {
                Name = "Verde"
            };
            List<Color> colorList = new List<Color>();
            colorList.Add(color);
            return colorList;
        }
        public List<Color> InitThirdColorListComplete()
        {
            Color color = new Color()
            {
                Name = "Verde"
            };
            Color secondColor = new Color()
            {
                Name = "Azul"
            };
            List<Color> colorList = new List<Color>();
            colorList.Add(color);
            colorList.Add(secondColor);
            return colorList;
        }

        public Product InitOneProductComplete()
        {
            Product product = new Product()
            {
                Name = "Remera Hombre",
                Price = 19.99,
                Description = "Remera Hombre perfecta para el verano",
                Brand = InitOneBrandComplete(),
                Colors = InitFirstColorListComplete(),
                ProductCategory = ProductCategory.Camisetas,
                Stock = 12,
                AvailableForPromotion = true,
                ImageURL = "https://fastly.picsum.photos/id/642/200/200.jpg?hmac=MJkhEaTWaybCn0y7rKfh_irNHvVuqRHmxcpziWABTKw"
            };
            return product;
        }
        public Product InitSecondProductComplete()
        {
            Product product = new Product()
            {
                Name = "Campera abrigada Columbia",
                Price = 120.99,
                Description = "Campera abrigada perfecta para el invierno",
                Brand = InitAnotherBrandComplete(),
                Colors = InitSecondColorListComplete(),
                ProductCategory = ProductCategory.Abrigos,
                Stock = 1,
                AvailableForPromotion = true,
                ImageURL = "https://fastly.picsum.photos/id/642/200/200.jpg?hmac=MJkhEaTWaybCn0y7rKfh_irNHvVuqRHmxcpziWABTKw"
            };
            return product;
        }
        public Product InitThirdProductComplete()
        {
            Product product = new Product()
            {
                Name = "Campera abrigada Columbia",
                Price = 120.99,
                Description = "Campera abrigada perfecta para el invierno",
                Brand = InitAnotherBrandComplete(),
                Colors = InitThirdColorListComplete(),
                ProductCategory = ProductCategory.Abrigos,
                Stock = 23,
                AvailableForPromotion = true
            };
            return product;
        }
        public Product InitFourthProductComplete()
        {
            Product product = new Product()
            {
                Name = "Campera abrigada Columbia",
                Price = 120.99,
                Description = "Campera abrigada perfecta para el invierno",
                Brand = InitAnotherBrandComplete(),
                Colors = InitThirdColorListComplete(),
                ProductCategory = ProductCategory.Camisas,
                Stock = 11,
                AvailableForPromotion = true
            };
            return product;
        }
        public Product InitFifthProductComplete()
        {
            Product product = new Product()
            {
                Name = "Campera abrigada Columbia",
                Price = 89.99,
                Description = "Campera abrigada perfecta para el invierno",
                Brand = InitAnotherBrandComplete(),
                Colors = InitThirdColorListComplete(),
                ProductCategory = ProductCategory.Chandal,
                Stock = 2,
                AvailableForPromotion = true
            };
            return product;
        }
        public List<Product> InitOneProductList()
        {
            List<Product> products = new List<Product>();
            Product oneProduct = InitOneProductComplete();
            Product anotherProduct = InitSecondProductComplete();
            products.Add(oneProduct);
            products.Add(anotherProduct);
            return products;
        }
        public Role InitOneRoleComplete()
        {
            Role role = new Role()
            {
                Name = "Administrator",
                Permissions = InitOnePermissionListComplete()
            };
            return role;
        }
        public List<Role> InitOneRoleListComplete()
        {
            Role role = new Role()
            {
                Name = "Administrator",
                Permissions = InitOnePermissionListComplete()
            };
            List<Role> roles = new List<Role>();
            roles.Add(role);
            return roles;
        }
        public List<Role> InitSecondRoleListComplete()
        {
            Role role = new Role()
            {
                Name = "Usuario Comun",
                Permissions = InitOnePermissionListComplete()
            };
            List<Role> roles = new List<Role>();
            roles.Add(role);
            return roles;
        }
        public Role GiveMeRoleSecondRoleListComplete() 
        {
            Role role = new Role()
            {
                Name = "Usuario Comun",
                Permissions = InitOnePermissionListComplete()
            };
            return role;
        }
        public Role InitBuyerRoleComplete()
        {
            Role oneRole = new Role()
            {
                Name = "Comprador",
                Permissions = InitOnePermissionListComplete()
            };
            return oneRole;
        }
        public User InitAdminUserComplete()
        {
            User adminUser = new User()
            {
                Name = "Pedro el Admin",
                DeliveryAdress = "Su casa 1142 apto 201",
                Password = "admin1234!",
                Email = "pedro@gmail.com",
                IsDeleted = false,
                Roles = InitOneRoleListComplete(),
            };

            return adminUser;
        }
        public User InitOneUserComplete()
        {
            User user = new User()
            {
                Name = "Jose",
                DeliveryAdress = "Mercedes 1423",
                Password = "milanesa12!",
                Email = "jose123@gmail.com",
                IsDeleted = false,
                Roles = InitSecondRoleListComplete()
            };
            return user;
        }
        public User InitAnotherUserComplete()
        {
            User user = new User()
            {
                Name = "Felipe",
                DeliveryAdress = "Agraciada 1200",
                Password = "contrasena!@#",
                Email = "felipefelipe@gmail.com",
                Roles = InitOneRoleListComplete()
            };
            return user;
        }
        public Purchase InitOnePurchaseWithOneProductComplete()
        {
            Purchase onePurchase = new Purchase()
            {
                User = InitOneUserComplete(),
                PurchaseDate = DateTime.Today,
                Products = InitOneProductListWithOneProduct(),
                FinalPrice = 19.20,
                ProductsPrice = 100,
                DiscountApplied = "2x1",
                PaymentMethod = InitOnePaymentMethod()
            };
            return onePurchase;
        }
        public List<Product> InitOneProductListWithOneProduct()
        {
            Product oneProduct = InitOneProductComplete();
            List<Product> productList = new List<Product>();
            productList.Add(oneProduct);
            return productList;

        }
        public List<Product> InitProductListWithOneProductComplete() 
        {
            List<Product> products = new List<Product>();
            Product oneProduct = InitOneProductComplete();
            products.Add(oneProduct);
            return products;
        }
        public Purchase InitOnePurchaseComplete()
        {
            Purchase onePurchase = new Purchase()
            {
                User = InitOneUserComplete(),
                PurchaseDate = DateTime.Today,
                Products = InitProductListWithOneProductComplete(),
                FinalPrice = 19.20,
                DiscountApplied = "2x1",
                PaymentMethod = InitOnePaymentMethod(),
            };
            return onePurchase;
        }
        public Purchase InitAnotherPurchaseComplete()
        {
            Purchase onePurchase = new Purchase()
            {
                User = InitAnotherUserComplete(),
                PurchaseDate = DateTime.Today,
                Products = InitOneProductList(),
                FinalPrice = 19.20,
                DiscountApplied = "2x1",
                PaymentMethod = InitOnePaymentMethod()
            };
            return onePurchase;
        }
        public ColorDiscount InitOneColorDiscountComplete()
        {
            ColorDiscount oneColorDiscount = new ColorDiscount()
            {
                Name = "Total Look",
                Color = InitOneColorComplete(),
                PercentageDiscount = 0.50,
                ProductToBeDiscounted = "MaxValue",
                IsActive = true
            };
            return oneColorDiscount;
        }
        public ColorDiscount InitSecondColorDiscountComplete()
        {
            ColorDiscount oneColorDiscount = new ColorDiscount()
            {
                Name = "Total Look",
                Color = InitOneColorComplete(),
                PercentageDiscount = 0.50,
                ProductToBeDiscounted = "MaxValue",
                MinProductsNeededForDiscount = 2
            };
            return oneColorDiscount;
        }
        public ColorDiscount InitThirdColorDiscountComplete()
        {
            ColorDiscount oneColorDiscount = new ColorDiscount()
            {
                Name = "Minimalistic Look",
                Color = InitOneColorComplete(),
                PercentageDiscount = 0.15,
                ProductToBeDiscounted = "MaxValue",
                MinProductsNeededForDiscount = 1
            };
            return oneColorDiscount;
        }
        public ColorDiscount InitAnotherColorDiscountComplete()
        {
            ColorDiscount oneColorDiscount = new ColorDiscount()
            {
                Name = "Total Look",
                Color = InitSecondColorComplete(),
                PercentageDiscount = 0.20,
                ProductToBeDiscounted = "MaxValue",
                IsActive = true
            };
            return oneColorDiscount;
        }
        public ColorDiscount InitAnotherColorDiscountMinValueComplete()
        {
            ColorDiscount oneColorDiscount = new ColorDiscount()
            {
                Name = "Total Look",
                Color = InitSecondColorComplete(),
                PercentageDiscount = 0.20,
                ProductToBeDiscounted = "MinValue",
                IsActive = true
            };
            return oneColorDiscount;
        }
        public QuantityDiscount InitOneQuantityDiscountComplete()
        {
            QuantityDiscount quantityDiscount = new QuantityDiscount()
            {
                Name = "3x2",
                ProductCategory = ProductCategory.Abrigos,
                MinProductsNeededForDiscount = 3,
                NumberOfProductsToBeFree = 1,
                ProductToBeDiscounted = "MinValue",
                IsActive = true
            };
            return quantityDiscount;
        }
        public QuantityDiscount InitAnotherQuantityDiscountComplete()
        {
            QuantityDiscount quantityDiscount = new QuantityDiscount()
            {
                Name = "4x3",
                ProductCategory = ProductCategory.Cazadoras,
                MinProductsNeededForDiscount = 4,
                NumberOfProductsToBeFree = 1,
                ProductToBeDiscounted = "MinValue",
                IsActive = true
            };
            return quantityDiscount;
        }
        public QuantityDiscount InitThirdQuantityDiscountComplete()
        {
            QuantityDiscount quantityDiscount = new QuantityDiscount()
            {
                Name = "4x3",
                ProductCategory = ProductCategory.Abrigos,
                MinProductsNeededForDiscount = 4,
                NumberOfProductsToBeFree = 1,
                ProductToBeDiscounted = "MaxValue",
                IsActive = true
            };
            return quantityDiscount;
        }
        public BrandDiscount InitOneBrandDiscountComplete()
        {
            BrandDiscount brandDiscount = new BrandDiscount()
            {
                Name = "3x1 Fidelidad",
                Brand = InitOneBrandComplete(),
                MinProductsForPromotion = 3,
                NumberOfProductsForFree = 2,
                ProductToBeDiscounted = "MinValue",
                IsActive = true
            };
            return brandDiscount;
        }
        public BrandDiscount InitAnotherBrandDiscountComplete()
        {
            BrandDiscount brandDiscount = new BrandDiscount()
            {
                Name = "2x1 Fidelidad",
                Brand = InitOneBrandComplete(),
                MinProductsForPromotion = 2,
                NumberOfProductsForFree = 1,
                ProductToBeDiscounted = "MaxValue",
                IsActive = true
            };
            return brandDiscount;
        }
        public PercentageDiscount InitOnePercentageDiscountComplete()
        {
            PercentageDiscount percentageDiscount = new PercentageDiscount()
            {
                Name = "20%",
                PercentageDiscounted = 0.20,
                ProductToBeDiscounted = "MaxValue",
                MinProductsNeededForDiscount = 2,
                IsActive = true
            };
            return percentageDiscount;

        }
        public PercentageDiscount InitAnotherPercentageDiscountComplete()
        {
            PercentageDiscount percentageDiscount = new PercentageDiscount()
            {
                Name = "40%",
                PercentageDiscounted = 0.40,
                ProductToBeDiscounted = "MaxValue",
                IsActive = true
            };
            return percentageDiscount;

        }
        public PercentageDiscount InitAnotherPercentageNotMaxValueDiscountComplete()
        {
            PercentageDiscount percentageDiscount = new PercentageDiscount()
            {
                Name = "40%",
                PercentageDiscounted = 0.40,
                ProductToBeDiscounted = "NotMaxValue",
                IsActive = true
            };
            return percentageDiscount;

        }
        public List<Permission> InitOnePermissionListComplete()
        {
            Permission onePermission = InitOnePermissionComplete();
            Permission anotherPermission = InitAnotherPermissionComplete();
            List<Permission> permissions = new List<Permission>();

            permissions.Add(onePermission);
            permissions.Add(anotherPermission);

            return permissions;
        }

        public List<Product> InitProductListWith3ItemsSameCategoryComplete()
        {
            Product oneProduct = InitSecondProductComplete();
            Product anotherProduct = InitSecondProductComplete();
            Product thirdProduct = InitSecondProductComplete();
            Product fourthProduct = InitOneProductComplete();
            Product fifthProduct = InitOneProductComplete();

            List<Product> products = new List<Product>();
            products.Add(oneProduct);
            products.Add(anotherProduct);
            products.Add(thirdProduct);
            products.Add(fourthProduct);
            products.Add(fifthProduct);

            return products;
        }
        public List<Product> InitAnotherProductListWith3ItemsSameCategoryComplete()
        {
            Product oneProduct = InitSecondProductComplete();
            Product anotherProduct = InitSecondProductComplete();
            Product thirdProduct = InitSecondProductComplete();
            Product fourthProduct = InitThirdProductComplete();
            Product fifthProduct = InitThirdProductComplete();

            List<Product> products = new List<Product>();
            products.Add(oneProduct);
            products.Add(anotherProduct);
            products.Add(thirdProduct);
            products.Add(fourthProduct);

            return products;
        }
        public List<Product> InitProductListWith3ItemsSameColorComplete()
        {
            Product oneProduct = InitFifthProductComplete();
            Product anotherProduct = InitFourthProductComplete();
            Product thirdProduct = InitThirdProductComplete();
            Product fourthProduct = InitSecondProductComplete();
            Product fifthProduct = InitOneProductComplete();

            List<Product> products = new List<Product>();
            products.Add(oneProduct);
            products.Add(anotherProduct);
            products.Add(thirdProduct);
            products.Add(fourthProduct);
            products.Add(fifthProduct);

            return products;
        }
        public List<Product> InitProductListWith3ItemsSameBrandComplete()
        {
            Product oneProduct = InitSecondProductComplete();
            Product anotherProduct = InitOneProductComplete();
            Product thirdProduct = InitOneProductComplete();
            Product fourthProduct = InitSecondProductComplete();
            Product fifthProduct = InitOneProductComplete();

            List<Product> products = new List<Product>();
            products.Add(oneProduct);
            products.Add(anotherProduct);
            products.Add(thirdProduct);
            products.Add(fourthProduct);
            products.Add(fifthProduct);

            return products;
        }
        public List<Product> InitProductListWith2ItemsComplete()
        {
            Product oneProduct = InitSecondProductComplete();
            Product anotherProduct = InitOneProductComplete();


            List<Product> products = new List<Product>();
            products.Add(oneProduct);
            products.Add(anotherProduct);

            return products;

        }
        public Purchase InitPurchaseForQuantityDiscountComplete()
        {
            Purchase onePurchase = new Purchase()
            {
                User = InitOneUserComplete(),
                PurchaseDate = DateTime.Today,
                Products = InitProductListWith3ItemsSameCategoryComplete(),
                FinalPrice = 0
            };
            return onePurchase;
        }
        public Purchase InitPurchaseForQuantitySameCategoryDiscountComplete()
        {
            Purchase onePurchase = new Purchase()
            {
                User = InitOneUserComplete(),
                PurchaseDate = DateTime.Today,
                Products = InitAnotherProductListWith3ItemsSameCategoryComplete(),
                FinalPrice = 0
            };
            return onePurchase;
        }
        public Purchase InitPurchaseForColorDiscountComplete()
        {
            Purchase onePurchase = new Purchase()
            {
                User = InitOneUserComplete(),
                PurchaseDate = DateTime.Today,
                Products = InitProductListWith3ItemsSameColorComplete(),
                FinalPrice = 0
            };
            return onePurchase;
        }
        public Purchase InitPurchaseForPercentageDiscountComplete()
        {
            Purchase onePurchase = new Purchase()
            {
                User = InitOneUserComplete(),
                PurchaseDate = DateTime.Today,
                Products = InitProductListWith2ItemsComplete(),
                FinalPrice = 0
            };
            return onePurchase;
        }
        public Purchase InitPurchaseForBrandDiscountComplete()
        {
            Purchase onePurchase = new Purchase()
            {
                User = InitOneUserComplete(),
                PurchaseDate = DateTime.Today,
                Products = InitProductListWith3ItemsSameBrandComplete(),
                FinalPrice = 0
            };
            return onePurchase;
        }
        public AdminToken InitOneAdminTokenComplete(User oneUser)
        {
            AdminToken adminToken = new AdminToken()
            {
                User = oneUser,
            };
            return adminToken;
        }
        public SearchResult InitOneSearchResultComplete() 
        {
            SearchResult searchResult = new SearchResult()
            {
                ProductName = "Remera Hombre",
                BrandName = "Nike",
                Category = ProductCategory.Camisetas,
                Price = 19.99
            };
            return searchResult;

        }
        public ProductInformation InitOneProductInformationComplete() 
        {
            ProductInformation productInformation = new ProductInformation()
            {
                Text = "Camisa",
                BrandId = Guid.NewGuid(),
                Category = "Abrigos"

            };
            return productInformation;
        }
        public ProductModelWrite InitOneProductModelWriteComplete(Product oneProduct) 
        {
            ProductModelWrite prodModWrite = new ProductModelWrite()
            {
                Name = oneProduct.Name,
                Price = oneProduct.Price,
                Description = oneProduct.Description,
                BrandId = oneProduct.Brand.Id,
                Category = oneProduct.ProductCategory.ToString(),
                ColorsIds = InitColorsGuidsList(oneProduct),
            };
            return prodModWrite;
        }
        public List<Guid> InitColorsGuidsList(Product oneProduct) 
        {
            List<Guid> guids = new List<Guid>();
            foreach (var color in oneProduct.Colors) 
            {
                guids.Add(color.Id);
            }
            return guids;
        }
        public PurchaseModelWrite InitOnePurchaseModelWriteComplete(Purchase purchase) 
        {
            PurchaseModelWrite purchaseModelWrite = new PurchaseModelWrite()
            {
                UserId = purchase.User.Id.ToString(),
                ProductIds = InitOneListOfGuidComplete(),
                PaymentMethodId = purchase.PaymentMethod.Id.ToString(),
            };
            return purchaseModelWrite;
        }
        public List<string> InitOneListOfGuidComplete() 
        {
            List<string> returnList = new List<string>();
            returnList.Add(new Guid().ToString());
            return returnList;
        }
        public PurchaseModelRead InitOnePurchaseModelRead(Purchase onePurchase) 
        {
            PurchaseModelRead onePurchaseModelRead = new PurchaseModelRead()
            {
                UserName = onePurchase.User.Name,
                PurchaseDate = onePurchase.PurchaseDate.ToString(),
                Products = ConvertToStringList(onePurchase.Products),
                ProductsPrice = onePurchase.ProductsPrice,
                FinalPrice = onePurchase.FinalPrice,
                Discount = onePurchase.DiscountApplied

            };
            return onePurchaseModelRead;
        }
        public PercentageDiscountModelWrite InitOnePercentageDiscountModelUpdateWrite(PercentageDiscount discount)
        {
            PercentageDiscountModelWrite percentageDiscountModelUpdateWrite = new PercentageDiscountModelWrite()
            {
                Name = discount.Name,
                PercentageDiscounted = discount.PercentageDiscounted,
                ProductToBeDiscounted = discount.ProductToBeDiscounted,
                MinProductNeededForDiscount = discount.MinProductsNeededForDiscount
            };
            return percentageDiscountModelUpdateWrite;
        }
        public PercentageDiscountUpdateModelWrite InitOnePercentageDiscountUpdateModelUpdateWrite(PercentageDiscount discount)
        {
            PercentageDiscountUpdateModelWrite percentageDiscountModelUpdateWrite = new PercentageDiscountUpdateModelWrite()
            {
                Name = discount.Name,
                PercentageDiscounted = discount.PercentageDiscounted,
                ProductToBeDiscounted = discount.ProductToBeDiscounted,
                MinProductNeededForDiscount = discount.MinProductsNeededForDiscount,
                IsActive = "true"
            };
            return percentageDiscountModelUpdateWrite;
        }
        public PercentageDiscountModelWrite InitOnePercentageModelWriteComplete(PercentageDiscount discount) 
        {
            PercentageDiscountModelWrite model = new PercentageDiscountModelWrite()
            {
                Name = discount.Name,
                PercentageDiscounted = discount.PercentageDiscounted,
                MinProductNeededForDiscount = discount.MinProductsNeededForDiscount,
                ProductToBeDiscounted = discount.ProductToBeDiscounted
            };
            return model;
        }
        public QuantityDiscountModelWrite InitOneQuantityDiscountModelWrite(QuantityDiscount discount) 
        {
            QuantityDiscountModelWrite model = new QuantityDiscountModelWrite()
            {
                Name = discount.Name,
                Category = discount.ProductCategory.ToString(),
                NumberOfProductToBeFree = discount.NumberOfProductsToBeFree,
                MinProductNeededForDiscount = discount.MinProductsNeededForDiscount,
                ProductToBeDiscounted = discount.ProductToBeDiscounted,
            };

            return model;
        }
        public QuantityDiscountUpdateModelWrite InitOneQuantityDiscountUpdateModelWrite(QuantityDiscount discount)
        {
            QuantityDiscountUpdateModelWrite model = new QuantityDiscountUpdateModelWrite()
            {
                Name = discount.Name,
                Category = discount.ProductCategory.ToString(),
                NumberOfProductToBeFree = discount.NumberOfProductsToBeFree,
                MinProductNeededForDiscount = discount.MinProductsNeededForDiscount,
                ProductToBeDiscounted = discount.ProductToBeDiscounted,
                IsActive = "false"
            };

            return model;
        }
        public ColorDiscountModelWrite InitOneColorDiscountModelWrite(ColorDiscount discount) 
        {
            ColorDiscountModelWrite model = new ColorDiscountModelWrite()
            {
                Name = discount.Name,
                ColorId = Guid.NewGuid(),
                PercentageDiscount = 0.50,
                ProductToBeDiscounted = "MaxValue",
                MinProductsNeededForDiscount = 2
            };

            return model;
        }
        public ColorDiscountUpdateModelWrite InitOneColorDiscountUpdateModelWrite(ColorDiscount discount)
        {
            ColorDiscountUpdateModelWrite model = new ColorDiscountUpdateModelWrite()
            {
                Name = discount.Name,
                ColorId = Guid.NewGuid(),
                PercentageDiscount = 0.50,
                ProductToBeDiscounted = "MaxValue",
                MinProductsNeededForDiscount = 2,
                IsActive = "false"
            };

            return model;
        }
        public BrandDiscountModelWrite InitOneBrandDiscountModelWrite(BrandDiscount discount) 
        {
            BrandDiscountModelWrite model = new BrandDiscountModelWrite()
            {
                Name = discount.Name, 
                BrandId = discount.Brand.Id,
                NumberOfProductsForFree = discount.NumberOfProductsForFree,
                ProductToBeDiscounted = discount.ProductToBeDiscounted,
                MinProductsNeededForDiscount = discount.MinProductsForPromotion
    };
            return model;
        }
        public BrandDiscountUpdateModelWrite InitOneBrandDiscountUpdateModelWrite(BrandDiscount discount)
        {
            BrandDiscountUpdateModelWrite model = new BrandDiscountUpdateModelWrite()
            {
                Name = discount.Name,
                BrandId = discount.Brand.Id,
                NumberOfProductsForFree = discount.NumberOfProductsForFree,
                ProductToBeDiscounted = discount.ProductToBeDiscounted,
                MinProductsNeededForDiscount = discount.MinProductsForPromotion,
                IsActive = "true"
            };
            return model;
        }
        private List<string> ConvertToStringList(List<Product> products)
        {
            List<string> stringList = new List<string>();
            foreach (Product p in products) 
            {
                stringList.Add(p.Name);
            }
            return stringList;
        }

    }
}