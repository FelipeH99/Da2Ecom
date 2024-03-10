using Entities;
using TestSetUp;
using DataAccess.Interface;
using Moq;
using Exceptions;

namespace BusinessLogic.Test
{
    [TestClass]
    public class ColorDiscountLogicTest : TestSetUps
    {
        [TestMethod]
        public void GetColorDiscountOkTest()
        {
            List<ColorDiscount> colorDiscountList = new List<ColorDiscount>();
            ColorDiscount oneColorDiscount = InitOneColorDiscountComplete();
            colorDiscountList.Add(oneColorDiscount);

            var colorDiscountRepositoryMock = new Mock<IColorDiscountRepository>(MockBehavior.Strict);
            colorDiscountRepositoryMock.Setup(cd => cd.Get()).Returns(colorDiscountList);

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);

            var colorDiscountService = new ColorDiscountLogic(colorDiscountRepositoryMock.Object,colorRepositoryMock.Object);

            var colorDiscountResult = colorDiscountService.Get().ToList<ColorDiscount>();
            colorDiscountRepositoryMock.VerifyAll();
            Assert.IsTrue(colorDiscountResult.Any(cd => cd.Id == oneColorDiscount.Id));
        }
        [TestMethod]
        public void GetOneColorDiscountByIdTest()
        {
            ColorDiscount oneColorDiscount = InitOneColorDiscountComplete();

            var colorDiscountRepositoryMock = new Mock<IColorDiscountRepository>(MockBehavior.Strict);
            colorDiscountRepositoryMock.Setup(cd => cd.Get(oneColorDiscount.Id)).Returns(oneColorDiscount);

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);

            var colorDiscountService = new ColorDiscountLogic(colorDiscountRepositoryMock.Object, colorRepositoryMock.Object);

