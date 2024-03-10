
namespace Entities
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public DateTime PurchaseDate { get; set; }
        public List<Product> Products { get; set; }
        public double ProductsPrice { get; set; }
        public double FinalPrice { get; set; }
        public string DiscountApplied { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Purchase() { }
        public override bool Equals(object obj)
        {
            bool equals = false;
            if (IsNotNull(obj) && IsSameTypeAsThis(obj))
            {
                Purchase onePurchase = (Purchase)obj;
                equals = this.User.Equals(onePurchase.User) && this.FinalPrice == onePurchase.FinalPrice &&
                    IsSameDate(this.PurchaseDate,onePurchase.PurchaseDate) && this.AreSameProducts(onePurchase.Products);
            }
            return equals;
        }
        private bool IsSameDate(DateTime firstPurchase, DateTime secondPurchase) 
        {
            return firstPurchase.Day.Equals(secondPurchase.Day) && firstPurchase.Month.Equals(secondPurchase.Month) && 
                firstPurchase.Year.Equals(secondPurchase.Year) && firstPurchase.Hour.Equals(secondPurchase.Hour) &&
                firstPurchase.Minute.Equals(secondPurchase.Minute);
        }
        private bool AreSameProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                if (!this.Products.Contains(product))
                {
                    return false;
                }
            }
            return true;
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
            return User.GetHashCode() + PurchaseDate.GetHashCode();
        }
    }
}
