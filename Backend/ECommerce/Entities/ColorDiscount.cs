using Microsoft.IdentityModel.Tokens;

namespace Entities
{
    public  class ColorDiscount
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
        public double PercentageDiscount { get; set; }
        public string ProductToBeDiscounted { get; set; }
        public int MinProductsNeededForDiscount { get; set; }
        public bool IsActive { get; set; }
        public ColorDiscount() { }
        public override bool Equals(object obj)
        {
            bool equals = false;
            if (IsNotNull(obj) && IsSameTypeAsThis(obj))
            {
                ColorDiscount colorDiscount = (ColorDiscount)obj;
                equals = this.Name.Equals(colorDiscount.Name);
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
            if (products.IsNullOrEmpty() || !ProductsSameColor(products))
            {
                return 0;
            }
            else
            {
                if (ProductToBeDiscounted.Equals("MaxValue"))
                {
                    List<Product> sortedList = products.OrderBy(d => d.Price).Where(p => p.AvailableForPromotion.Equals(true)).ToList();
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
            if (!products.IsNullOrEmpty()) 
            {
                double price = products[products.Count - 1].Price;
                return price * this.PercentageDiscount;
            }
            return 0;
        }
        private double GetDiscountMinPriceProducts(List<Product> products)
        {
            if (!products.IsNullOrEmpty())
            {
                double price = products[0].Price;
                return price * this.PercentageDiscount;
            }
            return 0;
        }
        private bool ProductsSameColor(List<Product> products)
        {
            int productsNeeded = 0;
            foreach (Product product in products)
            {
                if (product.Colors.Contains(this.Color))
                {
                    productsNeeded++;
                }
            }
            if (this.MinProductsNeededForDiscount <= productsNeeded) { return true; }
            else { return false; }
        }
    }
}
