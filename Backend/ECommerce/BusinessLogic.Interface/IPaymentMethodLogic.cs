using Entities;

namespace BusinessLogic.Interface
{
    public interface IPaymentMethodLogic
    {
        ICollection<PaymentMethod> Get();
        PaymentMethod Get(Guid id);
    }
}
