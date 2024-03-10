using Entities;

namespace WebAPI.Models
{
    public class ConnectedUserModel : Model<User, ConnectedUserModel>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ConnectedUserModel()
        {
        }
        public override ConnectedUserModel SetModel(User entity)
        {
            this.Email = entity.Email;
            this.Password = entity.Password;
            this.Id = entity.Id;
            return this;
        }

        public override bool Equals(Object obj) => (!(obj is ConnectedUserModel userModel)) ? false : userModel.Email.Equals(this.Email);
        public override int GetHashCode() => this.Email.GetHashCode();

        public override User ToEntity()
        {
            throw new NotImplementedException();
        }
    }
}
