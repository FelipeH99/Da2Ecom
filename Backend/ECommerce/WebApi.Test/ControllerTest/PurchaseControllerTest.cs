using BusinessLogic.Interface;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Reflection;
using TestSetUp;
using WebAPI.Controllers;
using WebAPI.Models.Read;
using WebAPI.Models.Write;

namespace WebApi.Test.ControllerTest
{
    [TestClass]
    public class PurchaseControllerTest : TestSetUps
    {
        [TestMethod]
        public void GetAllOkTest()
        {
            Purchase onePurchase = InitOnePurchaseComplete();
            List<Purchase> purchases = new List<Purchase>() { onePurchase };

            PurchaseModelRead purchaseModelRead = InitOnePurchaseModelRead(onePurchase);

            List<PurchaseModelRead> purchaseModelResultList = new List<PurchaseModelRead>();
            purchaseModelResultList.Add(purchaseModelRead);

            var purchaseServiceMock = new Mock<IPurchaseLogic>(MockBehavior.Strict);
            purchaseServiceMock.Setup(p => p.Get()).Returns(purchases);

            var purchaseController = new PurchaseController(purchaseServiceMock.Object);

            var result = purchaseController.Get();
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            IEnumerable<PurchaseModelRead> valueEnumerable = value as IEnumerable<PurchaseModelRead>;
            List<PurchaseModelRead> purchaseModelList = valueEnumerable.ToList();
            purchaseServiceMock.VerifyAll();
            Assert.IsTrue(purchaseModelResultList.SequenceEqual(purchaseModelList));
        }
        [TestMethod]
        public void GetByUserIdOkTest()
        {
            Purchase purchase = InitOnePurchaseComplete();
            List<Purchase> purchases = new List<Purchase>() { purchase};

            PurchaseModelRead purchaseModelRead = InitOnePurchaseModelRead(purchase);

            List<PurchaseModelRead> purchaseModelResultList = new List<PurchaseModelRead>();
            purchaseModelResultList.Add(purchaseModelRead);

            var purchaseServiceMock = new Mock<IPurchaseLogic>(MockBehavior.Strict);
            purchaseServiceMock.Setup(p => p.GetByUserId(It.IsAny<Guid>())).Returns(purchases);

            var purchaseController = new PurchaseController(purchaseServiceMock.Object);

            var result = purchaseController.Get(purchase.User.Id);
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            IEnumerable<PurchaseModelRead> valueEnumerable = value as IEnumerable<PurchaseModelRead>;
            List<PurchaseModelRead> purchaseModelList = valueEnumerable.ToList();
            purchaseServiceMock.VerifyAll();
            Assert.IsTrue(purchaseModelResultList.SequenceEqual(purchaseModelList));
        }
        [TestMethod]
        public void PostOkTest()
        {
            Purchase purchase = InitOnePurchaseComplete();
            Product oneProduct = InitOneProductComplete();
            purchase.PaymentMethod.Id = Guid.NewGuid();

            List<PurchaseModelCreatedRead> purchaseModelCreateReadResultList = new List<PurchaseModelCreatedRead>();
            purchaseModelCreateReadResultList.Add(PurchaseModelCreatedRead.ToModel(purchase));
            PurchaseModelWrite purchaseModelWrite = InitOnePurchaseModelWriteComplete(purchase);

            var productServiceMock = new Mock<IProductLogic>(MockBehavior.Strict);
            productServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(oneProduct);

            var paymentMethodService = new Mock<IPaymentMethodLogic>(MockBehavior.Strict);

            var reflectionLogic = new Mock<IReflectionImplementation>(MockBehavior.Strict);
            reflectionLogic.Setup(r => r.LoadDll(purchase.Products));

            var discountServiceMock = new Mock<IDiscountLogic>(MockBehavior.Strict);

            var purchaseServiceMock = new Mock<IPurchaseLogic>(MockBehavior.Strict);
            purchaseServiceMock.Setup(p => p.Create(It.IsAny<Purchase>(), productServiceMock.Object,paymentMethodService.Object,
                reflectionLogic.Object, discountServiceMock.Object))
                .Returns(purchase);
            var purchaseController = new PurchaseController(purchaseServiceMock.Object);

            var result = purchaseController.Post(purchaseModelWrite, productServiceMock.Object, paymentMethodService.Object,
                reflectionLogic.Object, discountServiceMock.Object);
            var createdResult = result as CreatedAtRouteResult;
            var value = createdResult.Value;
            purchaseServiceMock.VerifyAll();
            Assert.AreEqual(value, PurchaseModelCreatedRead.ToModel(purchase));
        }

    }
}
