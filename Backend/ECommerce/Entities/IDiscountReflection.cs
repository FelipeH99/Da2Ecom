
namespace Entities
{
    public interface IDiscountReflection
    {
        public string Name { get; set; }
        public double CalculateDiscount(List<ProductDto> products);
    }
    public class ProductDto
    {
        public double Price { get; set; }
        public string ProductCategory { get; set; }
        public string BrandName { get; set; }
        public bool IsAvailableForPromotion { get; set; }
        public List<string> ColorNames { get; set; }
        public ProductDto() { }
    }
}
