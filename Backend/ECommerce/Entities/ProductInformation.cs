
namespace Entities
{
    public class ProductInformation
    {
        public string Text { get; set; }
        public Guid BrandId { get; set; }
        public string Category { get; set; }
        public double minPrice { get; set; }
        public double maxPrice { get; set; }
        public bool AvailableForPromotion { get; set; }
        public string productsIds { get; set; }
        public ProductInformation()
        {
        }

    }
}
