using Entities;

namespace WebAPI.Models.Write.Discounts
{
    public class ColorDiscountUpdateModelWrite : ModelWrite<ColorDiscount, ColorDiscountUpdateModelWrite>
    {
        public string Name { get; set; }
        public Guid ColorId { get; set; }
        public double PercentageDiscount { get; set; }
        public string ProductToBeDiscounted { get; set; }
        public int MinProductsNeededForDiscount { get; set; }
        public string IsActive { get; set; }
        public override ColorDiscount ToEntity() => new ColorDiscount()
        {
            Name = Name,
            PercentageDiscount = PercentageDiscount,
            ProductToBeDiscounted = ProductToBeDiscounted,
            MinProductsNeededForDiscount = MinProductsNeededForDiscount,
            Color = new Color() { Id = ColorId },
            IsActive = bool.Parse(this.IsActive),
        };
        public override bool Equals(Object obj) => (!(obj is ColorDiscountModelWrite discountModelWrite)) ? false : discountModelWrite.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();
    }

}

