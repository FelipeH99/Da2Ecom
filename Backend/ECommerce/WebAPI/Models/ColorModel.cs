using Entities;
using WebAPI.Models.Read;

namespace WebAPI.Models
{
    public class ColorModel : ModelRead<Color, ColorModel>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ColorModel() { }

        public override ColorModel SetModel(Color entity)
        {
            this.Name = entity.Name;
            this.Id = entity.Id.ToString();
            return this;
        }
        public override bool Equals(Object obj) => (!(obj is ColorModel colorModel)) ? false : colorModel.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();
    }
}

