using DataAccess.Contexts;
using Entities;
using Microsoft.EntityFrameworkCore;
using TestSetUp;

namespace DataAccess.Test
{
    [TestClass]
    public class RoleRepositoryTest : TestSetUps
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
        public void GetRolesOKTest()
        {
            Role oneRole = InitOneRoleComplete();
            context.Set<Role>().Add(oneRole);
            context.SaveChanges();

            RoleRepository roleRepository = new RoleRepository(context);

            int actualResult = roleRepository.Get().Count();
            Assert.AreEqual(1, actualResult);
        }
        [TestMethod]
        public void GetRoleByIdOKTest()
        {
            Role oneRole = InitOneRoleComplete();
            context.Set<Role>().Add(oneRole);
            context.SaveChanges();

            RoleRepository roleRepository = new RoleRepository(context);

            Role roleById = roleRepository.Get(oneRole.Id);
            Assert.AreEqual(oneRole, roleById);
        }
        [TestMethod]
        public void GetRoleByNameOKTest() 
        {
            Role oneRole = InitOneRoleComplete();
            context.Set<Role>().Add(oneRole);
            context.SaveChanges();

            RoleRepository roleRepository = new RoleRepository(context);

            Role roleByName = roleRepository.GetRoleByName(oneRole.Name);
            Assert.AreEqual(oneRole, roleByName);
        }
    }
}
