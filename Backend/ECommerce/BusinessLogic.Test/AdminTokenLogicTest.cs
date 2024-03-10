using Entities;
using TestSetUp;
using Exceptions;
using DataAccess.Interface;
using Moq;
using System.Security.Authentication;

namespace BusinessLogic.Test
{
    [TestClass]
    public class AdminTokenLogicTest : TestSetUps
    {
        [TestMethod]
        public void LoginAdminOkTest()
        {
            List<AdminToken> adminTokens = new List<AdminToken>();
            var adminTokenRepositoryMock = new Mock<IAdminTokenRepository>(MockBehavior.Strict);

            User user = InitAdminUserComplete();
            var userList = new List<User>();
            userList.Add(user);
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Get()).Returns(userList);
            var userService = new UserLogic(userRepositoryMock.Object);

            userRepositoryMock.Setup(u => u.Get()).Returns(() => new List<User> { user });
            userRepositoryMock.Setup(u => u.ExistsUserWithEmailAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            adminTokenRepositoryMock.Setup(a => a.GetOneAdminTokensByEmail(It.IsAny<string>())).Returns(adminTokens);
            adminTokenRepositoryMock.Setup(a => a.Add(It.IsAny<AdminToken>()));
            var adminTokenService = new AdminTokenLogic(adminTokenRepositoryMock.Object, userRepositoryMock.Object);

            var tokenResult = adminTokenService.Login(user.Email, user.Password, userService);
            adminTokenRepositoryMock.VerifyAll();
            Assert.IsNotNull(tokenResult);
        }
        [ExpectedException(typeof(AuthenticationException))]
        [TestMethod]
        public void LoginAdminWrongCredentialsOkTest()
        {
            List<AdminToken> adminTokens = new List<AdminToken>();
            var adminTokenRepositoryMock = new Mock<IAdminTokenRepository>(MockBehavior.Strict);

            User user = InitAdminUserComplete();
            var userList = new List<User>();
            userList.Add(user);
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Get()).Returns(userList);
            var userService = new UserLogic(userRepositoryMock.Object);

            userRepositoryMock.Setup(u => u.Get()).Returns(() => new List<User> { user });
            userRepositoryMock.Setup(u => u.ExistsUserWithEmailAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            userRepositoryMock.Setup(u => u.IsDeleted(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            adminTokenRepositoryMock.Setup(a => a.GetOneAdminTokensByEmail(It.IsAny<string>())).Returns(adminTokens);
            adminTokenRepositoryMock.Setup(a => a.Add(It.IsAny<AdminToken>()));
            var adminTokenService = new AdminTokenLogic(adminTokenRepositoryMock.Object, userRepositoryMock.Object);

            var tokenResult = adminTokenService.Login(user.Email, user.Password, userService);
        }
        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void LogInAdminAlreadyLoggedInOkTest()
        {

            var adminTokenRepositoryMock = new Mock<IAdminTokenRepository>(MockBehavior.Strict);

            User user = InitAdminUserComplete();

            AdminToken oneAdminToken = InitOneAdminTokenComplete(user);
            List<AdminToken> adminTokens = new List<AdminToken>();
            adminTokens.Add(oneAdminToken);

            var userList = new List<User>();
            userList.Add(user);
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Get()).Returns(userList);
            var userService = new UserLogic(userRepositoryMock.Object);

            userRepositoryMock.Setup(u => u.Get()).Returns(() => new List<User> { user });
            userRepositoryMock.Setup(u => u.ExistsUserWithEmailAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            adminTokenRepositoryMock.Setup(a => a.GetOneAdminTokensByEmail(It.IsAny<string>())).Returns(adminTokens);
            adminTokenRepositoryMock.Setup(a => a.Add(It.IsAny<AdminToken>()));
            var adminTokenService = new AdminTokenLogic(adminTokenRepositoryMock.Object, userRepositoryMock.Object);

            var tokenResult = adminTokenService.Login(user.Email, user.Password, userService);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void LogInAdminWrongPasswordOkTest()
        {
            User user = InitAdminUserComplete();
            var userList = new List<User>();
            userList.Add(user);
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Get()).Returns(userList);
            var userService = new UserLogic(userRepositoryMock.Object);
            
            user.Password = "mal";
            AdminToken oneAdminToken = InitOneAdminTokenComplete(user);
            List<AdminToken> adminTokens = new List<AdminToken>();
            adminTokens.Add(oneAdminToken);
            var adminTokenRepositoryMock = new Mock<IAdminTokenRepository>(MockBehavior.Strict);

            userRepositoryMock.Setup(u => u.Get()).Returns(() => new List<User> { user });
            userRepositoryMock.Setup(u => u.ExistsUserWithEmailAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            adminTokenRepositoryMock.Setup(a => a.GetOneAdminTokensByEmail(It.IsAny<string>())).Returns(adminTokens);
            adminTokenRepositoryMock.Setup(a => a.Add(It.IsAny<AdminToken>()));
            var adminTokenService = new AdminTokenLogic(adminTokenRepositoryMock.Object, userRepositoryMock.Object);

            var tokenResult = adminTokenService.Login(user.Email, user.Password, userService);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectEmailException))]
        public void LogInAdminWrongEmailOkTest()
        {
            User user = InitAdminUserComplete();
            var userList = new List<User>();
            userList.Add(user);
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Get()).Returns(userList);
            var userService = new UserLogic(userRepositoryMock.Object);
            
            user.Email = "mal";
            AdminToken oneAdminToken = InitOneAdminTokenComplete(user);
            List<AdminToken> adminTokens = new List<AdminToken>();
            adminTokens.Add(oneAdminToken);
            var adminTokenRepositoryMock = new Mock<IAdminTokenRepository>(MockBehavior.Strict);

            userRepositoryMock.Setup(u => u.Get()).Returns(() => new List<User> { user });
            userRepositoryMock.Setup(u => u.ExistsUserWithEmailAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            adminTokenRepositoryMock.Setup(a => a.GetOneAdminTokensByEmail(It.IsAny<string>())).Returns(adminTokens);
            adminTokenRepositoryMock.Setup(a => a.Add(It.IsAny<AdminToken>()));
            var adminTokenService = new AdminTokenLogic(adminTokenRepositoryMock.Object, userRepositoryMock.Object);

            var tokenResult = adminTokenService.Login(user.Email, user.Password, userService);
        }
        [TestMethod]
        public void IsLoggedTestOk()
        {
            User user = InitAdminUserComplete();
            var userList = new List<User>();
            userList.Add(user);
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Get()).Returns(userList);
            var userService = new UserLogic(userRepositoryMock.Object);
            
            AdminToken adminToken = InitOneAdminTokenComplete(user);
            List<AdminToken> adminTokensLoggedIn = new List<AdminToken>();
            List<AdminToken> adminTokens = new List<AdminToken>();
            adminTokensLoggedIn.Add(adminToken);

            var adminTokenRepositoryMock = new Mock<IAdminTokenRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Get()).Returns(() => new List<User> { user });
            userRepositoryMock.Setup(u => u.ExistsUserWithEmailAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            adminTokenRepositoryMock.Setup(a => a.Get()).Returns(adminTokensLoggedIn);
            var adminTokenService = new AdminTokenLogic(adminTokenRepositoryMock.Object, userRepositoryMock.Object);

            var tokenResult = adminTokenService.GetToken(user);
            Assert.IsTrue(adminTokenService.IsLogged(tokenResult));
            adminTokenRepositoryMock.VerifyAll();
        }

    }
}
