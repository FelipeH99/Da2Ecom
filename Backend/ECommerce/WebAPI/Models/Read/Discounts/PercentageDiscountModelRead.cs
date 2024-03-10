using Entities;

namespace WebAPI.Models.Read.Discounts
{
    public class PercentageDiscountModelRead : ModelRead<PercentageDiscount, PercentageDiscountModelRead>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double PercentageDiscounted { get; set; }
        public string ProductToBeDiscounted { get; set; }
        public int MinProductNeededForDiscount { get; set; }
        public bool isActive { get; set; }

        public override PercentageDiscountModelRead SetModel(PercentageDiscount entity)
        {
            this.Id = entity.Id.ToString();
            this.Name = entity.Name;
            this.PercentageDiscounted = entity.PercentageDiscounted;
            this.ProductToBeDiscounted = entity.ProductToBeDiscounted;
            this.MinProductNeededForDiscount = entity.MinProductsNeededForDiscount;
            this.isActive = entity.IsActive;    
            return this;
        }
        public override bool Equals(Object obj) => (!(obj is PercentageDiscountModelRead discountModelRead)) ? false : discountModelRead.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();
    
    }
}
