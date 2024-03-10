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
    public class ColorDiscountControllerTest : TestSetUps
    {
        [TestMethod]
        public void PutOkTest()
        {
            ColorDiscount colorDiscount = InitOneColorDiscountComplete();
            Entities.Color color = colorDiscount.Color;

            List<ColorDiscountModelRead> colorDiscountModelReadResultList = new List<ColorDiscountModelRead>();
            colorDiscountModelReadResultList.Add(ColorDiscountModelRead.ToModel(colorDiscount));

            ColorDiscountUpdateModelWrite discountModel = InitOneColorDiscountUpdateModelWrite(colorDiscount);

            var colorDiscountServiceMock = new Mock<IColorDiscountLogic>(MockBehavior.Strict);
            colorDiscountServiceMock.Setup(d => d.Update(It.IsAny<Guid>(), It.IsAny<ColorDiscount>())).Returns(colorDiscount);

            var colorServiceMock = new Mock<IColorLogic>(MockBehavior.Strict);
            colorServiceMock.Setup(d => d.Get(It.IsAny<Guid>())).Returns(color);

            var discountController = new ColorDiscountController(colorDiscountServiceMock.Object, colorServiceMock.Object);

            var result = discountController.Put(colorDiscount.Id, discountModel);
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            colorDiscountServiceMock.VerifyAll();
            Assert.AreEqual(value, "Se actualizo el descuento correctamente.");
        }
        [TestMethod]
        public void PostOkTest()
        {
            ColorDiscount discount = InitOneColorDiscountComplete();
            Entities.Color color = discount.Color;

            List<ColorDiscountModelRead> colorModelCreateReadResultList = new List<ColorDiscountModelRead>();
            colorModelCreateReadResultList.Add(ColorDiscountModelRead.ToModel(discount));
            ColorDiscountModelWrite discountModelWrite = InitOneColorDiscountModelWrite(discount);

            var colorServiceMock = new Mock<IColorDiscountLogic>(MockBehavior.Strict);
            colorServiceMock.Setup(c => c.Create(It.IsAny<ColorDiscount>())).Returns(discount);

            var colorMock = new Mock<IColorLogic>(MockBehavior.Strict);
            colorMock.Setup(c => c.Get(It.IsAny<Guid>())).Returns(color);

            var colorController = new ColorDiscountController(colorServiceMock.Object, colorMock.Object);

            var result = colorController.Post(discountModelWrite);
            var createdResult = result as CreatedAtRouteResult;
            var value = createdResult.Value;
            colorServiceMock.VerifyAll();
            Assert.AreEqual(value, ColorDiscountModelRead.ToModel(discount));
        }
        [TestMethod]
        public void DeleteOkTest()
        {
            ColorDiscount discount = InitOneColorDiscountComplete();
            discount.Id = Guid.NewGuid();

            var discountServiceMock = new Mock<IColorDiscountLogic>(MockBehavior.Strict);
            discountServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(discount);
            discountServiceMock.Setup(p => p.Remove(It.IsAny<ColorDiscount>()));

            var colorMock = new Mock<IColorLogic>(MockBehavior.Strict);
            colorMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(discount.Color);

            var colorController = new ColorDiscountController(discountServiceMock.Object, colorMock.Object);

            var result = colorController.Delete(discount.Id);
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            discountServiceMock.VerifyAll();
            Assert.AreEqual(value, "Se elimino el descuento " + discount.Name);
        }

        [TestMethod]
        public void DeleteInvalidOkTest()
        {
            ColorDiscount discount = InitOneColorDiscountComplete();
            discount.Id = Guid.NewGuid();
            ColorDiscount secondDiscount = null;

            var discountServiceMock = new Mock<IColorDiscountLogic>(MockBehavior.Strict);
            discountServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(secondDiscount);

            var colorMock = new Mock<IColorLogic>(MockBehavior.Strict);

            var colorController = new ColorDiscountController(discountServiceMock.Object, colorMock.Object);

            var result = colorController.Delete(discount.Id);
            var okResult = result as ObjectResult;
            var value = okResult.Value;
            discountServiceMock.VerifyAll();
            Assert.AreEqual(value, "No existe el descuento con Id: " + discount.Id);
        }
    }
}
