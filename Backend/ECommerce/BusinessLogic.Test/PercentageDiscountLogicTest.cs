using Entities;
using TestSetUp;
using Exceptions;
using DataAccess.Interface;
using Moq;

namespace BusinessLogic.Test
{
    [TestClass]
    public class PercentageDiscountLogicTest : TestSetUps
    {
        [TestMethod]
        public void GetPercentageDiscountOkTest()
        {
            List<PercentageDiscount> percentageDiscountList = new List<PercentageDiscount>();
            PercentageDiscount onePercentageDiscount = InitOnePercentageDiscountComplete();
            percentageDiscountList.Add(onePercentageDiscount);

            var percentageDiscountRepositoryMock = new Mock<IPercentageDiscountRepository>(MockBehavior.Strict);
            percentageDiscountRepositoryMock.Setup(pd => pd.Get()).Returns(percentageDiscountList);
            var percentageDiscountService = new PercentageDiscountLogic(percentageDiscountRepositoryMock.Object);

            var percentageDiscountResult = percentageDiscountService.Get().ToList<PercentageDiscount>();
            percentageDiscountRepositoryMock.VerifyAll();
            Assert.IsTrue(percentageDiscountResult.Any(pd => pd.Id == onePercentageDiscount.Id));
        }
        [TestMethod]
        public void GetOnePercentageDiscountByIdTest()
        {
            PercentageDiscount onePercentageDiscount = InitOnePercentageDiscountComplete();

            var percentageDiscountRepositoryMock = new Mock<IPercentageDiscountRepository>(MockBehavior.Strict);
            percentageDiscountRepositoryMock.Setup(pd => pd.Get(onePercentageDiscount.Id)).Returns(onePercentageDiscount);
            var percentageDiscountService = new PercentageDiscountLogic(percentageDiscountRepositoryMock.Object);

            var percentageDiscountResult = percentageDiscountService.Get(onePercentageDiscount.Id);
            percentageDiscountRepositoryMock.VerifyAll();
            Assert.AreEqual(percentageDiscountResult.Id, onePercentageDiscount.Id);
        }
        [TestMethod]
        public void CreatePercentageDiscountOkTest()
        {
            PercentageDiscount onePercentageDiscount = InitOnePercentageDiscountComplete();

            var percentageDiscountRepositoryMock = new Mock<IPercentageDiscountRepository>(MockBehavior.Strict);
            percentageDiscountRepositoryMock.Setup(pd => pd.Exists(onePercentageDiscount)).Returns(false);
            percentageDiscountRepositoryMock.Setup(pd => pd.Add(onePercentageDiscount));
            percentageDiscountRepositoryMock.Setup(pd => pd.Save());

            var percentageDiscountService = new PercentageDiscountLogic(percentageDiscountRepositoryMock.Object);

            var percentageDiscountResult = percentageDiscountService.Create(onePercentageDiscount);
            percentageDiscountRepositoryMock.VerifyAll();
            Assert.AreEqual(percentageDiscountResult.Id, onePercentageDiscount.Id);
        }
        [TestMethod]
        public void UpdatePercentageDiscountOkTest()
        {
            PercentageDiscount oldPercentageDiscount = InitOnePercentageDiscountComplete();
            PercentageDiscount newPercentageDiscount = InitOnePercentageDiscountComplete();
            newPercentageDiscount.PercentageDiscounted = 0.35;
            newPercentageDiscount.Name = "Nuevo descuento";

            var percentageDiscountRepositoryMock = new Mock<IPercentageDiscountRepository>(MockBehavior.Strict);
            percentageDiscountRepositoryMock.Setup(pd => pd.Get(It.IsAny<Guid>())).Returns(oldPercentageDiscount);
            percentageDiscountRepositoryMock.Setup(pd => pd.Update(It.IsAny<PercentageDiscount>(), It.IsAny<PercentageDiscount>()));
            percentageDiscountRepositoryMock.Setup(pd => pd.Save());



            var percentageDiscountService = new PercentageDiscountLogic(percentageDiscountRepositoryMock.Object);
            percentageDiscountService.Update(oldPercentageDiscount.Id, newPercentageDiscount);

            percentageDiscountRepositoryMock.VerifyAll();
        }
        [TestMethod]
        public void RemovePercentageDiscountOkTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();

