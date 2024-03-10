using TestSetUp;

namespace Entities.Test
{
    [TestClass]
    public class RoleTest : TestSetUps
    {
        [TestMethod]
        public void CreateRoleOKTest() 
        {
            Role oneRole = new Role();
            Assert.IsNotNull(oneRole);
        }
        [TestMethod]
        public void CreateRoleWithNameOKTest()
        {
            Role oneRole = new Role();
            string name = "Comprador";
            oneRole.Name = name;
            Assert.AreEqual(oneRole.Name,name);
        }
        [TestMethod]
        public void CreateRoleWithPermissionListOKTest()
        {
            Role oneRole = new Role();
            List<Permission> permissions = InitOnePermissionListComplete();
            oneRole.Permissions = permissions;

            Assert.AreEqual(2, oneRole.Permissions.Count);
        }
    }
}
