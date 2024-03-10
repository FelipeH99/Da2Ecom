using Entities;

namespace DataAccess.Interface
{
    public interface IQuantityDiscountRepository
    {
        ICollection<QuantityDiscount> Get();
        QuantityDiscount Get(Guid Id);
        void Add(QuantityDiscount quantityDiscount);
        bool Exists(QuantityDiscount quantityDiscount);
        void Update(QuantityDiscount oldQuantityDiscount, QuantityDiscount newQuantityDiscount);
        void Remove(QuantityDiscount quantityDiscount);
        void Save();
    }
}
