using Entities;

namespace DataAccess.Interface
{
    public interface IRoleRepository
    {
        ICollection<Role> Get();
        Role Get(Guid id);
        Role GetRoleByName(string name);
        void Save();
    }
}
