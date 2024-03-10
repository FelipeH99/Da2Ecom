using Entities;

namespace WebAPI.Models.Read
{
    public class PurchaseModelCreatedRead : ModelRead<Purchase, PurchaseModelCreatedRead>
    {
        public string Id { get; set; }
        public double Price { get; set; }
        public string UserName { get; set; }
        public string DiscountApplied { get; set; }
        public List<string> ProductsNames { get; set; }

        public override PurchaseModelCreatedRead SetModel(Purchase entity)
        {
            this.UserName = entity.User.Name;
            this.Price = entity.FinalPrice;
            this.DiscountApplied = entity.DiscountApplied;
            this.ProductsNames = CreateProductsList(entity.Products);
            this.Id = entity.Id.ToString();
            return this;
        }
        private List<string> CreateProductsList(List<Product> products)
        {
            List<string> productsString = new List<string>();
            foreach (Product product in products)
            {
                productsString.Add(product.Name);
            }
            return productsString;
        }
        public override bool Equals(Object obj) => (!(obj is PurchaseModelCreatedRead purchaseModelCreateRead)) ? false : purchaseModelCreateRead.UserName.Equals(this.UserName);
        public override int GetHashCode() => this.UserName.GetHashCode();
    }
}
