using Entities;
using Entities.Enums;

namespace WebAPI.Models.Write.Discounts
{
    public class QuantityDiscountModelWrite : ModelWrite<QuantityDiscount, QuantityDiscountModelWrite>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int NumberOfProductToBeFree { get; set; }
        public string ProductToBeDiscounted { get; set; }
        public int MinProductNeededForDiscount { get; set; }

        public override QuantityDiscount ToEntity() => new QuantityDiscount()
        {
            Name = this.Name,
            ProductCategory = Enum.Parse<ProductCategory>(this.Category, true),
            NumberOfProductsToBeFree = this.NumberOfProductToBeFree,
            MinProductsNeededForDiscount = this.MinProductNeededForDiscount,
            ProductToBeDiscounted = this.ProductToBeDiscounted,
        };
        public override bool Equals(Object obj) => (!(obj is QuantityDiscountModelWrite discountModelWrite)) ? false : discountModelWrite.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();
    }
}
