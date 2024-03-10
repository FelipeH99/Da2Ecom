using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using Entities;
using Exceptions;

namespace DataAccess.Contexts
{
    public class PurchaseRepository : IPurchaseRepository
    {
        protected DbContext Context { get; set; }
        public PurchaseRepository(DbContext context)
        {
            this.Context = context;
        }
        public ICollection<Purchase> Get()
        {
            return this.Context.Set<Purchase>()
                .Include(p => p.User)
                .Include(p => p.Products)
                .Include(p => p.PaymentMethod)
                .ToList();
        }
        public Purchase Get(Guid id)
        {
            return this.Context.Set<Purchase>()
                .Include(p => p.User)
                .Include(p => p.Products)
                .Include(p => p.PaymentMethod)
                .FirstOrDefault(p => p.Id.Equals(id));
        }
        public ICollection<Purchase> GetByUser(Guid id) 
        {
            return this.Context.Set<Purchase>()
                .Include(p => p.User)
                .Include(p => p.Products)
                .Where(p => p.User.Id.Equals(id)).ToList();
        }
        public void Add(Purchase purchase) 
        {
            this.Context.Set<Purchase>().Add(purchase);
        }
        public void Save() 
        {
            this.Context.SaveChanges();
        }
        public bool Exists(Purchase purchase) 
        {
            return this.Context.Set<Purchase>().Any(p => p.User.Equals(purchase.User) 
            && p.PurchaseDate.Equals(purchase.PurchaseDate) && p.Products.All(pc => purchase.Products.Contains(pc)));
        }
        public void Update(Purchase oldPurchase, Purchase newPurchase) 
        {
            UpdateAttributes(oldPurchase, newPurchase);
            this.Context.Entry(oldPurchase).State = EntityState.Modified;
            this.Context.SaveChanges();
        }
        private void UpdateAttributes(Purchase oldPurchase, Purchase newPurchase)
        {
            oldPurchase.User = newPurchase.User;
            oldPurchase.FinalPrice = newPurchase.FinalPrice;
            oldPurchase.ProductsPrice = newPurchase.ProductsPrice;
            oldPurchase.PurchaseDate = newPurchase.PurchaseDate;
            oldPurchase.Products = newPurchase.Products;
            oldPurchase.DiscountApplied = newPurchase.DiscountApplied;
        }
        public void Remove(Purchase purchase) 
        {
            if (this.Exists(purchase))
            {
                this.Context.Set<Purchase>().Remove(purchase);
                this.Context.SaveChanges();
            }
            else
            {
                throw new IncorrectRequestException("No existe la Compra que desea eliminar.");
            }
        }
    }
}
