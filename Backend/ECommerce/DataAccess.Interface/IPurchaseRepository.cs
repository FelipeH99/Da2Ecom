using Entities;

namespace DataAccess.Interface
{
    public interface IPurchaseRepository
    {
        ICollection<Purchase> Get();
        Purchase Get(Guid id);
        void Add(Purchase purchase);
        void Save();
        bool Exists(Purchase purchase);
        void Update(Purchase oldPurchase, Purchase newPurchase);
        void Remove(Purchase purchase);
        ICollection<Purchase> GetByUser(Guid userId);
    }
}
