using Entities;
using Microsoft.EntityFrameworkCore;
using DataAccess.Interface;

namespace DataAccess.Contexts
{
    public class RoleRepository : IRoleRepository
    {
        protected DbContext Context { get; set; }
        public RoleRepository(DbContext context)
        {
            this.Context = context;
        }
        public ICollection<Role> Get()
        {
            return this.Context.Set<Role>()
                .Include(r => r.Permissions)
                .ToList();
        }

        public Role Get(Guid id)
        {
            return this.Context.Set<Role>()
                .Include(r => r.Permissions)
                .FirstOrDefault(r => r.Id.Equals(id));
        }
        public void Save()
        {
            this.Context.SaveChanges();
        }
        public Role GetRoleByName(string name) 
        {
            return this.Context.Set<Role>()
                .Include(r => r.Permissions)
                .FirstOrDefault(r => r.Name.Equals(name));
        }
    }
}
