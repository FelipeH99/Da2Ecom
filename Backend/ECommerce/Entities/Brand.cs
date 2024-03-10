
namespace Entities
{
    
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Brand() { }

        public override bool Equals(object obj)
        {
            bool equals = false;
            if (IsNotNull(obj) && IsSameTypeAsThis(obj))
            {
                Brand oneBrand = (Brand)obj;
                equals = this.Name.Equals(oneBrand.Name);
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
            return Name.GetHashCode();
        }
    }


}
