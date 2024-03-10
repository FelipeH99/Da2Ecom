using Exceptions;
using TestSetUp;
using Entities;
using Moq;
using DataAccess.Interface;

namespace BusinessLogic.Test
{
    [TestClass]
    public class BrandDiscountLogicTest : TestSetUps
    {
        [TestMethod]
        public void GetBrandDiscountOkTest()
        {
            List<BrandDiscount> brandDiscountList = new List<BrandDiscount>();
            BrandDiscount oneBrandDiscount = InitOneBrandDiscountComplete();
            brandDiscountList.Add(oneBrandDiscount);

            var brandDiscountRepositoryMock = new Mock<IBrandDiscountRepository>(MockBehavior.Strict);
            brandDiscountRepositoryMock.Setup(bd => bd.Get()).Returns(brandDiscountList);

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);

            var brandDiscountService = new BrandDiscountLogic(brandDiscountRepositoryMock.Object,brandRepositoryMock.Object);

            var brandDiscountResult = brandDiscountService.Get().ToList<BrandDiscount>();
            brandDiscountRepositoryMock.VerifyAll();
            Assert.IsTrue(brandDiscountResult.Any(bd => bd.Id == oneBrandDiscount.Id));
        }
        [TestMethod]
        public void GetOneBrandDiscountByIdTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();

