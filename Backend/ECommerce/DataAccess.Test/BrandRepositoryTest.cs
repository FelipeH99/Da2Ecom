using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using TestSetUp;
using Entities;

namespace DataAccess.Test
{
    [TestClass]
    public class BrandRepositoryTest : TestSetUps
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
        public void GetBrandsOKTest()
        {
            Brand oneBrand = InitOneBrandComplete();
            context.Set<Brand>().Add(oneBrand);
            context.SaveChanges();

            BrandRepository brandRepository = new BrandRepository(context);

            int actualResult = brandRepository.Get().Count();
            Assert.AreEqual(1, actualResult);
        }
        [TestMethod]
        public void GetBrandByIdOKTest()
        {
            Brand oneBrand = InitOneBrandComplete();
            context.Set<Brand>().Add(oneBrand);
            context.SaveChanges();

            BrandRepository brandRepository = new BrandRepository(context);

            Brand brandById = brandRepository.Get(oneBrand.Id);
            Assert.AreEqual(oneBrand, brandById);
        }
        [TestMethod]
        public void GetBrandByNameOKTest()
        {
            Brand oneBrand = InitOneBrandComplete();
            context.Set<Brand>().Add(oneBrand);
            context.SaveChanges();

            BrandRepository brandRepository = new BrandRepository(context);

            Brand brandBNamed = brandRepository.GetByName(oneBrand.Name);
            Assert.AreEqual(oneBrand, brandBNamed);
        }
        [TestMethod]
        public void GetBrandByIdNotOKTest()
        {
            Brand oneBrand = InitOneBrandComplete();


            BrandRepository brandRepository = new BrandRepository(context);

            Brand brandById = brandRepository.Get(oneBrand.Id);
            Assert.IsNull(brandById);
        }
        [TestMethod]
        public void AddBrandOKTest()
        {
            Brand oneBrand = InitOneBrandComplete();

            BrandRepository brandRepository = new BrandRepository(context);
            brandRepository.Add(oneBrand);
            context.SaveChanges();

            Assert.AreEqual(1, brandRepository.Get().ToList().Count);
        }
        [TestMethod]
        public void BrandRepositoryExistsOKTest()
        {
            Brand oneBrand = InitOneBrandComplete();

            BrandRepository brandRepository = new BrandRepository(context);
            brandRepository.Add(oneBrand);
            context.SaveChanges();

            Assert.IsTrue(brandRepository.Exists(oneBrand));
        }
    }
}
