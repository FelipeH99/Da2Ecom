using DataAccess.Contexts;
using Entities;
using Microsoft.EntityFrameworkCore;
using TestSetUp;
using Exceptions;

namespace DataAccess.Test
{
    [TestClass]
    public class QuantityDiscountRepositoryTest : TestSetUps
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
        public void GetQuantityDiscountsOKTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();
            context.Set<QuantityDiscount>().Add(quantityDiscount);
            context.SaveChanges();

            QuantityDiscountRepository quantityDiscountRepository = new QuantityDiscountRepository(context);

            int actualResult = quantityDiscountRepository.Get().Count();
            Assert.AreEqual(1, actualResult);
        }

        [TestMethod]
        public void GetQuantityDiscountByIdOKTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();
            context.Set<QuantityDiscount>().Add(quantityDiscount);
            context.SaveChanges();

            QuantityDiscountRepository quantityDiscountRepository = new QuantityDiscountRepository(context);

            QuantityDiscount quantityDiscountById = quantityDiscountRepository.Get(quantityDiscount.Id);
            Assert.AreEqual(quantityDiscount, quantityDiscountById);
        }

        [TestMethod]
        public void GetQuantityDiscountByIdNotOKTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();


            QuantityDiscountRepository quantityDiscountRepository = new QuantityDiscountRepository(context);

            QuantityDiscount quantityDiscountById = quantityDiscountRepository.Get(quantityDiscount.Id);
            Assert.IsNull(quantityDiscountById);
        }
        [TestMethod]
        public void AddQuantityDiscountOKTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();

            QuantityDiscountRepository quantityDiscountRepository = new QuantityDiscountRepository(context);

            quantityDiscountRepository.Add(quantityDiscount);
            context.SaveChanges();

            Assert.AreEqual(1, quantityDiscountRepository.Get().ToList().Count);
        }
        [TestMethod]
        public void QuantityDiscountRepositoryExistsOKTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();

            QuantityDiscountRepository quantityDiscountRepository = new QuantityDiscountRepository(context);

            quantityDiscountRepository.Add(quantityDiscount);
            context.SaveChanges();

            Assert.IsTrue(quantityDiscountRepository.Exists(quantityDiscount));
        }
        [TestMethod]
        public void UpdateQuantityDiscountRepositoryOKTest()
        {
            QuantityDiscountRepository quantityDiscountRepository = new QuantityDiscountRepository(context);

            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();
            QuantityDiscount anotherQuantityDiscount = InitAnotherQuantityDiscountComplete();

            quantityDiscountRepository.Add(quantityDiscount);
            quantityDiscountRepository.Save();

            quantityDiscountRepository.Update(quantityDiscount, anotherQuantityDiscount);

            Assert.AreEqual(quantityDiscountRepository.Get(quantityDiscount.Id),
               anotherQuantityDiscount);
        }
        [TestMethod]
        public void DeleteQuantityDiscountTest() 
        {

            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();

            context.Set<QuantityDiscount>().Add(quantityDiscount);

            QuantityDiscountRepository quantityDiscountRepository = new QuantityDiscountRepository(context);
            quantityDiscountRepository.Save();
            quantityDiscountRepository.Remove(quantityDiscount);

            Assert.AreEqual(quantityDiscountRepository.Get().Count, 0);
        }

        [ExpectedException(typeof(IncorrectRequestException))]
        [TestMethod]
        public void DeleteInvalidOkTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();
            QuantityDiscount anotherQuantityDiscount = InitAnotherQuantityDiscountComplete();

            QuantityDiscountRepository quantityDiscountRepository = new QuantityDiscountRepository(context);
            quantityDiscountRepository.Add(quantityDiscount);
            quantityDiscountRepository.Remove(anotherQuantityDiscount);
        }
    }
}
