using Entities;

namespace DataAccess.Interface
{
    public interface IUserRepository
    {
        ICollection<User> Get();
        User Get(Guid id);
        void Add(User user);
        bool Exists(User user);
        void Save();
        void Update(User oldUser, User newUser);
        void Remove(User user);
        bool ExistsUserWithEmailAndPassword(string email, string password);
        bool IsDeleted(string email,string password);

    }
}
