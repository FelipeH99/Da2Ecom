using Entities;
namespace BusinessLogic.Interface
{
    public interface IRoleLogic
    {
        ICollection<Role> Get();
        Role Get(Guid id);
        ICollection<Role> GetByIds(IEnumerable<Role> roles);
        ICollection<string> GetPermissionsByRole(User user);
    }
}
