using DataAccess.Contexts;
using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using TestSetUp;

namespace DataAccess.Test
{
    [TestClass]
    public class BrandDiscountRepositoryTest : TestSetUps
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
        public void GetBrandDiscountsOKTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            context.Set<BrandDiscount>().Add(brandDiscount);
            context.SaveChanges();

            BrandDiscountRepository brandDiscountRepository = new BrandDiscountRepository(context);

            int actualResult = brandDiscountRepository.Get().Count();
            Assert.AreEqual(1, actualResult);
        }
        [TestMethod]
        public void GetBrandDiscountByIdOKTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            context.Set<BrandDiscount>().Add(brandDiscount);
            context.SaveChanges();

            BrandDiscountRepository brandDiscountRepository = new BrandDiscountRepository(context);

            BrandDiscount brandDiscountById = brandDiscountRepository.Get(brandDiscount.Id);
            Assert.AreEqual(brandDiscount, brandDiscountById);
        }

        [TestMethod]
        public void GetBrandDiscountByIdNotOKTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();

            BrandDiscountRepository brandDiscountRepository = new BrandDiscountRepository(context);

            BrandDiscount brandDiscountById = brandDiscountRepository.Get(brandDiscount.Id);
            Assert.IsNull(brandDiscountById);
        }
        [TestMethod]
        public void AddBrandDiscountOKTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();

            BrandDiscountRepository brandDiscountRepository = new BrandDiscountRepository(context);

            brandDiscountRepository.Add(brandDiscount);
            context.SaveChanges();

            Assert.AreEqual(1, brandDiscountRepository.Get().ToList().Count);
        }
        [TestMethod]
        public void BrandDiscountRepositoryExistsOKTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();

            BrandDiscountRepository brandDiscountRepository = new BrandDiscountRepository(context);

            brandDiscountRepository.Add(brandDiscount);
            context.SaveChanges();

            Assert.IsTrue(brandDiscountRepository.Exists(brandDiscount));
        }
        [TestMethod]
        public void UpdateBrandDiscountRepositoryOKTest()
        {
            BrandDiscountRepository brandDiscountRepository = new BrandDiscountRepository(context);

            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            BrandDiscount anotherBrandDiscount = InitAnotherBrandDiscountComplete();

            brandDiscountRepository.Add(brandDiscount);
            brandDiscountRepository.Save();

            brandDiscountRepository.Update(brandDiscount, anotherBrandDiscount);

            Assert.AreEqual(brandDiscountRepository.Get(brandDiscount.Id),
               anotherBrandDiscount);
        }
        [TestMethod]
        public void DeleteBrandDiscountTest()
        {

            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();

            context.Set<BrandDiscount>().Add(brandDiscount);

            BrandDiscountRepository brandDiscountRepository = new BrandDiscountRepository(context);
            brandDiscountRepository.Save();
            brandDiscountRepository.Remove(brandDiscount);

            Assert.AreEqual(brandDiscountRepository.Get().Count, 0);
        }

        [ExpectedException(typeof(IncorrectRequestException))]
        [TestMethod]
        public void DeleteInvalidOkTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            BrandDiscount anotherBrandDiscount = InitAnotherBrandDiscountComplete();

            BrandDiscountRepository brandDiscountRepository = new BrandDiscountRepository(context);
            brandDiscountRepository.Add(brandDiscount);
            brandDiscountRepository.Remove(anotherBrandDiscount);
        }
    }
}
