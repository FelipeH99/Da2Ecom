using System.Net.Mail;
using System.Security.Authentication;
using BusinessLogic.Interface;
using DataAccess.Interface;
using Entities;
using Exceptions;

namespace BusinessLogic
{
    public class AdminTokenLogic : IAdminTokenLogic
    {
        private IAdminTokenRepository adminTokenRepository;
        private IUserRepository userRepository;
        public AdminTokenLogic(IAdminTokenRepository adminTokenRepository, IUserRepository userRepository)
        {
            this.adminTokenRepository = adminTokenRepository;
            this.userRepository = userRepository;
        }

        public AdminToken Login(string email, string password, IUserLogic userLogic)
        {
            ValidateEmail(email);
            ValidatePassword(password);
            var adminToken = adminTokenRepository.GetOneAdminTokensByEmail(email);
            if (adminToken.Count() > 0)
            {
                if (adminToken.Any(ad => ad.User.Password.Equals(password)))
                {
                    throw new AuthenticationException("El usuario ya está logueado.");
                }
                else
                {
                    throw new AuthenticationException("No existe combinacion contrasena - usuario registrada en el sistema.");
                }
            }
            else if (userLogic.ExistsUserWithEmailAndPassword(email, password))
            {
                User user = userLogic.Get().Where(u => u.Email.Equals(email) && u.Password.Equals(password)).First();
                AdminToken at = new AdminToken { User = user };
                adminTokenRepository.Add(at);
                return at;
            }
            throw new AuthenticationException("Credenciales ingresadas no son correctas.");
        }
        public void Logout(Guid token) 
        {
            if (adminTokenRepository.Get(token) == null)
            {
                throw new AuthenticationException("El usuario no está logueado.");
            }
            else 
            {
                adminTokenRepository.Remove(token);
            }

        }
        public Guid GetToken(User user)
        {
            AdminToken adminToken = adminTokenRepository.Get().Where(a => a.User == user).First();
            return adminToken.Id;
        }

        public bool IsLogged(Guid token)
        {
            int result = adminTokenRepository.Get().Where(a => a.Id == token).ToList().Count;

            return result > 0;
        }
        public bool IsLogged(string email)
        {
            int result = adminTokenRepository.Get().Where(a => a.User.Email == email).ToList().Count;

            return result > 0;
        }
        private void ValidateEmail(string email)
        {
            if (!isFormatOk(email))
            {
                throw new IncorrectEmailException("El email no cumple con el siguiente formato: prueba@prueba.com");
            }
        }

        private bool isFormatOk(string email)
        {
            bool myReturn = false;
            try
            {
                MailAddress mail = new MailAddress(email);
                myReturn = true;
            }
            catch (FormatException)
            {

            }
            catch (ArgumentNullException)
            {

            }
            catch (ArgumentException)
            {

            }
            return myReturn;
        }

        private void ValidatePassword(string password)
        {
            if (String.IsNullOrWhiteSpace(password) || !isValidPassword(password))
            {
                throw new IncorrectPasswordException("La contraseña no puede ser vacia y debe tener al menos 7 caracteres.");
            }
        }

        private bool isValidPassword(string password)
        {
            bool isValid = true;
            if (password.Length <= 7)
            {
                isValid = false;
            }
            return isValid;
        }

        public AdminToken GetAdminTokenById(Guid Id, IRoleLogic roleService)
        {
            AdminToken adminToken = adminTokenRepository.Get().Where(a => a.Id == Id).First();
            List<Permission> permissions = new List<Permission>();
            List<Role> finalRoles = new List<Role>();
            foreach (var role in adminToken.User.Roles)
            {
                var roleById = roleService.Get(role.Id);
                finalRoles.Add(roleById);
            }
            adminToken.User.Roles = finalRoles;
            return adminToken;
        }
        public User GetUser(Guid sessionToken) 
        {
            return this.adminTokenRepository.GetUser(sessionToken);
        }
    }
}
