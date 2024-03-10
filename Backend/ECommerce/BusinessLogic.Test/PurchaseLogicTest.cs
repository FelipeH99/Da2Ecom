using Exceptions;
using TestSetUp;
using Entities;
using DataAccess.Interface;
using Moq;
using BusinessLogic.Interface;
using Reflection;

namespace BusinessLogic.Test
{
    [TestClass]
    public class PurchaseLogicTest : TestSetUps
    {
        [TestMethod]
        public void GetPurchaseOkTest()
        {
            List<Purchase> purchaseList = new List<Purchase>();
            Purchase onePurchase = InitOnePurchaseComplete();
            purchaseList.Add(onePurchase);

            var purchaseRepositoryMock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            purchaseRepositoryMock.Setup(p => p.Get()).Returns(purchaseList);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            var discountRepositoryMock = new Mock<IDiscountLogic>(MockBehavior.Strict);

            var purchaseService = new PurchaseLogic(purchaseRepositoryMock.Object, userRepositoryMock.Object);

            var purchaseResult = purchaseService.Get().ToList<Purchase>();
            purchaseRepositoryMock.VerifyAll();
            Assert.IsTrue(purchaseResult.Any(p => p.Id == onePurchase.Id));
        }
        [TestMethod]
        public void GetOnePurchaseByIdTest()
        {
            Purchase onePurchase = InitOnePurchaseComplete();

            var purchaseRepositoryMock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            purchaseRepositoryMock.Setup(p => p.Get(onePurchase.Id)).Returns(onePurchase);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            var discountRepositoryMock = new Mock<IDiscountLogic>(MockBehavior.Strict);

            var purchaseService = new PurchaseLogic(purchaseRepositoryMock.Object, userRepositoryMock.Object);

            var purchaseResult = purchaseService.Get(onePurchase.Id); ;
            purchaseRepositoryMock.VerifyAll();
            Assert.AreEqual(purchaseResult.Id, onePurchase.Id);
        }
        [TestMethod]
        public void GetOnePurchaseByUserIdTest()
        {
            Purchase onePurchase = InitOnePurchaseComplete();
            List<Purchase> purchases = new List<Purchase>() { onePurchase };

            var purchaseRepositoryMock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            purchaseRepositoryMock.Setup(p => p.GetByUser(onePurchase.Id)).Returns(purchases);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            var discountRepositoryMock = new Mock<IDiscountLogic>(MockBehavior.Strict);

            var purchaseService = new PurchaseLogic(purchaseRepositoryMock.Object, userRepositoryMock.Object);

            var purchaseResult = purchaseService.GetByUserId(onePurchase.User.Id).ToList();
            purchaseRepositoryMock.VerifyAll();
            Assert.AreEqual(purchaseResult[0].Id, onePurchase.Id);
        }
        [TestMethod]
        public void CreatePurchaseOkTest()
        {
            Purchase onePurchase = InitOnePurchaseWithOneProductComplete();
            User user = onePurchase.User;
            Product product = onePurchase.Products[0];

            List<QuantityDiscount> quantityDiscount = new List<QuantityDiscount>() { InitOneQuantityDiscountComplete() };
            List<ColorDiscount> colorDiscount = new List<ColorDiscount>() { InitOneColorDiscountComplete() };
            List<BrandDiscount> brandDiscount = new List<BrandDiscount>(){InitOneBrandDiscountComplete()};
            List<PercentageDiscount> percentageDiscount = new List<PercentageDiscount>() { InitOnePercentageDiscountComplete()};

            var tuple = new List<(string name, double amountDiscounted)> { ("No se aplico descuento", 0.0) };
            
            var purchaseRepositoryMock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            purchaseRepositoryMock.Setup(p => p.Exists(onePurchase)).Returns(false);
            purchaseRepositoryMock.Setup(p => p.Add(onePurchase));
            purchaseRepositoryMock.Setup(p => p.Save());

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(u => u.Exists(user)).Returns(true);
            userRepositoryMock.Setup(u => u.Get(user.Id)).Returns(user);

            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.Exists(product)).Returns(true);

            var quantityDiscountRepositoryMock = new Mock<IQuantityDiscountRepository>(MockBehavior.Strict);
            quantityDiscountRepositoryMock.Setup(qd => qd.Get()).Returns(quantityDiscount);

            var percentageDiscountRepositoryMock = new Mock<IPercentageDiscountRepository>(MockBehavior.Strict);
            percentageDiscountRepositoryMock.Setup(pd => pd.Get()).Returns(percentageDiscount);

            var colorDiscountRepositoryMock = new Mock<IColorDiscountRepository>(MockBehavior.Strict);
            colorDiscountRepositoryMock.Setup(cd => cd.Get()).Returns(colorDiscount);

            var brandDiscountRepositoryMock = new Mock<IBrandDiscountRepository>(MockBehavior.Strict);
            brandDiscountRepositoryMock.Setup(bd => bd.Get()).Returns(brandDiscount);

