using Entities;

namespace DataAccess.Interface
{
    public interface IPaymentMethodRepository
    {
        ICollection<PaymentMethod> Get();
        PaymentMethod Get(Guid id);
    }
}
