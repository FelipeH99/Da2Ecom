
namespace Entities
{
    public class PaymentMethod
    {
        public Guid Id { get; set; }
        public string Name {  get; set; }
        public PaymentMethod() { }
        public override bool Equals(object obj)
        {
            bool equals = false;
            if (IsNotNull(obj) && IsSameTypeAsThis(obj))
            {
                PaymentMethod paymentMethod = (PaymentMethod)obj;
                equals = this.Name.Equals(paymentMethod.Name);
            }
            return equals;
        }
        public bool IsSameTypeAsThis(object obj)
        {
            return obj.GetType().Equals(this.GetType());
        }

        public bool IsNotNull(object obj)
        {
            return obj != null;
        }
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
