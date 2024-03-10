using TestSetUp;
using Exceptions;
using Entities;
using DataAccess.Interface;
using Moq;

namespace BusinessLogic.Test
{
    [TestClass]
    public class QuantityDiscountLogicTest : TestSetUps
    {
        [TestMethod]
        public void GetQuantityDiscountOkTest()
        {
            List<QuantityDiscount> quantityDiscountList = new List<QuantityDiscount>();
            QuantityDiscount oneQuantityDiscount = InitOneQuantityDiscountComplete();
            quantityDiscountList.Add(oneQuantityDiscount);

            var quantityDiscountRepositoryMock = new Mock<IQuantityDiscountRepository>(MockBehavior.Strict);
            quantityDiscountRepositoryMock.Setup(pd => pd.Get()).Returns(quantityDiscountList);
            var quantityDiscountService = new QuantityDiscountLogic(quantityDiscountRepositoryMock.Object);

            var quantityDiscountResult = quantityDiscountService.Get().ToList<QuantityDiscount>();
            quantityDiscountRepositoryMock.VerifyAll();
            Assert.IsTrue(quantityDiscountResult.Any(qd => qd.Id == oneQuantityDiscount.Id));
        }
        [TestMethod]
        public void GetOneQuantityDiscountByIdTest()
        {
            QuantityDiscount oneQuantityDiscount = InitOneQuantityDiscountComplete();

            var quantityDiscountRepositoryMock = new Mock<IQuantityDiscountRepository>(MockBehavior.Strict);
            quantityDiscountRepositoryMock.Setup(qd => qd.Get(oneQuantityDiscount.Id)).Returns(oneQuantityDiscount);
            var quantityDiscountService = new QuantityDiscountLogic(quantityDiscountRepositoryMock.Object);

            var quantityDiscountResult = quantityDiscountService.Get(oneQuantityDiscount.Id);
            quantityDiscountRepositoryMock.VerifyAll();
            Assert.AreEqual(quantityDiscountResult.Id, oneQuantityDiscount.Id);
        }
        [TestMethod]
        public void CreateQuantityDiscountOkTest()
        {
            QuantityDiscount oneQuantityDiscount = InitOneQuantityDiscountComplete();

            var quantityDiscountRepositoryMock = new Mock<IQuantityDiscountRepository>(MockBehavior.Strict);
            quantityDiscountRepositoryMock.Setup(qd => qd.Exists(oneQuantityDiscount)).Returns(false);
            quantityDiscountRepositoryMock.Setup(qd => qd.Add(oneQuantityDiscount));
            quantityDiscountRepositoryMock.Setup(qd => qd.Save());

            var quantityDiscountService = new QuantityDiscountLogic(quantityDiscountRepositoryMock.Object);

            var quantityDiscountResult = quantityDiscountService.Create(oneQuantityDiscount);
            quantityDiscountRepositoryMock.VerifyAll();
            Assert.AreEqual(quantityDiscountResult.Id, oneQuantityDiscount.Id);
        }
        [TestMethod]
        public void UpdateQuantityDiscountOkTest()
        {
            QuantityDiscount oldQuantityDiscount = InitOneQuantityDiscountComplete();
            QuantityDiscount newQuantityDiscount = InitOneQuantityDiscountComplete();
            newQuantityDiscount.Name = "Nuevo descuento";

            var quantityDiscountRepositoryMock = new Mock<IQuantityDiscountRepository>(MockBehavior.Strict);
            quantityDiscountRepositoryMock.Setup(qd => qd.Get(It.IsAny<Guid>())).Returns(oldQuantityDiscount);
            quantityDiscountRepositoryMock.Setup(qd => qd.Update(It.IsAny<QuantityDiscount>(), It.IsAny<QuantityDiscount>()));
            quantityDiscountRepositoryMock.Setup(qd => qd.Save());

            var quantityDiscountService = new QuantityDiscountLogic(quantityDiscountRepositoryMock.Object);
            quantityDiscountService.Update(oldQuantityDiscount.Id, newQuantityDiscount);

            quantityDiscountRepositoryMock.VerifyAll();
        }
        [TestMethod]
        public void RemoveQuantityDiscountOkTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();

