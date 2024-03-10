using Reflection;
using Entities;
namespace BusinessLogic.Interface
{
    public interface IPurchaseLogic
    {
        ICollection<Purchase> Get();
        Purchase Get(Guid id);
        Purchase Create(Purchase purchase,IProductLogic productLogic,IPaymentMethodLogic paymentMethodService
            ,IReflectionImplementation reflection, IDiscountLogic discountLogic);
        Purchase Update(Guid id, Purchase purchase);
        void Remove(Purchase purchase);
        ICollection<Purchase> GetByUserId(Guid userId);
    }
}
