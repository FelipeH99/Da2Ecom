
namespace Entities.Test
{
    [TestClass]
    public class ColorTest
    {
        [TestMethod]
        public void CreateColorOKTest() 
        {
            Color oneColor = new Color();

            Assert.IsNotNull(oneColor);
        }

        [TestMethod]
        public void CreateColorWithNameOKTest()
        {
            Color oneColor = new Color();
            oneColor.Name = "Rojo";

            Assert.AreEqual(oneColor.Name,"Rojo");
        }
        [TestMethod]
        public void EqualColorOKTest()
        {
            Color oneColor = new Color()
            {
                Name = "Rojo"
            };
            Color anotherColor = new Color()
            {
                Name = "Rojo"
            };
            Assert.AreEqual(oneColor, anotherColor);
        }
        [TestMethod]
        public void GetHashCodeOkTest()
        {
            Color oneColor = new Color()
            {
                Name = "Azul"
            };
            var oneName = oneColor.Name;

            Assert.AreEqual(oneName.GetHashCode(), oneColor.GetHashCode());
        }

    }
}
