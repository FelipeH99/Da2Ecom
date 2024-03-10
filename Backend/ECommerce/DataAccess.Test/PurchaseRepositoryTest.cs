using DataAccess.Contexts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Exceptions;
using TestSetUp;

namespace DataAccess.Test
{
    [TestClass]
    public class PurchaseRepositoryTest : TestSetUps
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
        public void GetPurchasesOKTest()
        {
            Purchase onePurchase = InitOnePurchaseComplete();
            context.Set<Purchase>().Add(onePurchase);
            context.SaveChanges();

            PurchaseRepository purchaseRepository = new PurchaseRepository(context);

            int actualResult = purchaseRepository.Get().Count();
            Assert.AreEqual(1, actualResult);
        }
        [TestMethod]
        public void GetPurchaseByIdOKTest()
        {
            Purchase onePurchase = InitOnePurchaseComplete();
            context.Set<Purchase>().Add(onePurchase);
            context.SaveChanges();

            PurchaseRepository purchaseRepository = new PurchaseRepository(context);

            Purchase purchasetById = purchaseRepository.Get(onePurchase.Id);
            Assert.AreEqual(onePurchase, purchasetById);
        }
        [TestMethod]
        public void GetPurchaseByUserIdOKTest()
        {
            Purchase onePurchase = InitOnePurchaseComplete();
            context.Set<Purchase>().Add(onePurchase);
            context.SaveChanges();

            PurchaseRepository purchaseRepository = new PurchaseRepository(context);

            List<Purchase> purchases = purchaseRepository.GetByUser(onePurchase.User.Id).ToList();
            Assert.AreEqual(onePurchase, purchases[0]);
        }
        [TestMethod]
        public void GetPurchaseByIdNotOKTest()
        {
            Purchase onePurchase = InitOnePurchaseComplete();

            PurchaseRepository purchaseRepository = new PurchaseRepository(context);

            Purchase purchasetById = purchaseRepository.Get(onePurchase.Id);
            Assert.IsNull(purchasetById);
        }
        [TestMethod]
        public void AddPurchaseOKTest()
        {
            Purchase onePurchase = InitOnePurchaseComplete();

            PurchaseRepository purchaseRepository = new PurchaseRepository(context);

            purchaseRepository.Add(onePurchase);
            context.SaveChanges();

            Assert.AreEqual(1, purchaseRepository.Get().ToList().Count);
        }
        [TestMethod]
        public void PurchaseRepositoryExistsOKTest()
        {
            PurchaseRepository purchaseRepository = new PurchaseRepository(context);
            Purchase onePurchase = InitOnePurchaseComplete();
            context.Set<Purchase>().Add(onePurchase);
            context.SaveChanges();

            Assert.IsTrue(purchaseRepository.Exists(onePurchase));
        }
        [TestMethod]
        public void UpdatePurchaseTest()
        {
            PurchaseRepository purchaseRepository = new PurchaseRepository(context);

            Purchase onePurchase = InitOnePurchaseComplete();
            Purchase anotherPurchase = InitAnotherPurchaseComplete();

            purchaseRepository.Add(onePurchase);
            purchaseRepository.Save();

            purchaseRepository.Update(onePurchase, anotherPurchase);
            Assert.AreEqual(purchaseRepository.Get(onePurchase.Id), anotherPurchase);
        }
        [TestMethod]
        public void RemovePurchaseOkTest()
        {
            PurchaseRepository purchaseRepository = new PurchaseRepository(context);

            Purchase onePurchase = InitOnePurchaseComplete();

            purchaseRepository.Add(onePurchase);
            purchaseRepository.Save();

            purchaseRepository.Remove(onePurchase);
            purchaseRepository.Save();

            Assert.AreEqual(purchaseRepository.Get().Count(), 0);
        }

        [ExpectedException(typeof(IncorrectRequestException))]
        [TestMethod]
        public void RemovePurchaseInvalidOkTest()
        {
            PurchaseRepository purchaseRepository = new PurchaseRepository(context);

            Purchase onePurchase = InitOnePurchaseComplete();

            purchaseRepository.Save();
            purchaseRepository.Remove(onePurchase);
        }
    }
}