            var quantityDiscountRepositoryMock = new Mock<IQuantityDiscountRepository>(MockBehavior.Strict);
            quantityDiscountRepositoryMock.Setup(qd => qd.Remove(quantityDiscount));
            quantityDiscountRepositoryMock.Setup(qd => qd.Save());

            var quantityDiscountService = new QuantityDiscountLogic(quantityDiscountRepositoryMock.Object);

            quantityDiscountService.Remove(quantityDiscount);

            quantityDiscountRepositoryMock.VerifyAll();
        }
        [ExpectedException(typeof(RepeatedObjectException))]
        [TestMethod]
        public void AddRepeatedQuantityDiscountTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();

            var quantityDiscountRepositoryMock = new Mock<IQuantityDiscountRepository>(MockBehavior.Strict);
            quantityDiscountRepositoryMock.Setup(qd => qd.Exists(quantityDiscount)).Returns(true);
            quantityDiscountRepositoryMock.Setup(qd => qd.Save());

            var quantityDiscountService = new QuantityDiscountLogic(quantityDiscountRepositoryMock.Object);

            quantityDiscountService.Create(quantityDiscount);
        }
        [ExpectedException(typeof(InvalidDiscountException))]
        [TestMethod]
        public void AddNullQuantityDiscountTest()
        {
            QuantityDiscount quantityDiscount = null;

            var quantityDiscountRepositoryMock = new Mock<IQuantityDiscountRepository>(MockBehavior.Strict);

            var quantityDiscountService = new QuantityDiscountLogic(quantityDiscountRepositoryMock.Object);

            quantityDiscountService.Create(quantityDiscount);
        }
        [ExpectedException(typeof(IncorrectNameException))]
        [TestMethod]
        public void AddQuantityDiscountWithEmptyNameTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();
            quantityDiscount.Name = "";

            var quantityDiscountRepositoryMock = new Mock<IQuantityDiscountRepository>(MockBehavior.Strict);

            var quantityDiscountService = new QuantityDiscountLogic(quantityDiscountRepositoryMock.Object);
            quantityDiscountService.Create(quantityDiscount);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddQuantityDiscountWithoutMinProductsForPromotionTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();
            quantityDiscount.MinProductsNeededForDiscount = 0;

            var quantityDiscountRepositoryMock = new Mock<IQuantityDiscountRepository>(MockBehavior.Strict);

            var quantityDiscountService = new QuantityDiscountLogic(quantityDiscountRepositoryMock.Object);
            quantityDiscountService.Create(quantityDiscount);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddQuantityDiscountWithotNumberOfProductsToBeFreeTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();
            quantityDiscount.NumberOfProductsToBeFree = 0;

            var quantityDiscountRepositoryMock = new Mock<IQuantityDiscountRepository>(MockBehavior.Strict);

            var quantityDiscountService = new QuantityDiscountLogic(quantityDiscountRepositoryMock.Object);
            quantityDiscountService.Create(quantityDiscount);
        }
        [TestMethod]
        public void AddQuantityDiscountWithotProductsToBeDiscountedTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();
            quantityDiscount.ProductToBeDiscounted = "";

            var quantityDiscountRepositoryMock = new Mock<IQuantityDiscountRepository>(MockBehavior.Strict);
            quantityDiscountRepositoryMock.Setup(qd => qd.Exists(It.IsAny<QuantityDiscount>())).Returns(false);
            quantityDiscountRepositoryMock.Setup(qd => qd.Add(It.IsAny<QuantityDiscount>()));
            quantityDiscountRepositoryMock.Setup(qd => qd.Save());
            var quantityDiscountService = new QuantityDiscountLogic(quantityDiscountRepositoryMock.Object);
            quantityDiscountService.Create(quantityDiscount);
        }
    }
}