            var brandDiscountRepositoryMock = new Mock<IBrandDiscountRepository>(MockBehavior.Strict);
            brandDiscountRepositoryMock.Setup(bd => bd.Get(brandDiscount.Id)).Returns(brandDiscount);

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);

            var brandDiscountService = new BrandDiscountLogic(brandDiscountRepositoryMock.Object, brandRepositoryMock.Object);

            var brandDiscountResult = brandDiscountService.Get(brandDiscount.Id);
            brandDiscountRepositoryMock.VerifyAll();
            Assert.AreEqual(brandDiscountResult.Id, brandDiscount.Id);
        }
        [TestMethod]
        public void CreateBrandDiscountOkTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            Brand oneBrand = brandDiscount.Brand;

            var brandDiscountRepositoryMock = new Mock<IBrandDiscountRepository>(MockBehavior.Strict);
            brandDiscountRepositoryMock.Setup(bd => bd.Exists(brandDiscount)).Returns(false);
            brandDiscountRepositoryMock.Setup(bd => bd.Add(brandDiscount));
            brandDiscountRepositoryMock.Setup(bd => bd.Save());

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);
            brandRepositoryMock.Setup(b => b.Exists(oneBrand)).Returns(true);


            var brandDiscountService = new BrandDiscountLogic(brandDiscountRepositoryMock.Object
                , brandRepositoryMock.Object);

            var brandDiscountResult = brandDiscountService.Create(brandDiscount);
            brandDiscountRepositoryMock.VerifyAll();
            Assert.AreEqual(brandDiscountResult.Id, brandDiscount.Id);
        }
        [TestMethod]
        public void UpdateBrandDiscountOkTest()
        {
            BrandDiscount oldBrandDiscount = InitOneBrandDiscountComplete();
            BrandDiscount newBrandDiscount = InitAnotherBrandDiscountComplete();

            var brandDiscountRepositoryMock = new Mock<IBrandDiscountRepository>(MockBehavior.Strict);
            brandDiscountRepositoryMock.Setup(bd => bd.Get(It.IsAny<Guid>())).Returns(oldBrandDiscount);
            brandDiscountRepositoryMock.Setup(bd => bd.Update(It.IsAny<BrandDiscount>(), It.IsAny<BrandDiscount>()));
            brandDiscountRepositoryMock.Setup(bd => bd.Save());


            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);

            var brandDiscountService = new BrandDiscountLogic(brandDiscountRepositoryMock.Object
                , brandRepositoryMock.Object);
            brandDiscountService.Update(oldBrandDiscount.Id, newBrandDiscount);

            brandDiscountRepositoryMock.VerifyAll();
        }
        [TestMethod]
        public void RemoveBrandDiscountOkTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();

            var brandDiscountRepositoryMock = new Mock<IBrandDiscountRepository>(MockBehavior.Strict);
            brandDiscountRepositoryMock.Setup(bd => bd.Remove(brandDiscount));
            brandDiscountRepositoryMock.Setup(bd => bd.Save());

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);

            var brandDiscountService = new BrandDiscountLogic(brandDiscountRepositoryMock.Object, brandRepositoryMock.Object);

            brandDiscountService.Remove(brandDiscount);

            brandDiscountRepositoryMock.VerifyAll();
        }
        [ExpectedException(typeof(RepeatedObjectException))]
        [TestMethod]
        public void AddRepeatedBrandDiscountTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            Brand oneBrand = brandDiscount.Brand;

            var brandDiscountRepositoryMock = new Mock<IBrandDiscountRepository>(MockBehavior.Strict);
            brandDiscountRepositoryMock.Setup(bd => bd.Exists(brandDiscount)).Returns(true);
            brandDiscountRepositoryMock.Setup(bd => bd.Save());


            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);
            brandRepositoryMock.Setup(b => b.Exists(oneBrand)).Returns(true);

            var brandDiscountService = new BrandDiscountLogic(brandDiscountRepositoryMock.Object
                , brandRepositoryMock.Object);

            brandDiscountService.Create(brandDiscount);
        }

        [ExpectedException(typeof(InvalidDiscountException))]
        [TestMethod]
        public void AddNullBrandDiscountTest()
        {
            BrandDiscount brandDiscount = null;

            var brandDiscountRepositoryMock = new Mock<IBrandDiscountRepository>(MockBehavior.Strict);

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);

            var brandDiscountService = new BrandDiscountLogic(brandDiscountRepositoryMock.Object, brandRepositoryMock.Object);

            brandDiscountService.Create(brandDiscount);
        }
        [ExpectedException(typeof(IncorrectNameException))]
        [TestMethod]
        public void AddBrandDiscountWithEmptyNameTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            brandDiscount.Name = "";

            var brandDiscountRepositoryMock = new Mock<IBrandDiscountRepository>(MockBehavior.Strict);

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);

            var brandDiscountService = new BrandDiscountLogic(brandDiscountRepositoryMock.Object, brandRepositoryMock.Object);

            brandDiscountService.Create(brandDiscount);
        }
        [ExpectedException(typeof(IncorrectBrandForDiscount))]
        [TestMethod]
        public void AddBrandDiscountWithNullBrandTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            brandDiscount.Brand = null;

            var brandDiscountRepositoryMock = new Mock<IBrandDiscountRepository>(MockBehavior.Strict);

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);

            var brandDiscountService = new BrandDiscountLogic(brandDiscountRepositoryMock.Object, brandRepositoryMock.Object);

            brandDiscountService.Create(brandDiscount);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddBrandDiscountWithoutMinProductsForPromotionTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            brandDiscount.MinProductsForPromotion = 0;

            var brandDiscountRepositoryMock = new Mock<IBrandDiscountRepository>(MockBehavior.Strict);

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);

            var brandDiscountService = new BrandDiscountLogic(brandDiscountRepositoryMock.Object, brandRepositoryMock.Object);

            brandDiscountService.Create(brandDiscount);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddBrandDiscountWithotMinProductsToBeFreeTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            brandDiscount.NumberOfProductsForFree = 0;

            var brandDiscountRepositoryMock = new Mock<IBrandDiscountRepository>(MockBehavior.Strict);

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);

            var brandDiscountService = new BrandDiscountLogic(brandDiscountRepositoryMock.Object, brandRepositoryMock.Object);

            brandDiscountService.Create(brandDiscount);
        }
        [TestMethod]
        public void AddBrandDiscountWithotProductsToBeDiscountedTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            brandDiscount.ProductToBeDiscounted = "";

            var brandDiscountRepositoryMock = new Mock<IBrandDiscountRepository>(MockBehavior.Strict);
            brandDiscountRepositoryMock.Setup(bd => bd.Exists(It.IsAny<BrandDiscount>())).Returns(false);
            brandDiscountRepositoryMock.Setup(bd => bd.Add(brandDiscount));
            brandDiscountRepositoryMock.Setup(bd => bd.Save());
            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);
            brandRepositoryMock.Setup(b => b.Exists(It.IsAny<Brand>())).Returns(true);

            var brandDiscountService = new BrandDiscountLogic(brandDiscountRepositoryMock.Object, brandRepositoryMock.Object);


            brandDiscountService.Create(brandDiscount);
        }
    }
}
