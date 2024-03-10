using Microsoft.IdentityModel.Tokens;

namespace Entities
{
    public class BrandDiscount
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Brand Brand { get; set; }
        public int MinProductsForPromotion { get; set; }
        public int NumberOfProductsForFree { get; set; }
        public string ProductToBeDiscounted { get; set; }
        public bool IsActive { get; set; }
        public BrandDiscount() { }
        public override bool Equals(object obj)
        {
            bool equals = false;
            if (IsNotNull(obj) && IsSameTypeAsThis(obj))
            {
                BrandDiscount brandDiscount = (BrandDiscount)obj;
                equals = this.Name.Equals(brandDiscount.Name);
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
            if (products.IsNullOrEmpty() || !ProductsSameBrand(products))
            {
                return 0;
            }
            else
            {
                if (ProductToBeDiscounted.Equals("MaxValue"))
                {
                    List<Product> sortedList = products.OrderBy(d => d.Price).Where(p => p.AvailableForPromotion.Equals(true)).ToList();
                    return GetItemsPricesMaxValue(sortedList);

                }
                else
                {
                    List<Product> sortedList = products.OrderBy(d => d.Price).Where(p => p.AvailableForPromotion.Equals(true)).ToList();
                    return GetItemsPricesMinValue(sortedList);
                }

            }
        }
        private double GetItemsPricesMaxValue(List<Product> products)
        {
            if (!products.IsNullOrEmpty()) 
            {
                double discount = 0;
                int productsToDiscount = this.NumberOfProductsForFree;

                for (int i = products.Count - 1; productsToDiscount > 0; i--)
                {
                    discount += products[i].Price;
                    productsToDiscount--;
                }
                return discount;
            }
            return 0;
        }
        private double GetItemsPricesMinValue(List<Product> products)
        {
            if (!products.IsNullOrEmpty()) 
            {
                double discount = 0;
                int productsToDiscount = this.NumberOfProductsForFree;

                for (int i = 0; productsToDiscount > 0; i++)
                {
                    discount += products[i].Price;
                    productsToDiscount--;
                }
                return discount;
            }
            return 0;
        }
        private bool ProductsSameBrand(List<Product> products)
        {
            int productsNeeded = 0;
            foreach (Product product in products)
            {
                if (product.Brand.Equals(this.Brand))
                {
                    productsNeeded++;
                }
            }
            if (this.MinProductsForPromotion <= productsNeeded) { return true; }
            else { return false; }
        }
    }
}
