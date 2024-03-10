using Entities;

namespace WebAPI.Models.Read.Discounts
{
    public class QuantityDiscountModelRead : ModelRead<QuantityDiscount, QuantityDiscountModelRead>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProductCategory { get; set; }
        public int MinProductsNeededForDiscount { get; set; }
        public int NumberOfProductsToBeFree { get; set; }
        public string ProductToBeDiscounted { get; set; }
        public bool IsActive { get; set; }

        public override QuantityDiscountModelRead SetModel(QuantityDiscount entity)
        {
            this.Id = entity.Id.ToString();
            this.Name = entity.Name;
            this.ProductCategory = entity.ProductCategory.ToString();
            this.ProductToBeDiscounted = entity.ProductToBeDiscounted;
            this.MinProductsNeededForDiscount = entity.MinProductsNeededForDiscount;
            this.NumberOfProductsToBeFree = entity.NumberOfProductsToBeFree;
            this.IsActive = entity.IsActive;
            return this;
        }
        public override bool Equals(Object obj) => (!(obj is QuantityDiscountModelRead discountModelRead)) ? false : discountModelRead.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();
    }
}
