using DataAccess.Contexts;
using Exceptions;
using Entities;
using TestSetUp;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Test
{
    [TestClass]
    public class UserRepositoryTest : TestSetUps
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
        public void GetUsersOkTest()
        {
            User oneUser = InitOneUserComplete();
            context.Set<User>().Add(oneUser);
            context.SaveChanges();

            UserRepository userRepository = new UserRepository(context);

            int actualResult = userRepository.Get().Count();
            Assert.AreEqual(1, actualResult);
        }
        [TestMethod]
        public void GetUserByIdOkTest()
        {
            User oneUser = InitOneUserComplete();
            context.Set<User>().Add(oneUser);
            context.SaveChanges();

            UserRepository userRepository = new UserRepository(context);

            User myUserById = userRepository.Get(oneUser.Id);
            Assert.AreEqual(myUserById, oneUser);
        }
        [TestMethod]
        public void AddUserToRepositoryOkTest()
        {
            User oneUser = InitOneUserComplete();

            UserRepository userRepository = new UserRepository(context);
            userRepository.Add(oneUser);
            context.SaveChanges();

            Assert.AreEqual(userRepository.Get().ToList().Count, 1);
        }
        [TestMethod]
        public void UserRepositoryExistsOkTest()
        {
            UserRepository userRepository = new UserRepository(context);

            User oneUser = InitOneUserComplete();

            context.Set<User>().Add(oneUser);
            context.SaveChanges();

            Assert.IsTrue(userRepository.Exists(oneUser));
        }
        [TestMethod]
        public void UpdateUserTest()
        {
            UserRepository userRepository = new UserRepository(context);

            User oneUser = InitOneUserComplete();
            User newUser = InitAnotherUserComplete();

            userRepository.Add(oneUser);
            userRepository.Save();


            userRepository.Update(oneUser,newUser);
            Assert.AreEqual(userRepository.Get(oneUser.Id), newUser);
        }
        [TestMethod]
        public void RemoveUserOkTest()
        {
            UserRepository userRepository = new UserRepository(context);

            User oneUser = InitOneUserComplete();

            userRepository.Add(oneUser);
            userRepository.Save();

            userRepository.Remove(oneUser);
            userRepository.Save();

            Assert.AreEqual(userRepository.Get().Count(), 0);
        }

        [ExpectedException(typeof(IncorrectRequestException))]
        [TestMethod]
        public void RemoveUserInvalidOkTest()
        {
            UserRepository userRepository = new UserRepository(context);

            User oneUser = InitOneUserComplete();

            userRepository.Save();
            userRepository.Remove(oneUser);
        }
    }
}