            var reflectionLogic = new Mock<IReflectionImplementation>(MockBehavior.Strict);
            reflectionLogic.Setup(r => r.LoadDll(It.IsAny<List<Product>>())).Returns(tuple);

            var discountLogicService = new DiscountLogic(percentageDiscountRepositoryMock.Object, quantityDiscountRepositoryMock.Object,
                brandDiscountRepositoryMock.Object, colorDiscountRepositoryMock.Object);

            var purchaseService = new PurchaseLogic(purchaseRepositoryMock.Object, userRepositoryMock.Object);

            var paymentMethodService = new Mock<IPaymentMethodLogic>(MockBehavior.Strict);
            paymentMethodService.Setup(pm => pm.Get(It.IsAny<Guid>())).Returns(onePurchase.PaymentMethod);

            var productServiceMock = new Mock<IProductLogic>(MockBehavior.Strict);
            productServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(product);
            productServiceMock.Setup(p => p.Update(It.IsAny<Guid>(), It.IsAny<Product>())).Returns(product);

            var purchaseResult = purchaseService.Create(onePurchase, productServiceMock.Object, paymentMethodService.Object,
                reflectionLogic.Object, discountLogicService);
            purchaseRepositoryMock.VerifyAll();
            Assert.AreEqual(purchaseResult.Id, onePurchase.Id);
        }
        [TestMethod]
        public void UpdatePurchaseOkTest()
        {
            Purchase oldPurchase = InitOnePurchaseWithOneProductComplete();
            Purchase newPurchase = InitOnePurchaseWithOneProductComplete();
            newPurchase.DiscountApplied = "";
            newPurchase.User = InitAnotherUserComplete();

            var purchaseRepositoryMock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            purchaseRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(oldPurchase);
            purchaseRepositoryMock.Setup(p => p.Update(It.IsAny<Purchase>(), It.IsAny<Purchase>()));
            purchaseRepositoryMock.Setup(p => p.Save());

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            var discountRepositoryMock = new Mock<IDiscountLogic>(MockBehavior.Strict);

            var purchaseService = new PurchaseLogic(purchaseRepositoryMock.Object, userRepositoryMock.Object);

            purchaseService.Update(oldPurchase.Id, newPurchase);

            purchaseRepositoryMock.VerifyAll();
        }
        [TestMethod]
        public void RemovePurchaseOkTest()
        {
            Purchase purchase = InitOnePurchaseWithOneProductComplete();

            var purchaseRepositoryMock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            purchaseRepositoryMock.Setup(p => p.Remove(purchase));
            purchaseRepositoryMock.Setup(p => p.Save());

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            var discountRepositoryMock = new Mock<IDiscountLogic>(MockBehavior.Strict);

            var purchaseService = new PurchaseLogic(purchaseRepositoryMock.Object, userRepositoryMock.Object);

            purchaseService.Remove(purchase);

            purchaseRepositoryMock.VerifyAll();
        }
        [ExpectedException(typeof(RepeatedObjectException))]
        [TestMethod]
        public void AddRepeatedPurchaseTest()
        {
            Purchase purchase = InitOnePurchaseWithOneProductComplete();
            User user = purchase.User;
            Product product = purchase.Products[0];

            var purchaseRepositoryMock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            purchaseRepositoryMock.Setup(p => p.Exists(purchase)).Returns(true);
            purchaseRepositoryMock.Setup(p => p.Save());

            var discountLogicMock = new Mock<IDiscountLogic>(MockBehavior.Strict);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(p => p.Get(user.Id)).Returns(user);
            userRepositoryMock.Setup(u => u.Exists(user)).Returns(true);
            userRepositoryMock.Setup(u => u.IsDeleted(user.Email,user.Password)).Returns(true);

            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.Exists(product)).Returns(false);

            var discountServiceMock = new Mock<IDiscountLogic>(MockBehavior.Strict);

            var purchaseService = new PurchaseLogic(purchaseRepositoryMock.Object, userRepositoryMock.Object);

            var productServiceMock = new Mock<IProductLogic>(MockBehavior.Strict);
            productServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(product);
            productServiceMock.Setup(p => p.Update(It.IsAny<Guid>(), It.IsAny<Product>())).Returns(product);

            var paymentMethodService = new Mock<IPaymentMethodLogic>(MockBehavior.Strict);

            var reflectionLogic = new Mock<IReflectionImplementation>(MockBehavior.Strict);
            reflectionLogic.Setup(r => r.LoadDll(purchase.Products));

