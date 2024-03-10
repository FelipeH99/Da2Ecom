using Moq;
using TestSetUp;
using Entities;
using DataAccess.Interface;

namespace BusinessLogic.Test
{
    [TestClass]
    public class PaymentMethodLogicTest : TestSetUps
    {
        [TestMethod]
        public void GetPaymentMethodOkTest()
        {
            List<PaymentMethod> paymentMethodList = new List<PaymentMethod>();
            PaymentMethod onePaymentMethod = InitOnePaymentMethod();
            paymentMethodList.Add(onePaymentMethod);

            var paymentRepositoryMock = new Mock<IPaymentMethodRepository>(MockBehavior.Strict);
            paymentRepositoryMock.Setup(p => p.Get()).Returns(paymentMethodList);
            var paymentService = new PaymentMethodLogic(paymentRepositoryMock.Object);

            var paymentResult = paymentService.Get().ToList<PaymentMethod>();
            paymentRepositoryMock.VerifyAll();
            Assert.IsTrue(paymentResult.Any(p => p.Id == onePaymentMethod.Id));
        }
        [TestMethod]
        public void GetOnePaymentMethodByIdTest()
        {
            PaymentMethod onePaymentMethod = InitOnePaymentMethod();

            var paymentRepositoryMock = new Mock<IPaymentMethodRepository>(MockBehavior.Strict);
            paymentRepositoryMock.Setup(p => p.Get(onePaymentMethod.Id)).Returns(onePaymentMethod);
            var paymentService = new PaymentMethodLogic(paymentRepositoryMock.Object);

            var paymentResult = paymentService.Get(onePaymentMethod.Id);
            paymentRepositoryMock.VerifyAll();
            Assert.AreEqual(paymentResult.Id, onePaymentMethod.Id);
        }
    }
}
