using DataAccess.Contexts;
using Entities;
using Microsoft.EntityFrameworkCore;
using TestSetUp;

namespace DataAccess.Test
{
    [TestClass]
    public class PaymentMethodRepositoryTest : TestSetUps
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
        public void GetPaymentMethodsOkTest()
        {
            PaymentMethod onePaymentMethod = InitOnePaymentMethod();
            context.Set<PaymentMethod>().Add(onePaymentMethod);
            context.SaveChanges();

            PaymentMethodRepository paymentMethodRepository = new PaymentMethodRepository(context);

            int actualResult = paymentMethodRepository.Get().Count();
            Assert.AreEqual(1, actualResult);
        }
        [TestMethod]
        public void GetPaymentMethodsByIdOkTest()
        {
            PaymentMethod onePaymentMethod = InitOnePaymentMethod();
            context.Set<PaymentMethod>().Add(onePaymentMethod);
            context.SaveChanges();

            PaymentMethodRepository paymentMethodRepository = new PaymentMethodRepository(context);

            PaymentMethod paymentById = paymentMethodRepository.Get(onePaymentMethod.Id);
            Assert.AreEqual(paymentById, onePaymentMethod);
        }
    }
}
