using Entities;

namespace BusinessLogic.Interface
{
    public interface IAdminTokenLogic
    {
        AdminToken Login(string email, string password, IUserLogic userLogic);
        bool IsLogged(Guid id);
        bool IsLogged(string email);

        Guid GetToken(User user);
        AdminToken GetAdminTokenById(Guid Id, IRoleLogic roleService);
        void Logout(Guid token);
        User GetUser(Guid sessionToken);
    }
}
