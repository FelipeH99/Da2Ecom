using Entities;

namespace WebAPI.Models.Read
{
    public class ProductModelReadNoSearchResult : ModelRead<Product, ProductModelReadNoSearchResult>
    {

        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public string BrandName { get; set; }
        public override ProductModelReadNoSearchResult SetModel(Product entity)
        {
            this.Name = entity.Name;
            this.Price = entity.Price;
            this.Category = entity.ProductCategory.ToString();
            this.BrandName = entity.Brand.Name;
            return this;
        }
        public override bool Equals(Object obj) => (!(obj is ProductModelReadNoSearchResult productModelRead)) ? false : productModelRead.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();
    }
}
