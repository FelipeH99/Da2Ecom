using Entities;

namespace WebAPI.Models.Read.Discounts
{
    public class BrandDiscountModelRead : ModelRead<BrandDiscount, BrandDiscountModelRead>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public int NumberOfProductsForFree { get; set; }
        public string ProductToBeDiscounted { get; set; }
        public int MinProductsNeededForDiscount { get; set; }
        public bool IsActive { get; set; }
        public override BrandDiscountModelRead SetModel(BrandDiscount entity)
        {
            this.Name = entity.Name;
            this.BrandName = entity.Brand.Name;
            this.NumberOfProductsForFree = entity.NumberOfProductsForFree;
            this.ProductToBeDiscounted = entity.ProductToBeDiscounted;
            this.MinProductsNeededForDiscount = entity.MinProductsForPromotion;
            this.Id = entity.Id.ToString();
            this.IsActive = entity.IsActive;
            return this;
        }
        public override bool Equals(Object obj) => (!(obj is BrandDiscountModelRead discountModelRead)) ? false : discountModelRead.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();
    }
}
