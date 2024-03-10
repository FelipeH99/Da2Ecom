using Entities;
using WebAPI.Models.Read;

namespace WebAPI.Models
{
    public class BrandModel : ModelRead<Brand, BrandModel>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public BrandModel() { }

        public override BrandModel SetModel(Brand entity)
        {
            this.Name = entity.Name;
            this.Id = entity.Id.ToString();
            return this;
        }
        public override bool Equals(Object obj) => (!(obj is BrandModel brandModel)) ? false : brandModel.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();
    }
}

