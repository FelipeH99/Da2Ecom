using Entities;
using TestSetUp;
using Exceptions;
using Moq;
using DataAccess.Interface;
using BusinessLogic.Interface;

namespace BusinessLogic.Test
{
    [TestClass]
    public class UserLogicTest : TestSetUps
    {
        [TestMethod]
        public void GetUsersOkTest()
        {
            List<User> userList = new List<User>();
            List<User> userResultList = new List<User>();
            User oneUser = InitOneUserComplete();
            userList.Add(oneUser);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Get()).Returns(userList);
            var userService = new UserLogic(userRepositoryMock.Object);

            userResultList = userService.Get().ToList<User>();
            userRepositoryMock.VerifyAll();
            Assert.IsTrue(userResultList.Any(u => u.Id == oneUser.Id));
        }
        [TestMethod]
        public void GetUserByIdOkTest()
        {
            User oneUser = InitOneUserComplete();

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Get(oneUser.Id)).Returns(oneUser);
            var userService = new UserLogic(userRepositoryMock.Object);

            List<User> userResultList = new List<User>();
            User myUserResult = userService.Get(oneUser.Id);
            userRepositoryMock.VerifyAll();
            Assert.AreEqual(myUserResult.Id, oneUser.Id);
        }
        [TestMethod]
        public void UserExistsOkTest()
        {
            User oneUser = InitOneUserComplete();

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Exists(oneUser)).Returns(true);
            var userService = new UserLogic(userRepositoryMock.Object);

            var result = userService.Exists(oneUser);
            userRepositoryMock.VerifyAll();
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void CreateOkTest()
        {
            User oneUser = InitOneUserComplete();
            var role = GiveMeRoleSecondRoleListComplete();

            var roleLogicMock = new Mock<IRoleLogic>(MockBehavior.Strict);
            roleLogicMock.Setup(r => r.Get(It.IsAny<Guid>())).Returns(role);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Exists(oneUser)).Returns(false);
            userRepositoryMock.Setup(u => u.Add(oneUser));
            userRepositoryMock.Setup(u => u.Save());
            var userService = new UserLogic(userRepositoryMock.Object);
            var userResult = userService.Create(oneUser, roleLogicMock.Object);

            userRepositoryMock.VerifyAll();
            Assert.AreEqual(userResult.Id, oneUser.Id);
        }
        [ExpectedException(typeof(RepeatedObjectException))]
        [TestMethod]
        public void AddRepeatedUserTest()
        {
            User oneUser = InitAnotherUserComplete();
            Role role = InitOneRoleComplete();

            var roleLogicMock = new Mock<IRoleLogic>(MockBehavior.Strict);
            roleLogicMock.Setup(r => r.Get(It.IsAny<Guid>())).Returns(role);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Exists(oneUser)).Returns(true);
            userRepositoryMock.Setup(u => u.Save());

            var userService = new UserLogic(userRepositoryMock.Object);
            userService.Create(oneUser, roleLogicMock.Object);
        }
        [TestMethod]
        public void UpdateUserOkTest()
        {
            User oneUser = InitOneUserComplete();
            User newUser = InitAnotherUserComplete();
            newUser.Roles = InitOneRoleListComplete();

            Role role = InitOneRoleComplete();

            var roleLogicMock = new Mock<IRoleLogic>(MockBehavior.Strict);
            roleLogicMock.Setup(r => r.Get(It.IsAny<Guid>())).Returns(role);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Get(It.IsAny<Guid>())).Returns(oneUser);
            userRepositoryMock.Setup(u => u.Update(It.IsAny<User>(), It.IsAny<User>()));

            var userService = new UserLogic(userRepositoryMock.Object);
            var updatedUser = userService.Update(oneUser.Id, newUser,roleLogicMock.Object);
            Assert.AreEqual(updatedUser, oneUser);
        }
        [TestMethod]
        public void RemoveUserNotAdminOkTest()
        {
            User oneUser = InitOneUserComplete();

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var adminTokenRepositoryMock = new Mock<IAdminTokenRepository>(MockBehavior.Strict);

            userRepositoryMock.Setup(u => u.Remove(It.IsAny<User>()));
            userRepositoryMock.Setup(u => u.Save());
            
            var userService = new UserLogic(userRepositoryMock.Object);
            userService.Remove(oneUser);

            userRepositoryMock.VerifyAll();
        }
        [TestMethod]
        public void RemoveUserAdminOkTest()
        {
            User oneUser = InitAdminUserComplete();

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var adminTokenRepositoryMock = new Mock<IAdminTokenRepository>(MockBehavior.Strict);

            userRepositoryMock.Setup(u => u.Remove(It.IsAny<User>()));
            userRepositoryMock.Setup(u => u.Save());

            var userService = new UserLogic(userRepositoryMock.Object);
            userService.Remove(oneUser);

            userRepositoryMock.VerifyAll();
        }
        [ExpectedException(typeof(IncorrectRequestException))]
        [TestMethod]
        public void AddNullUserTest()
        {
            User oneUser = null;

            var roleLogicMock = new Mock<IRoleLogic>(MockBehavior.Strict);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var userService = new UserLogic(userRepositoryMock.Object);
            userService.Create(oneUser, roleLogicMock.Object);
        }
        [ExpectedException(typeof(IncorrectEmailException))]
        [TestMethod]
        public void CreateUserWithEmptyEmailOkTest()
        {
            User oneUser = InitOneUserComplete();
            oneUser.Email = "";

            var roleLogicMock = new Mock<IRoleLogic>(MockBehavior.Strict);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var userService = new UserLogic(userRepositoryMock.Object);
            userService.Create(oneUser, roleLogicMock.Object);
        }
        [ExpectedException(typeof(IncorrectPasswordException))]
        [TestMethod]
        public void CreateUserWithEmptyPasswordOkTest()
        {
            User oneUser = InitOneUserComplete();
            oneUser.Password = "";

            var roleLogicMock = new Mock<IRoleLogic>(MockBehavior.Strict);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var userService = new UserLogic(userRepositoryMock.Object);
            userService.Create(oneUser, roleLogicMock.Object);
        }
        [ExpectedException(typeof(IncorrectNameException))]
        [TestMethod]
        public void CreateUserWithEmptyNameOkTest()
        {
            User oneUser = InitOneUserComplete();
            oneUser.Name = "";

            var roleLogicMock = new Mock<IRoleLogic>(MockBehavior.Strict);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var userService = new UserLogic(userRepositoryMock.Object);
            userService.Create(oneUser, roleLogicMock.Object);
        }
        [ExpectedException(typeof(IncorrectDeliveryAddressException))]
        [TestMethod]
        public void CreateUserWithEmptyDeliveryAccessOkTest()
        {
            User oneUser = InitOneUserComplete();
            oneUser.DeliveryAdress = "";

            var roleLogicMock = new Mock<IRoleLogic>(MockBehavior.Strict);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var userService = new UserLogic(userRepositoryMock.Object);
            userService.Create(oneUser, roleLogicMock.Object);
        }
        [ExpectedException(typeof(IncorrectPasswordException))]
        [TestMethod]
        public void CreateUserWithInvalidPasswordOkTest()
        {
            User oneUser = InitOneUserComplete();
            oneUser.Password = "mal";

            var roleLogicMock = new Mock<IRoleLogic>(MockBehavior.Strict);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var userService = new UserLogic(userRepositoryMock.Object);
            userService.Create(oneUser, roleLogicMock.Object);
        }
        [ExpectedException(typeof(IncorrectEmailException))]
        [TestMethod]
        public void CreateUserWithInvalidEmailOkTest()
        {
            User oneUser = InitOneUserComplete();
            oneUser.Email = "mal";

            var roleLogicMock = new Mock<IRoleLogic>(MockBehavior.Strict);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var userService = new UserLogic(userRepositoryMock.Object);
            userService.Create(oneUser, roleLogicMock.Object);
        }
        [TestMethod]
        public void CreateUserWithEmptyRolesTest()
        {
            User oneUser = InitOneUserComplete();
            oneUser.Roles = null;

            Role oneRole = new Role();
            oneRole.Name = "Buyer";

            var roleRepositoryMock = new Mock<IRoleRepository>(MockBehavior.Strict);
            roleRepositoryMock.Setup(r => r.GetRoleByName(oneRole.Name)).Returns(oneRole);

            var roleLogicMock = new Mock<IRoleLogic>(MockBehavior.Strict);
            roleLogicMock.Setup(r => r.Get(It.IsAny<Guid>())).Returns(oneRole);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Add(oneUser));
            userRepositoryMock.Setup(u => u.Exists(oneUser)).Returns(false);
            userRepositoryMock.Setup(u => u.Save());

            var userService = new UserLogic(userRepositoryMock.Object, roleRepositoryMock.Object);
            userService.Create(oneUser, roleLogicMock.Object);
        }
    }
}
