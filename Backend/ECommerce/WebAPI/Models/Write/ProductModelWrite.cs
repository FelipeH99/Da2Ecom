using Entities;
using Entities.Enums;

namespace WebAPI.Models.Write
{
    public class ProductModelWrite : ModelWrite<Product,ProductModelWrite>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Guid BrandId { get; set; }
        public string Category { get; set; }
        public List<Guid> ColorsIds { get; set; }
        public string ImageURL { get; set; }
        public int Stock { get; set; }

        public override Product ToEntity() => new Product()
        {
            Name = this.Name,
            Price = this.Price,
            Description = this.Description,
            Brand = new Brand()
            {
                Id = this.BrandId
            },
            ProductCategory = Enum.Parse<ProductCategory>(this.Category),
            Colors = this.ColorsIds.Select(c => new Color()
            {
                Id = c
            }).ToList(),
            ImageURL = this.ImageURL,
            Stock = this.Stock
        };
        public override bool Equals(Object obj) => (!(obj is ProductModelWrite productModelWrite)) ? false : productModelWrite.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();

    }
}
