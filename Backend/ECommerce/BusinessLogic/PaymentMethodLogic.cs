using BusinessLogic.Interface;
using DataAccess.Interface;
using Entities;

namespace BusinessLogic
{
    public class PaymentMethodLogic : IPaymentMethodLogic
    {
        private IPaymentMethodRepository PaymentMethodRepository;

        public PaymentMethodLogic(IPaymentMethodRepository paymentMethodRepository)
        {
            this.PaymentMethodRepository = paymentMethodRepository;
        }
        public ICollection<PaymentMethod> Get()
        {
            return PaymentMethodRepository.Get();
        }
        public PaymentMethod Get(Guid id)
        {
            return PaymentMethodRepository.Get(id);
        }
    }
}
