using TestSetUp;
using Exceptions;
using DataAccess.Contexts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Test
{
    [TestClass]
    public class PercentageDiscountRepositoryTestcs : TestSetUps
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
        public void GetPercentageDiscountsOKTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();
            context.Set<PercentageDiscount>().Add(percentageDiscount);
            context.SaveChanges();

            PercentageDiscountRepository percentageDiscountRepository = new PercentageDiscountRepository(context);

            int actualResult = percentageDiscountRepository.Get().Count();
            Assert.AreEqual(1, actualResult);
        }
        [TestMethod]
        public void GetPercentageDiscountByIdOKTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();
            context.Set<PercentageDiscount>().Add(percentageDiscount);
            context.SaveChanges();

            PercentageDiscountRepository percentageDiscountRepository = new PercentageDiscountRepository(context);

            PercentageDiscount percentageDiscountById = percentageDiscountRepository.Get(percentageDiscount.Id);
            Assert.AreEqual(percentageDiscount, percentageDiscountById);
        }
        [TestMethod]
        public void GetPercentageDiscountByIdNotOKTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();


            PercentageDiscountRepository percentageDiscountRepository = new PercentageDiscountRepository(context);

            PercentageDiscount percentageDiscountById = percentageDiscountRepository.Get(percentageDiscount.Id);
            Assert.IsNull(percentageDiscountById);
        }
        [TestMethod]
        public void AddPercentageDiscountOKTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();

            PercentageDiscountRepository percentageDiscountRepository = new PercentageDiscountRepository(context);

            percentageDiscountRepository.Add(percentageDiscount);
            context.SaveChanges();

            Assert.AreEqual(1, percentageDiscountRepository.Get().ToList().Count);
        }
        [TestMethod]
        public void PercentageDiscountRepositoryExistsOKTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();

            PercentageDiscountRepository percentageDiscountRepository = new PercentageDiscountRepository(context);

            percentageDiscountRepository.Add(percentageDiscount);
            context.SaveChanges();

            Assert.IsTrue(percentageDiscountRepository.Exists(percentageDiscount));
        }

        [TestMethod]
        public void UpdatePercentageDiscountRepositoryOKTest()
        {
            PercentageDiscountRepository percentageDiscountRepository = new PercentageDiscountRepository(context);

            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();
            PercentageDiscount anotherPercentageDiscount = InitAnotherPercentageDiscountComplete();

            percentageDiscountRepository.Add(percentageDiscount);
            percentageDiscountRepository.Save();

            percentageDiscountRepository.Update(percentageDiscount, anotherPercentageDiscount);

            Assert.AreEqual(percentageDiscountRepository.Get(percentageDiscount.Id),
               anotherPercentageDiscount);
        }
        [TestMethod]
        public void DeletePercentageDiscountTest()
        {

            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();

            context.Set<PercentageDiscount>().Add(percentageDiscount);

            PercentageDiscountRepository percentageDiscountRepository = new PercentageDiscountRepository(context);
            percentageDiscountRepository.Save();
            percentageDiscountRepository.Remove(percentageDiscount);

            Assert.AreEqual(percentageDiscountRepository.Get().Count, 0);
        }

        [ExpectedException(typeof(IncorrectRequestException))]
        [TestMethod]
        public void DeleteInvalidOkTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();
            PercentageDiscount anotherPercentageDiscount = InitAnotherPercentageDiscountComplete();

            PercentageDiscountRepository percentageDiscountRepository = new PercentageDiscountRepository(context);
            percentageDiscountRepository.Add(percentageDiscount);
            percentageDiscountRepository.Remove(anotherPercentageDiscount);
        }
    }
}
