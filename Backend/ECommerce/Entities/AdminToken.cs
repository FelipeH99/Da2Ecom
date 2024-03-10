
namespace Entities
{
    public class AdminToken
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public AdminToken() 
        {
            Id = Guid.NewGuid();
        }
        public override bool Equals(object obj)
        {
            bool equals = false;
            if (IsNotNull(obj) && IsSameTypeAsThis(obj))
            {
                AdminToken oneAdminToken = (AdminToken)obj;
                equals = this.User.Email.Equals(oneAdminToken.User.Email);
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
            return Id.GetHashCode();
        }
    }
}
