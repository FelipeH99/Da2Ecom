using Entities;

namespace WebAPI.Models.Read
{
    public class ProductModelCreatedRead : ModelRead<Product, ProductModelCreatedRead>
    {
        public double Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductCategory { get; set; }
        public string BrandName { get; set; }
        public List<string> Colors { get; set; }

        public override ProductModelCreatedRead SetModel(Product entity)
        {
            this.Name = entity.Name;
            this.Price = entity.Price;
            this.Description = entity.Description;
            this.ProductCategory = entity.ProductCategory.ToString();
            this.BrandName = entity.Brand.Name;
            this.Colors = CreateColorList(entity.Colors);
            return this;
        }
        private List<string> CreateColorList(List<Color> colors)
        {
            List<string> colorsString = new List<string>();
            foreach (Color color in colors)
            {
                colorsString.Add(color.ToString());
            }
            return colorsString;
        }
        public override bool Equals(Object obj) => (!(obj is ProductModelCreatedRead productModelCreateRead)) ? false : productModelCreateRead.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();
    }
}
