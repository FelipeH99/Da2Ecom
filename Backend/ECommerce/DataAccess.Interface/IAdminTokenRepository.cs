using Entities;

namespace DataAccess.Interface
{
    public interface IAdminTokenRepository
    {
        ICollection<AdminToken> Get();
        AdminToken Get(Guid token);
        AdminToken GetAdminTokenById(Guid id);
        void Add(AdminToken adminToken);
        bool Exists(Guid adminToken);
        void RemoveAdminTokensByUser(User user);
        void Remove(Guid token);
        ICollection<AdminToken> GetOneAdminTokensByEmail(string email);
        User GetUser(Guid sessionToken);
    }
}
