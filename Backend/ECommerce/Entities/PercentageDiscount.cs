using Microsoft.IdentityModel.Tokens;

namespace Entities
{
    public class PercentageDiscount
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double PercentageDiscounted { get; set; }
        public string ProductToBeDiscounted { get; set; }
        public int MinProductsNeededForDiscount { get; set; }
        public bool IsActive { get; set; }
        public PercentageDiscount() { }
        public override bool Equals(object obj)
        {
            bool equals = false;
            if (IsNotNull(obj) && IsSameTypeAsThis(obj))
            {
                PercentageDiscount percentageDiscount = (PercentageDiscount)obj;
                equals = this.Name.Equals(percentageDiscount.Name);
            }
            return equals;
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

        public double CalculateDiscount(List<Product> products)
        {
            if (products.IsNullOrEmpty() || products.Count < MinProductsNeededForDiscount)
            {
                return 0;
            }
            else 
            {
                if (this.ProductToBeDiscounted.Equals("MaxValue"))
                {
                    List<Product> sortedList = products.OrderBy(p => p.Price).Where(p => p.AvailableForPromotion.Equals(true)).ToList();
                    return GetDiscountMaxPriceProducts(sortedList); 
                }
                else 
                {
                    List<Product> sortedList = products.OrderBy(d => d.Price).Where(p => p.AvailableForPromotion.Equals(true)).ToList();
                    return GetDiscountMinPriceProducts(sortedList);
                }
            }
        }
        private double GetDiscountMaxPriceProducts(List<Product> products) 
        {
            if(!products.IsNullOrEmpty()) 
            {
                double price = products[products.Count - 1].Price;
                return price * this.PercentageDiscounted;
            }
            return 0;

        }
        private double GetDiscountMinPriceProducts(List<Product> products) 
        {
            if (!products.IsNullOrEmpty()) 
            {
                double price = products[0].Price;
                return price * this.PercentageDiscounted;
            }
            return 0;
        }
    }
}
