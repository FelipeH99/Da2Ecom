using TestSetUp;
using Entities;
using DataAccess.Interface;
using Moq;

namespace BusinessLogic.Test
{
    [TestClass]
    public class BrandLogicTest : TestSetUps
    {
        [TestMethod]
        public void GetBrandsOkTest()
        {
            List<Brand> brandList = new List<Brand>();
            Brand oneBrand = InitOneBrandComplete();
            brandList.Add(oneBrand);

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);
            brandRepositoryMock.Setup(b => b.Get()).Returns(brandList);
            var brandService = new BrandLogic(brandRepositoryMock.Object);

            var brandResult = brandService.Get().ToList<Brand>();
            brandRepositoryMock.VerifyAll();
            Assert.IsTrue(brandResult.Any(b => b.Id == oneBrand.Id));
        }
        [TestMethod]
        public void GetOneBrandByIdOkTest()
        {
            Brand oneBrand = InitOneBrandComplete();

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);
            brandRepositoryMock.Setup(b => b.Get(oneBrand.Id)).Returns(oneBrand);
            var brandService = new BrandLogic(brandRepositoryMock.Object);

            var brandResult = brandService.Get(oneBrand.Id);
            brandRepositoryMock.VerifyAll();
            Assert.AreEqual(brandResult.Id, oneBrand.Id);
        }
        [TestMethod]
        public void GetOneBrandByNameOkTest()
        {
            Brand oneBrand = InitOneBrandComplete();

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);
            brandRepositoryMock.Setup(b => b.GetByName(oneBrand.Name)).Returns(oneBrand);
            var brandService = new BrandLogic(brandRepositoryMock.Object);

            var brandResult = brandService.GetByName(oneBrand.Name);
            brandRepositoryMock.VerifyAll();
            Assert.AreEqual(brandResult.Id, oneBrand.Id);
        }
    }
}
