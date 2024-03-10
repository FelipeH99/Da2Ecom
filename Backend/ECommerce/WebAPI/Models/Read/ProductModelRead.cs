using Entities;

namespace WebAPI.Models.Read
{
    public class ProductModelRead : ModelRead<SearchResult, ProductModelRead>
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public string BrandName { get; set; }
        public string ProductId { get; set; }
        public string ImageURL { get; set; }
        public override ProductModelRead SetModel(SearchResult entity)
        {
            this.ProductName = entity.ProductName;
            this.Price = entity.Price;
            this.Category = entity.Category.ToString();
            this.BrandName = entity.BrandName;
            this.ProductId = entity.ProductId.ToString();
            this.ImageURL = entity.Image;
            return this;
        }
        public override bool Equals(Object obj) => (!(obj is ProductModelRead productModelRead)) ? false : productModelRead.ProductName.Equals(this.ProductName);
        public override int GetHashCode() => this.ProductName.GetHashCode();
    }
}
