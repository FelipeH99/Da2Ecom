using TestSetUp;

namespace Entities.Test
{
    [TestClass]
    public class UserTests : TestSetUps
    {
        [TestMethod]
        public void CreateUser()
        {
            var user = new User();

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void CreateNameTest()
        {
            var user = new User()
            {
                Name = "user"
            };

            Assert.AreEqual(user.Name, "user");
        }
        [TestMethod]
        public void CreateEmailTest()
        {
            var user = new User()
            {
                Email = "user@mail.com"
            };

            Assert.AreEqual(user.Email, "user@mail.com");
        }
        [TestMethod]
        public void CreateDeliveryAdressTest()
        {
            var user = new User()
            {
                DeliveryAdress = "userAdress 123"
            };

            Assert.AreEqual(user.DeliveryAdress, "userAdress 123");
        }

        [TestMethod]
        public void CreatePasswordTest()
        {
            var user = new User()
            {
                Password = "Pass123"
            };

            Assert.AreEqual(user.Password, "Pass123");
        }
        [TestMethod]
        public void CreateUserDeletedOKTest() 
        {
            User user = new User();
            bool deleted = true;
            user.IsDeleted = deleted;

            Assert.AreEqual(deleted,user.IsDeleted);
        }
        [TestMethod]
        public void EqualsOKTest()
        {
            User oneUser = InitOneUserComplete();
            User anotherUser = InitOneUserComplete();

            Assert.AreEqual(oneUser, anotherUser);
        }
        [TestMethod]
        public void GetHashCodeOkTest()
        {
            User oneUser = InitOneUserComplete();
            var hash = oneUser.Name.GetHashCode() + oneUser.Email.GetHashCode();

            Assert.AreEqual(hash, oneUser.GetHashCode());
        }
        [TestMethod]
        public void CreateWithRolesOKTest()
        {
            User user = InitOneUserComplete();

            List<Role> roles = InitOneRoleListComplete();

            user.Roles = roles;


            Assert.AreEqual(1, user.Roles.Count);
        }
    }

}