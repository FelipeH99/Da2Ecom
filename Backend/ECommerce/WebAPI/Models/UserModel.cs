using Entities;
namespace WebAPI.Models
{
    public class UserModel : Model<User, UserModel>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string DeliveryAdress { get; set; }
        public IEnumerable<Guid> RolesId { get; set; }
        public string UserId { get; set; }

        public UserModel()
        {
        }

        public override User ToEntity() => new User()
        {
            Id = this.Id,
            Email = this.Email,
            Name = this.Name,
            Password = this.Password,
            DeliveryAdress = this.DeliveryAdress,
            Roles = this.RolesId.Select(r => new Role()
            {
                Id = r
            }).ToList(),
        };

        public override UserModel SetModel(User entity)
        {
            this.Id = entity.Id;
            this.Email = entity.Email;
            this.Name = entity.Name;
            this.Password = entity.Password;
            this.DeliveryAdress = entity.DeliveryAdress;
            this.RolesId = entity.Roles.Select(r => r.Id).ToList();
            this.UserId = entity.Id.ToString();
            return this;
        }

        public override bool Equals(Object obj) => (!(obj is UserModel userModel)) ? false : userModel.Name.Equals(this.Name);
        public override int GetHashCode() => this.Name.GetHashCode();
    }
}
