using Entities;

namespace WebAPI.Models.Read.Discounts
{
    public class ColorDiscountModelRead : ModelRead<ColorDiscount, ColorDiscountModelRead>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ColorName { get; set; }
        public double PercentageDiscount { get; set; }
        public string ProductToBeDiscounted { get; set; }
        public int MinProductsNeededForDiscount { get; set; }
        public bool isActive { get; set; }
        public override ColorDiscountModelRead SetModel(ColorDiscount entity)
        {
            this.Id = entity.Id.ToString();
            this.Name = entity.Name;
            this.ColorName = entity.Color.Name;
            this.PercentageDiscount = entity.PercentageDiscount;
            this.ProductToBeDiscounted = entity.ProductToBeDiscounted;
            this.MinProductsNeededForDiscount = entity.MinProductsNeededForDiscount;
            this.isActive = entity.IsActive;
            return this;
        }
        public override bool Equals(Object obj) => (!(obj is ColorDiscountModelRead discountModelRead)) ? false : discountModelRead.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();
    }
}
