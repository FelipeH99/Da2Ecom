
namespace Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DeliveryAdress { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public List<Role> Roles { get; set; }

        public override bool Equals(object obj)
        {
            bool equals = false;
            if (IsNotNull(obj) && IsSameTypeAsThis(obj))
            {
                User oneUser = (User)obj;
                equals = this.Name.Equals(oneUser.Name) && this.Email.Equals(oneUser.Email);
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
            return this.Name.GetHashCode() + this.Email.GetHashCode();
        }
    }
}