            var colorDiscountResult = colorDiscountService.Get(oneColorDiscount.Id);
            colorDiscountRepositoryMock.VerifyAll();
            Assert.AreEqual(colorDiscountResult.Id, oneColorDiscount.Id);
        }
        [TestMethod]
        public void CreateColorDiscountOkTest()
        {
            ColorDiscount colorDiscount = InitSecondColorDiscountComplete();
            Color color = colorDiscount.Color;

            var colorDiscountRepositoryMock = new Mock<IColorDiscountRepository>(MockBehavior.Strict);
            colorDiscountRepositoryMock.Setup(cd => cd.Exists(colorDiscount)).Returns(false);
            colorDiscountRepositoryMock.Setup(cd => cd.Add(colorDiscount));
            colorDiscountRepositoryMock.Setup(cd => cd.Save());

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);
            colorRepositoryMock.Setup(c => c.Exists(color)).Returns(true);


            var colorDiscountService = new ColorDiscountLogic(colorDiscountRepositoryMock.Object
                , colorRepositoryMock.Object);

            var colorDiscountResult = colorDiscountService.Create(colorDiscount);
            colorDiscountRepositoryMock.VerifyAll();
            Assert.AreEqual(colorDiscountResult.Id, colorDiscount.Id);
        }
        [TestMethod]
        public void UpdateColorDiscountOkTest()
        {
            ColorDiscount oldColorDiscount = InitSecondColorDiscountComplete();
            ColorDiscount newColorDiscount = InitThirdColorDiscountComplete();

            var colorDiscountRepositoryMock = new Mock<IColorDiscountRepository>(MockBehavior.Strict);
            colorDiscountRepositoryMock.Setup(cd => cd.Get(It.IsAny<Guid>())).Returns(oldColorDiscount);
            colorDiscountRepositoryMock.Setup(cd => cd.Update(It.IsAny<ColorDiscount>(), It.IsAny<ColorDiscount>()));
            colorDiscountRepositoryMock.Setup(cd => cd.Save());


            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);

            var colorDiscountService = new ColorDiscountLogic(colorDiscountRepositoryMock.Object
                , colorRepositoryMock.Object);
            colorDiscountService.Update(oldColorDiscount.Id, newColorDiscount);

            colorDiscountRepositoryMock.VerifyAll();
        }
        [TestMethod]
        public void RemoveColorDiscountOkTest()
        {
            ColorDiscount colorDiscount = InitSecondColorDiscountComplete();

            var colorDiscountRepositoryMock = new Mock<IColorDiscountRepository>(MockBehavior.Strict);
            colorDiscountRepositoryMock.Setup(cd => cd.Remove(colorDiscount));
            colorDiscountRepositoryMock.Setup(cd => cd.Save());


            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);

            var colorDiscountService = new ColorDiscountLogic(colorDiscountRepositoryMock.Object, colorRepositoryMock.Object);

            colorDiscountService.Remove(colorDiscount);

            colorDiscountRepositoryMock.VerifyAll();
        }
        [ExpectedException(typeof(RepeatedObjectException))]
        [TestMethod]
        public void AddRepeatedColorDiscountTest()
        {
            ColorDiscount colorDiscount = InitSecondColorDiscountComplete();
            Color oneColor = colorDiscount.Color;

            var colorDiscountRepositoryMock = new Mock<IColorDiscountRepository>(MockBehavior.Strict);
            colorDiscountRepositoryMock.Setup(cd => cd.Exists(colorDiscount)).Returns(true);
            colorDiscountRepositoryMock.Setup(cd => cd.Save());


            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);
            colorRepositoryMock.Setup(c => c.Exists(oneColor)).Returns(true);

            var colorDiscountService = new ColorDiscountLogic(colorDiscountRepositoryMock.Object
                , colorRepositoryMock.Object);

            colorDiscountService.Create(colorDiscount);
        }



        [ExpectedException(typeof(InvalidDiscountException))]
        [TestMethod]
        public void AddNullColorDiscountTest()
        {
            ColorDiscount colorDiscount = null;

            var colorDiscountRepositoryMock = new Mock<IColorDiscountRepository>(MockBehavior.Strict);


            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);

            var colorDiscountService = new ColorDiscountLogic(colorDiscountRepositoryMock.Object, colorRepositoryMock.Object);

            colorDiscountService.Create(colorDiscount);
        }

        [ExpectedException(typeof(IncorrectNameException))]
        [TestMethod]
        public void AddColorDiscountWithEmptyNameTest()
        {
            ColorDiscount colorDiscount = InitOneColorDiscountComplete();
            colorDiscount.Name = "";

            var colorDiscountRepositoryMock = new Mock<IColorDiscountRepository>(MockBehavior.Strict);


            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);

            var colorDiscountService = new ColorDiscountLogic(colorDiscountRepositoryMock.Object, colorRepositoryMock.Object);

            colorDiscountService.Create(colorDiscount);
        }

        [ExpectedException(typeof(IncorrectColorForDiscount))]
        [TestMethod]
        public void AddColorDiscountWithNullColorTest()
        {
            ColorDiscount colorDiscount = InitSecondColorDiscountComplete();
            colorDiscount.Color = null;

            var colorDiscountRepositoryMock = new Mock<IColorDiscountRepository>(MockBehavior.Strict);


            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);

            var colorDiscountService = new ColorDiscountLogic(colorDiscountRepositoryMock.Object, colorRepositoryMock.Object);

            colorDiscountService.Create(colorDiscount);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddColorDiscountWithoutMinProductsForPromotionTest()
        {
            ColorDiscount colorDiscount = InitSecondColorDiscountComplete();
            colorDiscount.MinProductsNeededForDiscount = 0;

            var colorDiscountRepositoryMock = new Mock<IColorDiscountRepository>(MockBehavior.Strict);


            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);

            var colorDiscountService = new ColorDiscountLogic(colorDiscountRepositoryMock.Object, colorRepositoryMock.Object);

            colorDiscountService.Create(colorDiscount);
        }


        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddColorDiscountWithotPercentageDiscountTest()
        {
            ColorDiscount colorDiscount = InitSecondColorDiscountComplete();
            colorDiscount.PercentageDiscount = 0;

            var colorDiscountRepositoryMock = new Mock<IColorDiscountRepository>(MockBehavior.Strict);


            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);

            var colorDiscountService = new ColorDiscountLogic(colorDiscountRepositoryMock.Object, colorRepositoryMock.Object);

            colorDiscountService.Create(colorDiscount);
        }

        [TestMethod]
        public void AddColorDiscountWithotItemToBeDiscountedTest()
        {
            ColorDiscount colorDiscount = InitSecondColorDiscountComplete();
            colorDiscount.ProductToBeDiscounted = "";

            var colorDiscountRepositoryMock = new Mock<IColorDiscountRepository>(MockBehavior.Strict);
            colorDiscountRepositoryMock.Setup(cd => cd.Exists(It.IsAny<ColorDiscount>())).Returns(false);
            colorDiscountRepositoryMock.Setup(cd => cd.Add(It.IsAny<ColorDiscount>()));
            colorDiscountRepositoryMock.Setup(cd => cd.Save());
            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);
            colorRepositoryMock.Setup(c => c.Exists(It.IsAny<Color>())).Returns(true);

            var colorDiscountService = new ColorDiscountLogic(colorDiscountRepositoryMock.Object, colorRepositoryMock.Object);

            colorDiscountService.Create(colorDiscount);
        }
    }
}
