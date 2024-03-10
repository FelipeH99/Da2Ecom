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
    public class QuantityDiscountControllerTest : TestSetUps
    {
        [TestMethod]
        public void PutOkTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();

            List<QuantityDiscountModelRead> quantityDiscountModelReadResultList = new List<QuantityDiscountModelRead>();
            quantityDiscountModelReadResultList.Add(QuantityDiscountModelRead.ToModel(quantityDiscount));

            QuantityDiscountUpdateModelWrite discountModel = InitOneQuantityDiscountUpdateModelWrite(quantityDiscount);

            var quantityDiscountServiceMock = new Mock<IQuantityDiscountLogic>(MockBehavior.Strict);
            quantityDiscountServiceMock.Setup(d => d.Update(It.IsAny<Guid>(), It.IsAny<QuantityDiscount>())).Returns(quantityDiscount);

            var discountController = new QuantityDiscountController(quantityDiscountServiceMock.Object);

            var result = discountController.Put(quantityDiscount.Id, discountModel);
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            quantityDiscountServiceMock.VerifyAll();
            Assert.AreEqual(value, "Se actualizo el descuento correctamente.");
        }
        [TestMethod]
        public void PostOkTest()
        {
            QuantityDiscount discount = InitOneQuantityDiscountComplete();
            List<QuantityDiscountModelRead> quantityModelCreateReadResultList = new List<QuantityDiscountModelRead>();
            quantityModelCreateReadResultList.Add(QuantityDiscountModelRead.ToModel(discount));
            QuantityDiscountModelWrite discountModelWrite = InitOneQuantityDiscountModelWrite(discount);

            var quantityServiceMock = new Mock<IQuantityDiscountLogic>(MockBehavior.Strict);
            quantityServiceMock.Setup(p => p.Create(It.IsAny<QuantityDiscount>())).Returns(discount);
            var quantityController = new QuantityDiscountController(quantityServiceMock.Object);

            var result = quantityController.Post(discountModelWrite);
            var createdResult = result as CreatedAtRouteResult;
            var value = createdResult.Value;
            quantityServiceMock.VerifyAll();
            Assert.AreEqual(value, QuantityDiscountModelRead.ToModel(discount));
        }

        [TestMethod]
        public void DeleteOkTest()
        {
            QuantityDiscount discount = InitOneQuantityDiscountComplete();
            discount.Id = Guid.NewGuid();

            var discountServiceMock = new Mock<IQuantityDiscountLogic>(MockBehavior.Strict);
            discountServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(discount);
            discountServiceMock.Setup(p => p.Remove(It.IsAny<QuantityDiscount>()));

            var discountController = new QuantityDiscountController(discountServiceMock.Object);

            var result = discountController.Delete(discount.Id);
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            discountServiceMock.VerifyAll();
            Assert.AreEqual(value, "Se elimino el descuento " + discount.Name);
        }

        [TestMethod]
        public void DeleteInvalidOkTest()
        {
            QuantityDiscount discount = InitOneQuantityDiscountComplete();
            discount.Id = Guid.NewGuid();
            QuantityDiscount secondDiscount = null;

            var discountServiceMock = new Mock<IQuantityDiscountLogic>(MockBehavior.Strict);
            discountServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(secondDiscount);

            var discountController = new QuantityDiscountController(discountServiceMock.Object);

            var result = discountController.Delete(discount.Id);
            var okResult = result as ObjectResult;
            var value = okResult.Value;
            discountServiceMock.VerifyAll();
            Assert.AreEqual(value, "No existe el descuento con Id: " + discount.Id);
        }
    }
}
