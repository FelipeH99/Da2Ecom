using Entities;
namespace BusinessLogic.Interface
{
    public interface IUserLogic
    {
        ICollection<User> Get();
        User Get(Guid id);
        bool Exists(User oneUser);
        User Create(User oneUser, IRoleLogic roleService);
        User Update(Guid id, User oneUser, IRoleLogic roleService);
        void Remove(User user);
        bool ExistsUserWithEmailAndPassword(string email, string password);
        bool IsDeleted(string email, string password);


    }
}
