using Entities;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using Exceptions;

namespace DataAccess.Contexts
{
    public class AdminTokenRepository : IAdminTokenRepository
    {
        protected DbContext Context { get; set; }

        public AdminTokenRepository(DbContext context)
        {
            this.Context = context;
        }

        public ICollection<AdminToken> Get()
        {
            return this.Context.Set<AdminToken>()
                .Include(adToken => adToken.User)
                .Include(adToken => adToken.User.Roles)
                .ToList()
                ;
        }
        public AdminToken Get(Guid token)
        {
            return this.Context.Set<AdminToken>()
                .Include(adToken => adToken.User)
                .Include(adToken => adToken.User.Roles)
                .FirstOrDefault(ad => ad.Id.Equals(token))
                ;
        }
        public AdminToken GetAdminTokenById(Guid id)
        {
            return this.Context.Set<AdminToken>()
                .Include(adToken => adToken.User)
                .Include(adToken => adToken.User.Roles).ToList()
                .Where(adToken => adToken.Id == id)
                .FirstOrDefault<AdminToken>();
        }
        public void Add(AdminToken adminToken)
        {
            this.Context.Set<User>().Attach(adminToken.User);
            this.Context.Set<AdminToken>().Add(adminToken);
            this.Context.SaveChanges();
        }
        public bool Exists(Guid token)
        {
            return this.Context.Set<AdminToken>()
                .Any(adToken => token.Equals(adToken.Id));
        }
        public void RemoveAdminTokensByUser(User oneUser)
        {
            var itemsToDelete = Context.Set<AdminToken>().Where(a => a.User.Email == oneUser.Email);
            this.Context.Set<AdminToken>().RemoveRange(itemsToDelete);
            this.Context.SaveChanges();
        }
        public void Remove(Guid token) 
        {
            AdminToken adToken = this.Get(token);
            if (this.Exists(token))
            {
                this.Context.Set<AdminToken>().Remove(adToken);
                this.Context.SaveChanges();
            }
            else
            {
                throw new IncorrectRequestException("No existe el token que desea eliminar.");
            }

        }
        public ICollection<AdminToken> GetOneAdminTokensByEmail(string email)
        {
            return this.Context.Set<AdminToken>()
                .Include(adToken => adToken.User)
                .Where(adToken => adToken.User.Email.Equals(email)).ToList();
        }
        public User GetUser(Guid sessionToken) 
        {
            var adT = this.Context.Set<AdminToken>().Include(ad => ad.User).Include(u => u.User.Roles).FirstOrDefault(ad => ad.Id.Equals(sessionToken));
            return adT.User;
        }
    }
}
