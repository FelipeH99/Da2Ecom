using Entities;
using System.Reflection;

namespace Reflection
{
    public class ReflectionImplementation : IReflectionImplementation
    {

        public List<(string name, double amountDiscounted)> LoadDll(List<Product> products)
        {
            List<ProductDto> productsDtos = ConvertToProductDto(products);
            var discounts = GetImporterImplementations();
            return discounts.Select(instance =>
            {
                string name = instance.Name;
                double discountCalculated = instance.CalculateDiscount(productsDtos);

                return (name, discountCalculated);
            }).ToList();
        }
        private List<IDiscountReflection> GetImporterImplementations()
        {
            List<IDiscountReflection> availableImporters = new List<IDiscountReflection>();
            string importersPath = "./Importers";
            string[] filePaths = Directory.GetFiles(importersPath);

            foreach (string filePath in filePaths)
            {
                if (filePath.EndsWith(".dll"))
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    Assembly assembly = Assembly.LoadFile(fileInfo.FullName);

                    foreach (Type type in assembly.GetTypes())
                    {
                        if (typeof(IDiscountReflection).IsAssignableFrom(type) && !type.IsInterface)
                        {
                            IDiscountReflection importer = (IDiscountReflection)Activator.CreateInstance(type);
                            if (importer != null)
                                availableImporters.Add(importer);
                        }
                    }
                }
            }
            return availableImporters;
        }
        private List<ProductDto> ConvertToProductDto(List<Product> products)
        {
            List<ProductDto> returnList = products.Select(p => new ProductDto
            {
                Price = p.Price,
                ProductCategory = p.ProductCategory.ToString(),
                BrandName = p.Brand.Name,
                IsAvailableForPromotion = p.AvailableForPromotion,
                ColorNames = p.Colors.Select(c => c.Name).ToList(),
            }).ToList();

            return returnList;

        }
    }
}
