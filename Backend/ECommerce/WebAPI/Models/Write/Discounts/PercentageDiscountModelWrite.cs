using Entities;

namespace WebAPI.Models.Write.Discounts
{
    public class PercentageDiscountModelWrite : ModelWrite<PercentageDiscount, PercentageDiscountModelWrite>
    {
        public string Name { get; set; }
        public double PercentageDiscounted { get; set; }
        public string ProductToBeDiscounted { get; set; }
        public int MinProductNeededForDiscount { get; set; }

        public override PercentageDiscount ToEntity() => new PercentageDiscount()
        {
            Name = this.Name,
            PercentageDiscounted = this.PercentageDiscounted,
            MinProductsNeededForDiscount = this.MinProductNeededForDiscount,
            ProductToBeDiscounted = this.ProductToBeDiscounted,
        };
        public override bool Equals(Object obj) => (!(obj is PercentageDiscountModelWrite discountModelWrite)) ? false : discountModelWrite.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();

    }
}
