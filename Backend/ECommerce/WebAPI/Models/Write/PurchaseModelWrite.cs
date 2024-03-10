using Entities;

namespace WebAPI.Models.Write
{
    public class PurchaseModelWrite : ModelWrite<Purchase, PurchaseModelWrite>
    {
        public string UserId { get; set; }
        public List<string> ProductIds { get; set; }
        public string PaymentMethodId { get; set; }

        public override Purchase ToEntity() => new Purchase()
        {
            User = new User() 
            {
                Id = Guid.Parse(UserId),
            },
            Products = this.ProductIds.Select(pId => new Product()
            {
                Id = Guid.Parse(pId)
            }).ToList(),
            PaymentMethod = new PaymentMethod() 
            {
                Id= Guid.Parse(PaymentMethodId),
            }

        };
        public override bool Equals(Object obj) => (!(obj is PurchaseModelWrite purchaseModelWrite)) ? false : purchaseModelWrite.UserId.Equals(this.UserId);
        public override int GetHashCode() => this.UserId.GetHashCode();
    }
}