            var percentageDiscountRepositoryMock = new Mock<IPercentageDiscountRepository>(MockBehavior.Strict);
            percentageDiscountRepositoryMock.Setup(pd => pd.Remove(percentageDiscount));
            percentageDiscountRepositoryMock.Setup(pd => pd.Save());

            var percentageDiscountService = new PercentageDiscountLogic(percentageDiscountRepositoryMock.Object);

            percentageDiscountService.Remove(percentageDiscount);

            percentageDiscountRepositoryMock.VerifyAll();
        }
        [ExpectedException(typeof(RepeatedObjectException))]
        [TestMethod]
        public void AddRepeatedPercentageDiscountTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();

            var percentageDiscountRepositoryMock = new Mock<IPercentageDiscountRepository>(MockBehavior.Strict);
            percentageDiscountRepositoryMock.Setup(pd => pd.Exists(percentageDiscount)).Returns(true);
            percentageDiscountRepositoryMock.Setup(pd => pd.Save());

            var percentageDiscountService = new PercentageDiscountLogic(percentageDiscountRepositoryMock.Object);

            percentageDiscountService.Create(percentageDiscount);
        }
        [ExpectedException(typeof(InvalidDiscountException))]
        [TestMethod]
        public void AddNullPercentageDiscountTest()
        {
            PercentageDiscount percentageDiscount = null;

            var percentageDiscountRepositoryMock = new Mock<IPercentageDiscountRepository>(MockBehavior.Strict);

            var percentageDiscountService = new PercentageDiscountLogic(percentageDiscountRepositoryMock.Object);

            percentageDiscountService.Create(percentageDiscount);
        }
        [ExpectedException(typeof(IncorrectNameException))]
        [TestMethod]
        public void AddPercentageDiscountWithEmptyNameTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();
            percentageDiscount.Name = "";

            var percentageDiscountRepositoryMock = new Mock<IPercentageDiscountRepository>(MockBehavior.Strict);

            var percentageDiscountService = new PercentageDiscountLogic(percentageDiscountRepositoryMock.Object);
            percentageDiscountService.Create(percentageDiscount);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddPercentageDiscountWithoutMinProductsForPromotionTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();
            percentageDiscount.MinProductsNeededForDiscount = 0;

            var percentageDiscountRepositoryMock = new Mock<IPercentageDiscountRepository>(MockBehavior.Strict);

            var percentageDiscountService = new PercentageDiscountLogic(percentageDiscountRepositoryMock.Object);
            percentageDiscountService.Create(percentageDiscount);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddPercentageDiscountWithotPercentageDiscountedTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();
            percentageDiscount.PercentageDiscounted = 0;

            var percentageDiscountRepositoryMock = new Mock<IPercentageDiscountRepository>(MockBehavior.Strict);

            var percentageDiscountService = new PercentageDiscountLogic(percentageDiscountRepositoryMock.Object);
            percentageDiscountService.Create(percentageDiscount);
        }
        [TestMethod]
        public void AddPercentageDiscountWithotProductsToBeDiscountedTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();
            percentageDiscount.ProductToBeDiscounted = "";

            var percentageDiscountRepositoryMock = new Mock<IPercentageDiscountRepository>(MockBehavior.Strict);
            percentageDiscountRepositoryMock.Setup(pd => pd.Exists(It.IsAny<PercentageDiscount>())).Returns(false);
            percentageDiscountRepositoryMock.Setup(pd => pd.Add(It.IsAny<PercentageDiscount>()));
            percentageDiscountRepositoryMock.Setup(pd => pd.Save());


            var percentageDiscountService = new PercentageDiscountLogic(percentageDiscountRepositoryMock.Object);
            percentageDiscountService.Create(percentageDiscount);
        }
    }
}
