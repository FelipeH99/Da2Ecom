using Entities;

namespace WebAPI.Models.Read
{
    public class PurchaseModelRead : ModelRead<Purchase, PurchaseModelRead>
    {
        public string UserName { get; set; }
        public string PurchaseDate { get; set; }
        public List<string> Products { get; set; }
        public double ProductsPrice { get; set; }
        public double FinalPrice { get; set; }
        public string Discount { get; set; }


        public override PurchaseModelRead SetModel(Purchase entity)
        {
            this.UserName = entity.User.Name;
            this.PurchaseDate = entity.PurchaseDate.ToString();
            this.Products = ConvertToStringList(entity.Products);
            this.ProductsPrice = entity.ProductsPrice;
            this.FinalPrice = entity.FinalPrice;
            this.Discount = entity.DiscountApplied;
            return this;
        }

        private List<string> ConvertToStringList(List<Product> products) 
        {
            List<string> productsString = new List<string>();
            foreach (Product product in products) 
            {
                productsString.Add(product.Name);
            }
            return productsString;
        }
        public override bool Equals(Object obj) => (!(obj is PurchaseModelRead purchaseModelRead)) ? false : purchaseModelRead.UserName.Equals(this.UserName);
        public override int GetHashCode() => this.UserName.GetHashCode();
    }
}
