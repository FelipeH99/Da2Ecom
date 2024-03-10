using TestSetUp;
using Entities;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Test
{
    [TestClass]
    public class AdminTokenRepositoryTest : TestSetUps
    {
        private DbContextOptions options;
        private DbContext context;

        [TestInitialize]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "ECommerce").Options;
            context = new DataBaseContext(options);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            context.Database.EnsureDeleted();
        }
        [TestMethod]
        public void GetAdminTokenOkTest()
        {
            User oneUser = InitAdminUserComplete();
            User secondUser = InitOneUserComplete();
            context.Set<User>().Add(oneUser);
            context.Set<User>().Add(secondUser);
            context.SaveChanges();

            AdminToken oneAdminToken = InitOneAdminTokenComplete(oneUser);
            AdminToken secondAdminToken = InitOneAdminTokenComplete(secondUser);
            context.Set<AdminToken>().Add(oneAdminToken);
            context.Set<AdminToken>().Add(secondAdminToken);
            context.SaveChanges();

            AdminTokenRepository adminTokenRepository = new AdminTokenRepository(context);

            bool result = adminTokenRepository.Get().Contains(oneAdminToken)
            && adminTokenRepository.Get().Contains(secondAdminToken);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void GetIdOkTest()
        {
            User oneUser = InitAdminUserComplete();
            context.Set<User>().Add(oneUser);
            AdminToken oneAdminToken = InitOneAdminTokenComplete(oneUser);
            context.Set<AdminToken>().Add(oneAdminToken);
            context.SaveChanges();

            AdminTokenRepository adminTokenRepository = new AdminTokenRepository(context);
            var result = adminTokenRepository.GetAdminTokenById(oneAdminToken.Id);

            Assert.AreEqual(result, oneAdminToken);
        }
        [TestMethod]
        public void AddAdminTokenToRepositoryOkTest()
        {
            UserRepository userRepository = new  UserRepository(context);
            User oneUser = InitAdminUserComplete();
            userRepository.Add(oneUser);
            context.SaveChanges();

            AdminToken oneAdminToken = InitOneAdminTokenComplete(oneUser);
            AdminTokenRepository adminTokenRepository = new AdminTokenRepository(context);
            adminTokenRepository.Add(oneAdminToken);
            context.SaveChanges();

            int actualResult = adminTokenRepository.Get().Count;
            Assert.AreEqual(1, actualResult);
        }
        [TestMethod]
        public void CheckNotExistingAdminTokenInListTest()
        {
            UserRepository userRepository = new UserRepository(context);
            User oneUser = InitAdminUserComplete();
            userRepository.Add(oneUser);
            context.SaveChanges();

            AdminToken oneAdminToken = InitOneAdminTokenComplete(oneUser);

            AdminTokenRepository adminTokenRepository = new AdminTokenRepository(context);

            Assert.IsFalse(adminTokenRepository.Exists(oneAdminToken.Id));
        }
        [TestMethod]
        public void DeleteAdminTokensByUserTest()
        {
            User oneUser = InitAdminUserComplete();
            AdminToken oneAdminToken = InitOneAdminTokenComplete(oneUser);
            context.Set<AdminToken>().Add(oneAdminToken);
            context.SaveChanges();

            AdminTokenRepository adminTokenRepository = new AdminTokenRepository(context);
            adminTokenRepository.RemoveAdminTokensByUser(oneAdminToken.User);

            Assert.AreEqual(adminTokenRepository.Get().Count, 0);
        }
        [TestMethod]
        public void GetAdminTokensByEmailOkTest()
        {
            User oneUser = InitAdminUserComplete();
            User secondUser = InitOneUserComplete();
            context.Set<User>().Add(oneUser);
            context.Set<User>().Add(secondUser);
            context.SaveChanges();

            AdminToken oneAdminToken = InitOneAdminTokenComplete(oneUser);
            AdminToken secondAdminToken = InitOneAdminTokenComplete(secondUser);
            context.Set<AdminToken>().Add(oneAdminToken);
            context.Set<AdminToken>().Add(secondAdminToken);
            context.SaveChanges();

            AdminTokenRepository adminTokenRepository = new AdminTokenRepository(context);
            var result = adminTokenRepository.GetOneAdminTokensByEmail(oneUser.Email).Count;

            Assert.IsTrue(result == 1);
        }
    }
}
