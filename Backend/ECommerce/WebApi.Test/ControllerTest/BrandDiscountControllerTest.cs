using BusinessLogic.Interface;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestSetUp;
using WebAPI.Controllers;
using WebAPI.Models.Read.Discounts;
using WebAPI.Models.Write.Discounts;

namespace WebApi.Test.ControllerTest
{
    [TestClass]
    public class BrandDiscountControllerTest : TestSetUps
    {
        [TestMethod]
        public void PutOkTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            Brand brand = brandDiscount.Brand;

            List<BrandDiscountModelRead> brandDiscountModelReadResultList = new List<BrandDiscountModelRead>();
            brandDiscountModelReadResultList.Add(BrandDiscountModelRead.ToModel(brandDiscount));

            BrandDiscountUpdateModelWrite discountModel = InitOneBrandDiscountUpdateModelWrite(brandDiscount);

            var brandDiscountServiceMock = new Mock<IBrandDiscountLogic>(MockBehavior.Strict);
            brandDiscountServiceMock.Setup(d => d.Update(It.IsAny<Guid>(), It.IsAny<BrandDiscount>())).Returns(brandDiscount);

            var brandServiceMock = new Mock<IBrandLogic>(MockBehavior.Strict);
            brandServiceMock.Setup(d => d.Get(It.IsAny<Guid>())).Returns(brand);

            var discountController = new BrandDiscountController(brandDiscountServiceMock.Object, brandServiceMock.Object);

            var result = discountController.Put(brandDiscount.Id, discountModel);
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            brandDiscountServiceMock.VerifyAll();
            Assert.AreEqual(value, "Se actualizo el descuento correctamente.");
        }
        [TestMethod]
        public void PostOkTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            Brand brand = brandDiscount.Brand;

            List<BrandDiscountModelRead> brandModelCreateReadResultList = new List<BrandDiscountModelRead>();
            brandModelCreateReadResultList.Add(BrandDiscountModelRead.ToModel(brandDiscount));
            BrandDiscountModelWrite discountModelWrite = InitOneBrandDiscountModelWrite(brandDiscount);

            var brandDiscountServiceMock = new Mock<IBrandDiscountLogic>(MockBehavior.Strict);
            brandDiscountServiceMock.Setup(d => d.Create(It.IsAny<BrandDiscount>())).Returns(brandDiscount);

            var brandServiceMock = new Mock<IBrandLogic>(MockBehavior.Strict);
            brandServiceMock.Setup(d => d.Get(It.IsAny<Guid>())).Returns(brand);

            var discountController = new BrandDiscountController(brandDiscountServiceMock.Object, brandServiceMock.Object);

            var result = discountController.Post(discountModelWrite);
            var createdResult = result as CreatedAtRouteResult;
            var value = createdResult.Value;
            brandServiceMock.VerifyAll();
            Assert.AreEqual(value, BrandDiscountModelRead.ToModel(brandDiscount));
        }
        [TestMethod]
        public void DeleteOkTest()
        {
            BrandDiscount discount = InitOneBrandDiscountComplete();
            discount.Id = Guid.NewGuid();

            var discountServiceMock = new Mock<IBrandDiscountLogic>(MockBehavior.Strict);
            discountServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(discount);
            discountServiceMock.Setup(p => p.Remove(It.IsAny<BrandDiscount>()));

            var brandMock = new Mock<IBrandLogic>(MockBehavior.Strict);

            var brandController = new BrandDiscountController(discountServiceMock.Object, brandMock.Object);

            var result = brandController.Delete(discount.Id);
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            discountServiceMock.VerifyAll();
            Assert.AreEqual(value, "Se elimino el descuento " + discount.Name);
        }

        [TestMethod]
        public void DeleteInvalidOkTest()
        {
            BrandDiscount discount = InitOneBrandDiscountComplete();
            discount.Id = Guid.NewGuid();
            BrandDiscount secondDiscount = null;

            var discountServiceMock = new Mock<IBrandDiscountLogic>(MockBehavior.Strict);
            discountServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(secondDiscount);

            var brandMock = new Mock<IBrandLogic>(MockBehavior.Strict);

            var brandController = new BrandDiscountController(discountServiceMock.Object, brandMock.Object);

            var result = brandController.Delete(discount.Id);
            var okResult = result as ObjectResult;
            var value = okResult.Value;
            discountServiceMock.VerifyAll();
            Assert.AreEqual(value, "No existe el descuento con Id: " + discount.Id);
        }
    }
}
