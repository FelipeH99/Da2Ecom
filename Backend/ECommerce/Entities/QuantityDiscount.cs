using Entities.Enums;
using Microsoft.IdentityModel.Tokens;

namespace Entities
{
    public class QuantityDiscount
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int MinProductsNeededForDiscount { get; set; }
        public int NumberOfProductsToBeFree { get; set; }
        public string ProductToBeDiscounted { get; set; }
        public bool IsActive { get; set; }

        public QuantityDiscount() { }
        public override bool Equals(object obj)
        {
            bool equals = false;
            if (IsNotNull(obj) && IsSameTypeAsThis(obj))
            {
                QuantityDiscount quantityDiscount = (QuantityDiscount)obj;
                equals = this.Name.Equals(quantityDiscount.Name) && 
                    this.ProductCategory.Equals(quantityDiscount.ProductCategory);
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
            if (products.IsNullOrEmpty() || !ProductsSameCategory(products))
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
                int productsToDiscount = this.NumberOfProductsToBeFree;

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
                int productsToDiscount = this.NumberOfProductsToBeFree;

                for (int i = 0; productsToDiscount > 0; i++)
                {
                    discount += products[i].Price;
                    productsToDiscount--;
                }
                return discount;
            }
            return 0;
        }
        private bool ProductsSameCategory(List<Product> products) 
        {
            int productsNeeded = 0;
            foreach (Product product in products)
            {
                if (product.ProductCategory.Equals(this.ProductCategory))
                {
                    productsNeeded++;
                }
            }
            if (this.MinProductsNeededForDiscount <= productsNeeded) { return true; }
            else { return false; }
        }
    }
}
