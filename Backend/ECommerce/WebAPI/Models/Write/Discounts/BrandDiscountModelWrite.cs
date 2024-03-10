using Entities;

namespace WebAPI.Models.Write.Discounts
{
    public class BrandDiscountModelWrite : ModelWrite<BrandDiscount, BrandDiscountModelWrite>
    {
        public string Name { get; set; }
        public Guid BrandId { get; set; }
        public int NumberOfProductsForFree { get; set; }
        public string ProductToBeDiscounted { get; set; }
        public int MinProductsNeededForDiscount { get; set; }
        public override BrandDiscount ToEntity() => new BrandDiscount()
        {
            Name = Name,
            NumberOfProductsForFree = NumberOfProductsForFree,
            ProductToBeDiscounted = ProductToBeDiscounted,
            MinProductsForPromotion = MinProductsNeededForDiscount,
            Brand = new Brand() 
            {
                Id = BrandId
            }
        };
        public override bool Equals(Object obj) => (!(obj is BrandDiscountModelWrite discountModelWrite)) ? false : discountModelWrite.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();
    }
}
