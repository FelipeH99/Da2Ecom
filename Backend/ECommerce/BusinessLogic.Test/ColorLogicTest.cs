using DataAccess.Interface;
using Entities;
using Moq;
using TestSetUp;

namespace BusinessLogic.Test
{
    [TestClass]
    public class ColorLogicTest : TestSetUps
    {
        [TestMethod]
        public void GetColorsOkTest()
        {
            List<Color> colorList = new List<Color>();
            Color oneColor = InitOneColorComplete();
            colorList.Add(oneColor);

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);
            colorRepositoryMock.Setup(c => c.Get()).Returns(colorList);
            var colorService = new ColorLogic(colorRepositoryMock.Object);

            var colorResult = colorService.Get().ToList<Color>();
            colorRepositoryMock.VerifyAll();
            Assert.IsTrue(colorResult.Any(c => c.Id == oneColor.Id));
        }
        [TestMethod]
        public void GetOneColorByIdOkTest()
        {
            Color oneColor = InitOneColorComplete();

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);
            colorRepositoryMock.Setup(c => c.Get(oneColor.Id)).Returns(oneColor);
            var colorService = new ColorLogic(colorRepositoryMock.Object);

            var colorResult = colorService.Get(oneColor.Id);
            colorRepositoryMock.VerifyAll();
            Assert.AreEqual(colorResult.Id, oneColor.Id);
        }
        [TestMethod]
        public void GetOneColorByNameOkTest()
        {
            Color oneColor = InitOneColorComplete();

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);
            colorRepositoryMock.Setup(c => c.GetByName(oneColor.Name)).Returns(oneColor);
            var colorService = new ColorLogic(colorRepositoryMock.Object);

            var colorResult = colorService.GetByName(oneColor.Name);
            colorRepositoryMock.VerifyAll();
            Assert.AreEqual(colorResult.Id, oneColor.Id);
        }
    }
}
