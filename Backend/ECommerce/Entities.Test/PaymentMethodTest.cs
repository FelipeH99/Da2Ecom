using TestSetUp;

namespace Entities.Test
{
    [TestClass]
    public class PaymentMethodTest : TestSetUps
    {

        [TestMethod]
        public void CreatePaymentMethodOkTest()
        {
            PaymentMethod paymentMethod = new PaymentMethod();

            Assert.IsNotNull(paymentMethod);
        }
        [TestMethod]
        public void CreatePaymentMethodWithNameOKTest()
        {
            PaymentMethod paymentMethod = new PaymentMethod();
            paymentMethod.Name = "Debito Banco Santander";

            Assert.AreEqual(paymentMethod.Name, "Debito Banco Santander");
        }
        [TestMethod]
        public void EqualsOKTest()
        {
            PaymentMethod paymentMethod = InitOnePaymentMethod();
            PaymentMethod anotherPaymentMethod = InitOnePaymentMethod();


            Assert.AreEqual(paymentMethod, anotherPaymentMethod);
        }
        [TestMethod]
        public void GetHashCodeOkTest()
        {
            PaymentMethod paymentMethod = InitOnePaymentMethod();
            var hash = paymentMethod.Name.GetHashCode();

            Assert.AreEqual(hash, paymentMethod.GetHashCode());
        }
    }
}
