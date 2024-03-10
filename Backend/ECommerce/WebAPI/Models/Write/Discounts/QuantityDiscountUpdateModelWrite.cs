using Entities.Enums;
using Entities;

namespace WebAPI.Models.Write.Discounts
{
    public class QuantityDiscountUpdateModelWrite : ModelWrite<QuantityDiscount, QuantityDiscountUpdateModelWrite>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int NumberOfProductToBeFree { get; set; }
        public string ProductToBeDiscounted { get; set; }
        public int MinProductNeededForDiscount { get; set; }
        public string IsActive { get; set; }

        public override QuantityDiscount ToEntity() => new QuantityDiscount()
        {
            Name = this.Name,
            ProductCategory = Enum.Parse<ProductCategory>(this.Category, true),
            NumberOfProductsToBeFree = this.NumberOfProductToBeFree,
            MinProductsNeededForDiscount = this.MinProductNeededForDiscount,
            ProductToBeDiscounted = this.ProductToBeDiscounted,
            IsActive = bool.Parse(this.IsActive),
        };
        public override bool Equals(Object obj) => (!(obj is QuantityDiscountModelWrite discountModelWrite)) ? false : discountModelWrite.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();

    }
}
