using Entities;
namespace BusinessLogic.Interface
{
    public interface IQuantityDiscountLogic
    {
        ICollection<QuantityDiscount> Get();
        QuantityDiscount Get(Guid id);
        QuantityDiscount Create(QuantityDiscount quantityDiscount);
        QuantityDiscount Update(Guid id, QuantityDiscount quantityDiscount);
        void Remove(QuantityDiscount quantityDiscount);
        
    }
}
