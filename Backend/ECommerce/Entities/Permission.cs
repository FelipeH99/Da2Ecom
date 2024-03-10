
namespace Entities
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Permission() { }

        public override bool Equals(object? obj)
        {
            bool equals = false;
            if (IsNotNull(obj) && IsSameTypeAsThis(obj))
            {
                Permission onePermission = (Permission)obj;
                equals = this.Name.Equals(onePermission.Name);
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
