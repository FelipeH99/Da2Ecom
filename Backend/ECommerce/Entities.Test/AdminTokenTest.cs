using TestSetUp;

namespace Entities.Test
{
    [TestClass]
    public class AdminTokenTest : TestSetUps
    {
        [TestMethod]
        public void CreateAdminTokenOkTest()
        {
            AdminToken adminToken = new AdminToken();

            Assert.IsNotNull(adminToken);
        }
        [TestMethod]
        public void CreateAdminTokenWithUserOKTest() 
        {
            AdminToken adminToken = new AdminToken();
            User oneUser = InitAdminUserComplete();
            adminToken.User = oneUser;

            Assert.AreEqual(oneUser, adminToken.User);
        }
        [TestMethod]
        public void EqualsOkTest()
        {
            User oneUser = InitAdminUserComplete();
            AdminToken adminToken = new AdminToken();
            adminToken.User = oneUser;
            AdminToken secondAdminToken = new AdminToken();
            secondAdminToken.User = oneUser;

            Assert.IsTrue(adminToken.Equals(secondAdminToken));
        }
        [TestMethod]
        public void EqualsWithNullObjectTest()
        {
            User oneUser = InitAdminUserComplete();
            AdminToken oneAdminToken = new AdminToken();
            oneAdminToken.User = oneUser;
            AdminToken secondAdminToken = null;

            Assert.IsFalse(oneAdminToken.Equals(secondAdminToken));
        }
        [TestMethod]
        public void EqualsOkOnlySameEmailTest()
        {
            User oneUser = InitOneUserComplete();
            User secondUser = InitAdminUserComplete();

            string email = "new@email.com";
            oneUser.Email = email;
            secondUser.Email = email;

            AdminToken oneAdminToken = new AdminToken();
            oneAdminToken.User = oneUser;
            AdminToken secondAdminToken = new AdminToken();
            secondAdminToken.User = secondUser;

            Assert.IsTrue(oneAdminToken.Equals(secondAdminToken));
        }

        [TestMethod]
        public void NotEqualsDifferentEditorTokens()
        {
            User oneUser = InitOneUserComplete();
            User secondUser = InitAdminUserComplete();

            AdminToken oneAdminToken = new AdminToken();
            oneAdminToken.User = oneUser;
            AdminToken secondAdminToken = new AdminToken();
            secondAdminToken.User = secondUser;

            Assert.IsFalse(oneAdminToken.Equals(secondAdminToken));
        }
        [TestMethod]
        public void GetHashCodeOkTest()
        {
            User oneUser = InitAdminUserComplete();
            AdminToken oneAdminToken = new AdminToken();
            oneAdminToken.User = oneUser;

            var oneId = oneAdminToken.Id;

            Assert.AreEqual(oneId.GetHashCode(), oneAdminToken.GetHashCode());
        }
    }
}
