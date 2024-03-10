using BusinessLogic.Interface;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestSetUp;
using WebAPI.Controllers;
using WebAPI.Models;

namespace WebApi.Test.ControllerTest
{
    [TestClass]
    public class UserControllerTest : TestSetUps
    {
        [TestMethod]
        public void GetOkTest()
        {
            User oneUser = InitOneUserComplete();
            User secondUser = InitOneUserComplete();
            List<User> users = new List<User>();
            users.Add(oneUser);
            users.Add(secondUser);

            List<UserModel> userModelResultList = new List<UserModel>();
            userModelResultList.Add(UserModel.ToModel(oneUser));
            userModelResultList.Add(UserModel.ToModel(secondUser));

            var userServiceMock = new Mock<IUserLogic>(MockBehavior.Strict);
            userServiceMock.Setup(u => u.Get()).Returns(users);
            var userController = new UserController(userServiceMock.Object);

            var result = userController.Get();
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            IEnumerable<UserModel> valueEnumerable = value as IEnumerable<UserModel>;
            List<UserModel> userModelList = valueEnumerable.ToList();
            userServiceMock.VerifyAll();
            Assert.IsTrue(userModelResultList.SequenceEqual(userModelList));
        }
        [TestMethod]
        public void GetInvalidOkTest()
        {
            User oneUser = InitOneUserComplete();
            List<User> users = new List<User>();
            List<UserModel> adminModelResultList = new List<UserModel>();

            var userServiceMock = new Mock<IUserLogic>(MockBehavior.Strict);
            userServiceMock.Setup(a => a.Get()).Returns(users);
            var userController = new UserController(userServiceMock.Object);

            var result = userController.Get();
            var okResult = result as ObjectResult;
            var value = okResult.Value;
            userServiceMock.VerifyAll();
            Assert.AreEqual(value, "No hay usuarios en el sistema.");
        }
        [TestMethod]
        public void GetIdOkTest()
        {
            User oneUser = InitOneUserComplete();

            var userServiceMock = new Mock<IUserLogic>(MockBehavior.Strict);
            userServiceMock.Setup(a => a.Get(It.IsAny<Guid>())).Returns(oneUser);
            var userController = new UserController(userServiceMock.Object);

            var result = userController.Get(oneUser.Id);
            var okResult = result as ObjectResult;
            var value = okResult.Value as UserModel;
            userServiceMock.VerifyAll();
            Assert.AreEqual(value, UserModel.ToModel(oneUser));
        }
        [TestMethod]
        public void GetIdInvalidOkTest()
        {
            User oneUser = null;
            Guid myGuid = Guid.NewGuid();
            var userServiceMock = new Mock<IUserLogic>(MockBehavior.Strict);
            userServiceMock.Setup(a => a.Get(It.IsAny<Guid>())).Returns(oneUser);
            var userController = new UserController(userServiceMock.Object);

            var result = userController.Get(myGuid);
            var okResult = result as ObjectResult;
            var value = okResult.Value;
            userServiceMock.VerifyAll();
            Assert.AreEqual(value, "No existe el usuario con Id: " + myGuid);
        }
        [TestMethod]
        public void PutOkTest()
        {
            User oneUser = InitOneUserComplete();

            var roleLogicMock = new Mock<IRoleLogic>(MockBehavior.Strict);

            var userServiceMock = new Mock<IUserLogic>(MockBehavior.Strict);
            userServiceMock.Setup(a => a.Get(It.IsAny<Guid>())).Returns(oneUser);
            userServiceMock.Setup(a => a.Update(It.IsAny<Guid>(), It.IsAny<User>(),roleLogicMock.Object)).Returns(oneUser);

            var userController = new UserController(userServiceMock.Object);
            var result = userController.Put(oneUser.Id, UserModel.ToModel(oneUser),roleLogicMock.Object);
            var okResult = result as ObjectResult;
            var value = okResult.Value;
            userServiceMock.VerifyAll();
            Assert.AreEqual(value, "Se cambio el usuario correctamente.");
        }
        [TestMethod]
        public void PostOkTest()
        {
            User oneUser = InitOneUserComplete();
            var userServiceMock = new Mock<IUserLogic>(MockBehavior.Strict);

            var roleLogicMock = new Mock<IRoleLogic>();

            userServiceMock.Setup(a => a.Create(It.IsAny<User>(), roleLogicMock.Object)).Returns(oneUser);
            var userController = new UserController(userServiceMock.Object);
            var result = userController.Post(UserModel.ToModel(oneUser), roleLogicMock.Object);

            var createdResult = result as ObjectResult;
            var value = createdResult.Value;
            userServiceMock.VerifyAll();
            Assert.AreEqual(value, UserModel.ToModel(oneUser));
        }
        [TestMethod]
        public void DeleteOkTest()
        {
            User oneUser = InitOneUserComplete();

            var userServiceMock = new Mock<IUserLogic>(MockBehavior.Strict);
            userServiceMock.Setup(a => a.Get(It.IsAny<Guid>())).Returns(oneUser);
            userServiceMock.Setup(a => a.Remove(It.IsAny<User>()));

            var userController = new UserController(userServiceMock.Object);

            var result = userController.Delete(oneUser.Id);
            var okResult = result as ObjectResult;
            var value = okResult.Value;
            userServiceMock.VerifyAll();
            Assert.AreEqual(value, "Se elimino el usuario con el email: " + oneUser.Email);
        }
        [TestMethod]
        public void DeleteInvalidOkTest()
        {
            User oneUser = null;
            Guid myGuid = Guid.NewGuid();

            var userServiceMock = new Mock<IUserLogic>(MockBehavior.Strict);
            userServiceMock.Setup(a => a.Get(It.IsAny<Guid>())).Returns(oneUser);
            var userController = new UserController(userServiceMock.Object);

            var result = userController.Delete(myGuid);
            var okResult = result as ObjectResult;
            var value = okResult.Value;
            userServiceMock.VerifyAll();
            Assert.AreEqual(value, "No existe el usuario con Id: " + myGuid);
        }
    }
}
