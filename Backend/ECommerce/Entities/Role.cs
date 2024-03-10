
namespace Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Permission> Permissions { get; set; }
        public Role() { }
        public override bool Equals(object obj)
        {
            bool equals = false;
            if (IsNotNull(obj) && IsSameTypeAsThis(obj))
            {
                Role oneRole = (Role)obj;
                equals = this.Name.Equals(oneRole.Name) && this.Permissions.Equals(oneRole.Permissions);
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
    }
}
