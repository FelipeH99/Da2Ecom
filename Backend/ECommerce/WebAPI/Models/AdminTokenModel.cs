using Entities;

namespace WebAPI.Models
{
    public class AdminTokenModel : Model<AdminToken, AdminTokenModel>
    {
        public Guid Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public AdminTokenModel()
        {
        }

        public override AdminToken ToEntity() => new AdminToken()
        {
            Id = this.Id,
        };

        public override AdminTokenModel SetModel(AdminToken entity)
        {
            this.Id = entity.Id;
            this.email = entity.User.Email;
            this.password = entity.User.Password;
            return this;
        }

        public override bool Equals(Object obj) => (!(obj is AdminTokenModel adminTokenModel)) ? false : adminTokenModel.Id.Equals(this.Id);
        public override int GetHashCode() => this.Id.GetHashCode();
    }
}