            purchaseService.Create(purchase, productServiceMock.Object, paymentMethodService.Object,reflectionLogic.Object,
                discountServiceMock.Object);
        }
        [ExpectedException(typeof(InvalidPurchaseException))]
        [TestMethod]
        public void AddNullPurchaseTest()
        {
            Purchase purchase = null;
            Product product = null;

            var purchaseRepositoryMock = new Mock<IPurchaseRepository>(MockBehavior.Strict);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            var discountServiceMock = new Mock<IDiscountLogic>(MockBehavior.Strict);

            var purchaseService = new PurchaseLogic(purchaseRepositoryMock.Object, userRepositoryMock.Object);

            var productServiceMock = new Mock<IProductLogic>(MockBehavior.Strict);
            productServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(product);
            productServiceMock.Setup(p => p.Update(It.IsAny<Guid>(), It.IsAny<Product>())).Returns(product);

            var paymentMethodService = new Mock<IPaymentMethodLogic>(MockBehavior.Strict);

            var reflectionLogic = new Mock<IReflectionImplementation>(MockBehavior.Strict);
            reflectionLogic.Setup(r => r.LoadDll(It.IsAny<List<Product>>()));

            purchaseService.Create(purchase, productServiceMock.Object, paymentMethodService.Object, reflectionLogic.Object,
                discountServiceMock.Object);
        }
        [ExpectedException(typeof(InvalidUserInPurchaseException))]
        [TestMethod]
        public void AddPurchaseWithNullUserTest()
        {
            Purchase purchase = InitOnePurchaseComplete();
            User user = purchase.User;
            Product product = purchase.Products[0];
            purchase.User = null;

            var purchaseRepositoryMock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            purchaseRepositoryMock.Setup(p => p.Exists(purchase)).Returns(true);

            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.Exists(product)).Returns(true);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            var discountServiceMock = new Mock<IDiscountLogic>(MockBehavior.Strict);

            var purchaseService = new PurchaseLogic(purchaseRepositoryMock.Object, userRepositoryMock.Object);

            var productServiceMock = new Mock<IProductLogic>(MockBehavior.Strict);
            productServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(product);
            productServiceMock.Setup(p => p.Update(It.IsAny<Guid>(),It.IsAny<Product>())).Returns(product);

            var paymentMethodService = new Mock<IPaymentMethodLogic>(MockBehavior.Strict);

            var reflectionLogic = new Mock<IReflectionImplementation>(MockBehavior.Strict);
            reflectionLogic.Setup(r => r.LoadDll(purchase.Products));

            purchaseService.Create(purchase, productServiceMock.Object, paymentMethodService.Object,reflectionLogic.Object,
                discountServiceMock.Object);
        }
        [ExpectedException(typeof(InvalidUserInPurchaseException))]
        [TestMethod]
        public void AddPurchaseWithUserNotRegisteredTest()
        {
            Purchase purchase = InitOnePurchaseComplete();
            User user = purchase.User;
            Product product = purchase.Products[0];

            var purchaseRepositoryMock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            purchaseRepositoryMock.Setup(p => p.Exists(purchase)).Returns(true);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(p => p.Get(user.Id)).Returns(user);
            userRepositoryMock.Setup(p => p.Exists(user)).Returns(false);

            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.Exists(product)).Returns(true);

            var discountServiceMock = new Mock<IDiscountLogic>(MockBehavior.Strict);

            var purchaseService = new PurchaseLogic(purchaseRepositoryMock.Object, userRepositoryMock.Object);

            var productServiceMock = new Mock<IProductLogic>(MockBehavior.Strict);
            productServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(product);
            productServiceMock.Setup(p => p.Update(It.IsAny<Guid>(), It.IsAny<Product>())).Returns(product);

            var paymentMethodService = new Mock<IPaymentMethodLogic>(MockBehavior.Strict);

            var reflectionLogic = new Mock<IReflectionImplementation>(MockBehavior.Strict);
            reflectionLogic.Setup(r => r.LoadDll(purchase.Products));

            purchaseService.Create(purchase, productServiceMock.Object, paymentMethodService.Object,reflectionLogic.Object,
                discountServiceMock.Object);
        }
        [ExpectedException(typeof(InvalidPurchaseException))]
        [TestMethod]
        public void AddPurchaseWithNoProductsTest()
        {
            Purchase purchase = InitOnePurchaseComplete();
            User user = purchase.User;
            purchase.Products = new List<Product>();
            Product oneProduct = null;

            var purchaseRepositoryMock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            purchaseRepositoryMock.Setup(p => p.Exists(purchase)).Returns(true);

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(p => p.Get(user.Id)).Returns(user);
            userRepositoryMock.Setup(p => p.Exists(user)).Returns(true);

            var discountServiceMock = new Mock<IDiscountLogic>(MockBehavior.Strict);

            var purchaseService = new PurchaseLogic(purchaseRepositoryMock.Object, userRepositoryMock.Object);

            var productServiceMock = new Mock<IProductLogic>(MockBehavior.Strict);
            productServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(oneProduct);

            var paymentMethodService = new Mock<IPaymentMethodLogic>(MockBehavior.Strict);

            var reflectionLogic = new Mock<IReflectionImplementation>(MockBehavior.Strict);
            reflectionLogic.Setup(r => r.LoadDll(purchase.Products));

            purchaseService.Create(purchase, productServiceMock.Object, paymentMethodService.Object,reflectionLogic.Object,
                discountServiceMock.Object);
        }
    }
}
