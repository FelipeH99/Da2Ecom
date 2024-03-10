using DataAccess.Interface;
using Entities;
using BusinessLogic.Interface;
using Exceptions;

namespace BusinessLogic
{
    public class RoleLogic : IRoleLogic
    {
        private IRoleRepository RoleRepository;

        public RoleLogic(IRoleRepository roleRepository)
        {
            this.RoleRepository = roleRepository;
        }

        public ICollection<Role> Get()
        {
            return RoleRepository.Get();
        }
        public Role Get(Guid id)
        {
            return RoleRepository.Get(id);
        }

        public ICollection<Role> GetByIds(IEnumerable<Role> roles)
        {
            var rolesDB = this.Get();
            var role = new Role();
            var roleResult = new List<Role>();
            foreach (var roleId in roles) 
            {
                role = rolesDB.Where(r => r.Id.Equals(roleId.Id)).FirstOrDefault();
                if (role != null)
                {
                    roleResult.Add(role);
                }
                else 
                {
                    throw new IncorrectRequestException("Uno de los roles no existe en el sistema.");
                }
            }
            return roleResult;
        }

        public ICollection<string> GetPermissionsByRole(User user)
        {
            var permissionsString = new List<string>();
            foreach (Role role in user.Roles) 
            {
                var roleInDb = this.RoleRepository.Get(role.Id);
                if (roleInDb != null) 
                {
                    var permissions = roleInDb.Permissions;
                    foreach (var permission in permissions) 
                    {
                        permissionsString.Add(permission.Name);
                    }
                }
            }
            return permissionsString;
        }
    }
}
