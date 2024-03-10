using Entities;
using WebAPI.Models.Read;

namespace WebAPI.Models
{
    public class ProductModel : ModelRead<Product, ProductModel>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string BrandId { get; set; }
        public string Category { get; set; } 
        public List<string> Colors { get; set; }
        public string ImageURL { get; set; }
        public int Stock { get; set; }

        public ProductModel() { }

        public override ProductModel SetModel(Product entity)
        {
            this.Id = entity.Id.ToString();
            this.Name = entity.Name;
            this.Price = entity.Price;
            this.Category = entity.ProductCategory.ToString();
            this.BrandId = entity.Brand.Id.ToString();
            this.Description = entity.Description;
            this.Colors = CreateColorList(entity.Colors);
            this.ImageURL = entity.ImageURL;
            this.Stock = entity.Stock;
            return this;
        }
        private List<string> CreateColorList(List<Color> colors) 
        {
            List<string> colorsString = new List<string>();
            foreach(Color color in colors) 
            {
                colorsString.Add(color.ToString());
            }
            return colorsString;
        } 
        public override bool Equals(Object obj) => (!(obj is ProductModel productModel)) ? false : productModel.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();
    }
}

