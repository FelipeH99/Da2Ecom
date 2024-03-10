using TestSetUp;
using Entities;
using DataAccess.Interface;
using Moq;

namespace BusinessLogic.Test
{
    [TestClass]
    public class RoleLogicTest : TestSetUps
    {
        [TestMethod]
        public void GetRolesOkTest()
        {
            List<Role> roleList = new List<Role>();
            Role oneRole= InitOneRoleComplete();
            roleList.Add(oneRole);

            var roleRepositoryMock = new Mock<IRoleRepository>(MockBehavior.Strict);
            roleRepositoryMock.Setup(r => r.Get()).Returns(roleList);
            var roleService = new RoleLogic(roleRepositoryMock.Object);

            var roleResult = roleService.Get().ToList<Role>();
            roleRepositoryMock.VerifyAll();
            Assert.IsTrue(roleResult.Any(r => r.Id == oneRole.Id));
        }
        [TestMethod]
        public void GetOneRoleByIdOkTest()
        {
            Role oneRole = InitOneRoleComplete();

            var roleRepositoryMock = new Mock<IRoleRepository>(MockBehavior.Strict);
            roleRepositoryMock.Setup(r => r.Get(oneRole.Id)).Returns(oneRole);
            var roleService = new RoleLogic(roleRepositoryMock.Object);

            var roleResult = roleService.Get(oneRole.Id);
            roleRepositoryMock.VerifyAll();
            Assert.AreEqual(roleResult.Id, oneRole.Id);
        }
    }
}
