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
    public class PercentageDiscountControllerTest : TestSetUps
    {
        [TestMethod]
        public void PutOkTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();

            List<PercentageDiscountModelRead> percentageDiscountModelReadResultList = new List<PercentageDiscountModelRead>();
            percentageDiscountModelReadResultList.Add(PercentageDiscountModelRead.ToModel(percentageDiscount));

            PercentageDiscountUpdateModelWrite discountModel = InitOnePercentageDiscountUpdateModelUpdateWrite(percentageDiscount);

            var percentageDiscountServiceMock = new Mock<IPercentageDiscountLogic>(MockBehavior.Strict);
            percentageDiscountServiceMock.Setup(d => d.Update(It.IsAny<Guid>(), It.IsAny<PercentageDiscount>())).Returns(percentageDiscount);

            var discountController = new PercentageDiscountController(percentageDiscountServiceMock.Object);

            var result = discountController.Put(percentageDiscount.Id, discountModel);
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            percentageDiscountServiceMock.VerifyAll();
            Assert.AreEqual(value, "Se actualizo el descuento correctamente.");
        }
        [TestMethod]
        public void PostOkTest()
        {
            PercentageDiscount discount = InitOnePercentageDiscountComplete();
            List<PercentageDiscountModelRead> percentageModelCreateReadResultList = new List<PercentageDiscountModelRead>();
            percentageModelCreateReadResultList.Add(PercentageDiscountModelRead.ToModel(discount));
            PercentageDiscountModelWrite discountModelWrite = InitOnePercentageModelWriteComplete(discount);

            var percentageServiceMock = new Mock<IPercentageDiscountLogic>(MockBehavior.Strict);
            percentageServiceMock.Setup(p => p.Create(It.IsAny<PercentageDiscount>())).Returns(discount);
            var percentageController = new PercentageDiscountController(percentageServiceMock.Object);

            var result = percentageController.Post(discountModelWrite);
            var createdResult = result as CreatedAtRouteResult;
            var value = createdResult.Value;
            percentageServiceMock.VerifyAll();
            Assert.AreEqual(value, PercentageDiscountModelRead.ToModel(discount));
        }
        [TestMethod]
        public void DeleteOkTest()
        {
            PercentageDiscount discount = InitOnePercentageDiscountComplete();
            discount.Id = Guid.NewGuid();

            var discountServiceMock = new Mock<IPercentageDiscountLogic>(MockBehavior.Strict);
            discountServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(discount);
            discountServiceMock.Setup(p => p.Remove(It.IsAny<PercentageDiscount>()));

            var discountController = new PercentageDiscountController(discountServiceMock.Object);

            var result = discountController.Delete(discount.Id);
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            discountServiceMock.VerifyAll();
            Assert.AreEqual(value, "Se elimino el descuento " + discount.Name);
        }

        [TestMethod]
        public void DeleteInvalidOkTest()
        {
            PercentageDiscount discount = InitOnePercentageDiscountComplete();
            discount.Id = Guid.NewGuid();
            PercentageDiscount secondDiscount = null;

            var discountServiceMock = new Mock<IPercentageDiscountLogic>(MockBehavior.Strict);
            discountServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(secondDiscount);

            var discountController = new PercentageDiscountController(discountServiceMock.Object);

            var result = discountController.Delete(discount.Id);
            var okResult = result as ObjectResult;
            var value = okResult.Value;
            discountServiceMock.VerifyAll();
            Assert.AreEqual(value, "No existe el descuento con Id: " + discount.Id);
        }
    }
}
