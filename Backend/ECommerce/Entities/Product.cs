using Entities.Enums;

namespace Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductCategory ProductCategory {  get; set; }
        public Brand Brand { get; set; }
        public int Stock { get; set; }
        public bool AvailableForPromotion { get; set; }
        public List<Color> Colors { get; set; }
        public string ImageURL { get; set; }


        public Product()
        {

        }
        public override bool Equals(object obj)
        {
            bool equals = false;
            if (IsNotNull(obj) && IsSameTypeAsThis(obj))
            {
                Product oneProduct = (Product)obj;
                equals = this.Name.Equals(oneProduct.Name) && this.Price == oneProduct.Price && 
                    this.Description.Equals(oneProduct.Description) && this.Brand.Equals(oneProduct.Brand) &&
                    this.HasSameColors(oneProduct.Colors) && this.ProductCategory.Equals(oneProduct.ProductCategory);
            }
            return equals;
        }
        public bool HasSameColors(List<Color> colors) 
        {
            foreach (Color color in colors){
                
                if (!this.Colors.Contains(color))
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsSameTypeAsThis(object obj)
        {
            return obj.GetType().Equals(this.GetType());
        }

        public bool IsNotNull(object obj)
        {
            return obj != null;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
