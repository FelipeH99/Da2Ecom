using BusinessLogic.Interface;
using DataAccess.Interface;
using Entities;
using System.Net.Mail;
using Exceptions;
using Microsoft.IdentityModel.Tokens;
namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private IUserRepository UserRepository;
        private IRoleRepository RoleRepository;
        public UserLogic(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }
        public UserLogic(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.UserRepository = userRepository;
            this.RoleRepository = roleRepository;
        }
        public ICollection<User> Get()
        {
            return UserRepository.Get();
        }
        public User Get(Guid id)
        {
            return UserRepository.Get(id);
        }
        public bool Exists(User oneUser)
        {
            return this.UserRepository.Exists(oneUser);
        }
        public User Create(User oneUser, IRoleLogic roleService) 
        {
            ValidateUser(oneUser);
            SetRolesNames(oneUser,roleService);
            ValidateRepeatedUser(oneUser);
            this.UserRepository.Add(oneUser);
            this.UserRepository.Save();
            return oneUser;
        }
        public User Update(Guid id, User oneUser, IRoleLogic roleService)
        {
            User userToChange = Get(id);
            ValidateUser(userToChange);
            ValidateUser(oneUser);
            SetRolesNames(oneUser, roleService);
            ValidateNullFields(userToChange);
            ValidateNullFields(oneUser);
            UserRepository.Update(userToChange,oneUser);
            return userToChange;
        }
        public void Remove(User oneUser)
        {
            UserRepository.Remove(oneUser);
            UserRepository.Save();
        }
        private void SetRolesNames(User oneUser, IRoleLogic roleService) 
        {
            List<Role> rolesBaseDatos = new List<Role>();
            foreach (Role role in oneUser.Roles) 
            {
                var roleDatabase = roleService.Get(role.Id);
                if (roleDatabase == null)
                {
                    throw new IncorrectRequestException("El rol ingresado no existe en la base de datos.");
                }
                else 
                {
                    rolesBaseDatos.Add(roleDatabase);

                }
            }
            oneUser.Roles = rolesBaseDatos;
        }
        private void ValidateRepeatedUser(User oneUser)
        {
            if (Exists(oneUser))
            {
                throw new RepeatedObjectException("El usuario ya se encuentra en el sistema");
            }
        }
        private void ValidateUser(User oneUser)
        {
            ValidateNullUser(oneUser);
            EmptyOrWhiteSpaceEmail(oneUser.Email);
            EmptyOrWhiteSpacePassword(oneUser.Password);
            EmptyOrWhiteSpaceName(oneUser.Name);
            ValidatePassword(oneUser.Password);
            ValidateEmail(oneUser.Email);
            ValidateDeliveryAddress(oneUser.DeliveryAdress);
            ValidateRoleList(oneUser);

        }
        private void ValidateNullFields(User oneUser) 
        {
            if (oneUser.Name == null || oneUser.Password == null || oneUser.DeliveryAdress == null ||
                oneUser.Email == null || oneUser.Roles == null) 
            {
                throw new ArgumentNullException("Para actualizar un usuario debe indicar un valor para cada " +
                    "uno de los campos solicitados.");
            }
        }
        private void ValidateRoleList(User oneUser) 
        {
            if (oneUser.Roles.IsNullOrEmpty()) 
            {
                var basicRole = this.RoleRepository.GetRoleByName("Buyer");
                if (basicRole == null)
                {
                    throw new IncorrectRequestException("No se puede crear el usuario porque no existe en el sistema " +
                        "el rol necesario, contactese con un administrador.");
                }
                else 
                {
                    List<Role> roles = new List<Role>();
                    roles.Add(basicRole);
                    oneUser.Roles = roles;
                }
            }
        }
        private void ValidateDeliveryAddress(string deliveryAddress) 
        {
            if (IsEmptyOrWhiteSpaceString(deliveryAddress))
            {
                throw new IncorrectDeliveryAddressException("Direccion de envio no puede ser vacio");
            }
        }
        private void ValidateNullUser(User oneUser)
        {
            if (oneUser == null)
            {
                throw new IncorrectRequestException("No existe ese usuario.");
            }
        }
        private void EmptyOrWhiteSpaceEmail(string email)
        {
            if (IsEmptyOrWhiteSpaceString(email))
            {
                throw new IncorrectEmailException("El email ingresado no puede ser vacio");
            }
        }

        private void EmptyOrWhiteSpacePassword(string password)
        {
            if (IsEmptyOrWhiteSpaceString(password))
            {
                throw new IncorrectPasswordException("La contraseña no puede ser vacia.");
            }
        }
        private void EmptyOrWhiteSpaceName(string name)
        {
            if (IsEmptyOrWhiteSpaceString(name))
            {
                throw new IncorrectNameException("Nombre no puede ser vacio");
            }
        }
        private void ValidatePassword(string password)
        {
            var passwordMinimumLength = 8;
            if (password.Length < passwordMinimumLength)
            {
                throw new IncorrectPasswordException("La contraseña debe tener al menos 8 caracteres.");
            }
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

        private bool IsEmptyOrWhiteSpaceString(string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public bool ExistsUserWithEmailAndPassword(string email, string password)
        {
            return this.UserRepository.ExistsUserWithEmailAndPassword(email, password);
        }
        public bool IsDeleted(string email, string password) 
        {
            return this.UserRepository.IsDeleted(email, password);
        }

    }
}
