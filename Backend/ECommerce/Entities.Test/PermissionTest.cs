using TestSetUp;

namespace Entities.Test
{
    [TestClass]
    public class PermissionTest : TestSetUps
    {
        [TestMethod]
        public void CreatePermissionOKTest() 
        {
            Permission permission = new Permission();

            Assert.IsNotNull(permission);
        }
        [TestMethod]
        public void CreatePermissionWithNameOKTest()
        {
            Permission permission = new Permission();
            string name = "POST/Purchase";
            permission.Name = name;

            Assert.AreEqual(name, permission.Name);
        }
        [TestMethod]
        public void NotEqualsOKTest()
        {
            Permission permission = InitOnePermissionComplete();
            Permission anotherPermission = InitAnotherPermissionComplete();

            Assert.AreNotEqual(permission, anotherPermission);
        }
        [TestMethod]
        public void EqualsOKTest()
        {
            Permission permission = InitOnePermissionComplete();
            Permission anotherPermission = InitOnePermissionComplete();

            Assert.AreEqual(permission, anotherPermission);
        }
        [TestMethod]
        public void GetHashCodeOkTest()
        {
            Permission permission = InitOnePermissionComplete();
            var hash = permission.Name.GetHashCode();

            Assert.AreEqual(hash, permission.GetHashCode());
        }

    }
}
