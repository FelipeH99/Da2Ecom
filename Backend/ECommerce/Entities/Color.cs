
namespace Entities
{
    public class Color
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Color() { }

        public override bool Equals(object obj)
        {
            bool equals = false;
            if (IsNotNull(obj) && IsSameTypeAsThis(obj))
            {
                Color oneColor = (Color)obj;
                equals = this.Name.Equals(oneColor.Name);
            }
            return equals;
        }
        public override string ToString()
        {
            return this.Name;
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
