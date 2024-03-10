using Microsoft.EntityFrameworkCore;
using Entities;
using Exceptions;
using DataAccess.Interface;

namespace DataAccess.Contexts
{
    public class UserRepository : IUserRepository
    {
        protected DbContext Context { get; set; }

        public UserRepository(DbContext context)
        {
            this.Context = context;
        }

        public ICollection<User> Get()
        {
            return this.Context.Set<User>().Where(u => u.IsDeleted.Equals(false))
                .Include(u => u.Roles)
                .ToList();
        }
        public User Get(Guid id) 
        {
            return this.Context.Set<User>().
                Include(u => u.Roles)
                .FirstOrDefault(u => u.Id.Equals(id) && u.IsDeleted.Equals(false));
        }
        public void Add(User user) 
        {
            this.Context.Set<User>().Add(user);
        }
        public void Save() 
        {
            this.Context.SaveChanges();
        }
        public bool Exists(User user)
        {
            return this.Context.Set<User>().Any(u => u.Id.Equals(user.Id));
        }
        public void Update(User oldUser, User newUser) 
        {
            UpdateAttributes(oldUser, newUser);
            this.Context.Entry(oldUser).State = EntityState.Modified;
            this.Context.SaveChanges();
        }
        private void UpdateAttributes(User oldUser, User newUser)
        {
            oldUser.Name = newUser.Name;
            oldUser.Email = newUser.Email;
            oldUser.Password = newUser.Password;
            oldUser.DeliveryAdress = newUser.DeliveryAdress;
            oldUser.IsDeleted = newUser.IsDeleted;
        }
        public void Remove(User user)
        {
            if (this.Exists(user))
            {
                user.IsDeleted = true;
                this.Context.Entry(user).State = EntityState.Modified;
                this.Context.SaveChanges();
            }
            else
            {
                throw new IncorrectRequestException("No existe el Usuario que desea eliminar.");
            }
        }
        public bool ExistsUserWithEmailAndPassword(string email, string password)
        {
            return this.Context.Set<User>()
                .Any(u => u.Email.Equals(email) && u.Password.Equals(password) && u.IsDeleted == false);
        }
        public bool IsDeleted(string email, string password) 
        {
            var user = this.Context.Set<User>().FirstOrDefault(u => u.Email.Equals(email) && u.Password.Equals(password));
            return user.IsDeleted;
        }
    }
}
