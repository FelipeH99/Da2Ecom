using DataAccess.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        protected DbContext Context { get; set; }
        public PaymentMethodRepository(DbContext context)
        {
            this.Context = context;
        }
        public ICollection<PaymentMethod> Get()
        {
            return this.Context.Set<PaymentMethod>().ToList();
        }
        public PaymentMethod Get(Guid Id)
        {
            return this.Context.Set<PaymentMethod>().FirstOrDefault(pm => pm.Id.Equals(Id));
        }
    }
}
