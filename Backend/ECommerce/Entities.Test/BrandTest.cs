
namespace Entities.Test
{
    [TestClass]
    public class BrandTest
    {
        [TestMethod]
        public void CreateBrandOKTest() 
        {
            BrandTest oneBrand = new BrandTest();

            Assert.IsNotNull(oneBrand);
        }
        [TestMethod]
        public void CreateBrandWithNameOKTest()
        {
            Brand oneBrand = new Brand();
            oneBrand.Name = "Adidas";

            Assert.AreEqual(oneBrand.Name,"Adidas");
        }
        [TestMethod]
        public void EqualBrandOKTest() 
        {
            Brand oneBrand = new Brand()
            {
                Name = "Gucci"
            };
            Brand anotherBrand = new Brand()
            {
                Name = "Gucci"
            };
            Assert.AreEqual(oneBrand, anotherBrand);
        }
        [TestMethod]
        public void GetHashCodeOkTest()
        {
            Brand oneBrand = new Brand()
            {
                Name = "Gucci"
            };
            var oneName = oneBrand.Name;

            Assert.AreEqual(oneName.GetHashCode(), oneBrand.GetHashCode());
        }
    }
}
