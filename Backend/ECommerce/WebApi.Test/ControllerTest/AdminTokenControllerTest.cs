using BusinessLogic;
using BusinessLogic.Interface;
using DataAccess.Interface;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestSetUp;
using WebAPI.Controllers;
using WebAPI.Models;

namespace WebApi.Test.ControllerTest
{
    [TestClass]
    public class AdminTokenControllerTest : TestSetUps
    {
        [TestMethod]
        public void LogInOkTest()
        {
            User user = InitAdminUserComplete();
            var userList = new List<User>();
            userList.Add(user);
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Get()).Returns(userList);
            var userService = new UserLogic(userRepositoryMock.Object);

            AdminToken oneAdminToken = InitOneAdminTokenComplete(user);

            AdminTokenModel adminTokenModel = AdminTokenModel.ToModel(oneAdminToken);

            var adminTokenServiceMock = new Mock<IAdminTokenLogic>(MockBehavior.Strict);
            adminTokenServiceMock.Setup(aT => aT.Login(user.Email, user.Password,userService)).Returns(oneAdminToken);

            var adminTokenController = new AdminTokenController(adminTokenServiceMock.Object);

            var result = adminTokenController.LogIn(adminTokenModel,userService);
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            var expected = new
            {
                mensaje = "Se ha logeado correctamente.",
                token = oneAdminToken.Id
            };
            adminTokenServiceMock.VerifyAll();
            Assert.AreEqual(value.ToString(), expected.ToString());
        }
        [TestMethod]
        public void LogoutOkTest()
        {
            User oneUser = InitAdminUserComplete();
            AdminToken oneAdminToken = InitOneAdminTokenComplete(oneUser);

            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);

            var adminTokenServiceMock = new Mock<IAdminTokenLogic>(MockBehavior.Strict);
            adminTokenServiceMock.Setup(aT => aT.IsLogged(oneAdminToken.Id)).Returns(true);
            adminTokenServiceMock.Setup(aT => aT.Logout(oneAdminToken.Id));

            var adminTokenController = new AdminTokenController(adminTokenServiceMock.Object);

            var result = adminTokenController.LogOut(oneAdminToken.Id);
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            var expected = new
            {
                mensaje = "Se ha deslogueado correctamente.",
            };
            adminTokenServiceMock.VerifyAll();
            Assert.AreEqual(value.ToString(), expected.ToString());
        }
        [TestMethod]
        public void LogoutNotAlreadyLoginOkTest()
        {
            User oneUser = InitAdminUserComplete();
            AdminToken oneAdminToken = InitOneAdminTokenComplete(oneUser);
            var adTList = new List<AdminToken>();
            adTList.Add(oneAdminToken);

            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);

            var adminTokenRepository = new Mock<IAdminTokenRepository>(MockBehavior.Strict);
            adminTokenRepository.Setup(aT => aT.Get()).Returns(adTList);

            var adminTokenServiceMock = new Mock<IAdminTokenLogic>(MockBehavior.Strict);
            adminTokenServiceMock.Setup(aT => aT.IsLogged(oneAdminToken.Id)).Returns(false);

            var adminTokenController = new AdminTokenController(adminTokenServiceMock.Object);

            var result = adminTokenController.LogOut(oneAdminToken.Id);
            var okResult = result as ObjectResult;
            var value = okResult.Value;
            adminTokenServiceMock.VerifyAll();
            Assert.AreEqual(value, "No hay usuarios logueados en el sistema con ese correo electronico.");
        }
    }
}
