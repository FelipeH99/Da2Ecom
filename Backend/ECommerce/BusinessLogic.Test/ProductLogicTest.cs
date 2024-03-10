using Exceptions;
using Moq;
using TestSetUp;
using Entities;
using DataAccess.Interface;

namespace BusinessLogic.Test
{
    [TestClass]
    public class ProductLogicTest : TestSetUps
    {
        [TestMethod]
        public void GetProductsOkTest()
        {
            List<Product> productList = new List<Product>();
            Product oneProduct = InitOneProductComplete();
            productList.Add(oneProduct);

            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.Get()).Returns(productList);

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);

            var productService = new ProductLogic(productRepositoryMock.Object,brandRepositoryMock.Object,
                colorRepositoryMock.Object);

            var productResult = productService.Get().ToList<Product>();
            productRepositoryMock.VerifyAll();
            Assert.IsTrue(productResult.Any(l => l.Id == oneProduct.Id));
        }
        [TestMethod]
        public void GetOneProductByIdTest()
        {
            Product oneProduct = InitOneProductComplete();

            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.Get(oneProduct.Id)).Returns(oneProduct);

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);

            var productService = new ProductLogic(productRepositoryMock.Object, brandRepositoryMock.Object,
                colorRepositoryMock.Object);

            var productResult = productService.Get(oneProduct.Id);;
            productRepositoryMock.VerifyAll();
            Assert.AreEqual(productResult.Id, oneProduct.Id);
        }
        [TestMethod]
        public void CreateProductOkTest()
        {
            Product oneProduct = InitOneProductComplete();
            Brand oneBrand = InitOneBrandComplete();
            Color oneColor = InitOneColorComplete();

            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.Exists(oneProduct)).Returns(false);
            productRepositoryMock.Setup(p => p.Add(oneProduct));
            productRepositoryMock.Setup(p => p.Save());

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);
            brandRepositoryMock.Setup(b => b.Exists(oneBrand)).Returns(true);
            brandRepositoryMock.Setup(b => b.Get(It.IsAny<Guid>())).Returns(oneBrand);

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);
            colorRepositoryMock.Setup(c => c.Get(It.IsAny<Guid>())).Returns(oneColor);

            var productService = new ProductLogic(productRepositoryMock.Object, brandRepositoryMock.Object, 
                colorRepositoryMock.Object);

            var productResult = productService.Create(oneProduct);
            productRepositoryMock.VerifyAll();
            Assert.AreEqual(productResult.Id, oneProduct.Id);
        }
        [TestMethod]
        public void UpdateProductOkTest()
        {
            Product oldProduct = InitOneProductComplete();
            Product newProduct = InitSecondProductComplete();

            Color oneColor = InitOneColorComplete();

            Brand oneBrand = newProduct.Brand;

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);
            brandRepositoryMock.Setup(b => b.Get(It.IsAny<Guid>())).Returns(oldProduct.Brand);
            brandRepositoryMock.Setup(b => b.Get(It.IsAny<Guid>())).Returns(newProduct.Brand);


            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);
            colorRepositoryMock.Setup(c => c.Get(It.IsAny<Guid>())).Returns(oneColor);


            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(oldProduct);
            productRepositoryMock.Setup(p => p.Update(It.IsAny<Product>(), It.IsAny<Product>()));
            productRepositoryMock.Setup(p => p.Save());

            var productService = new ProductLogic(productRepositoryMock.Object, brandRepositoryMock.Object, colorRepositoryMock.Object);

            productService.Update(oldProduct.Id, newProduct);

            productRepositoryMock.VerifyAll();
        }
        [TestMethod]
        public void RemoveProductOkTest()
        {
            Product oneProduct = InitOneProductComplete();

            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.Remove(oneProduct));
            productRepositoryMock.Setup(p => p.Save());

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);

            var productService = new ProductLogic(productRepositoryMock.Object, brandRepositoryMock.Object,
                colorRepositoryMock.Object);

            productService.Remove(oneProduct);

            productRepositoryMock.VerifyAll();
        }
        [ExpectedException(typeof(RepeatedObjectException))]
        [TestMethod]
        public void AddRepeatedProductTest()
        {
            Product oneProduct = InitOneProductComplete();
            Brand oneBrand = InitOneBrandComplete();
            Color oneColor = InitOneColorComplete();

            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.Exists(oneProduct)).Returns(true);
            productRepositoryMock.Setup(p => p.Save());

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);
            brandRepositoryMock.Setup(b => b.Exists(It.IsAny<Brand>())).Returns(true);
            brandRepositoryMock.Setup(b => b.Get(It.IsAny<Guid>())).Returns(oneProduct.Brand);

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);
            colorRepositoryMock.Setup(c => c.Get(It.IsAny<Guid>())).Returns(oneColor);

            var productService = new ProductLogic(productRepositoryMock.Object, brandRepositoryMock.Object,
                colorRepositoryMock.Object);

            productService.Create(oneProduct);
        }
        [ExpectedException(typeof(InvalidProductException))]
        [TestMethod]
        public void AddNullProductTest()
        {
            Product oneProduct = null;

            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);

            var productService = new ProductLogic(productRepositoryMock.Object, brandRepositoryMock.Object,
                colorRepositoryMock.Object);
            productService.Create(oneProduct);
        }
        [ExpectedException(typeof(IncorrectNameException))]
        [TestMethod]
        public void AddProductWithEmptyNameTest()
        {
            Product oneProduct = InitOneProductComplete();
            Color oneColor = InitOneColorComplete();
            oneProduct.Name = "";

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);
            brandRepositoryMock.Setup(b => b.Get(It.IsAny<Guid>())).Returns(oneProduct.Brand);

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);
            colorRepositoryMock.Setup(c => c.Get(It.IsAny<Guid>())).Returns(oneColor);

            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            var productService = new ProductLogic(productRepositoryMock.Object,brandRepositoryMock.Object,colorRepositoryMock.Object);
            productService.Create(oneProduct);
        }
        [ExpectedException(typeof(IncorrectRequestException))]
        [TestMethod]
        public void AddProductWithInvalidPriceTest()
        {
            Product oneProduct = InitOneProductComplete();
            Color oneColor = InitOneColorComplete();
            oneProduct.Price = -98.20;

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);
            brandRepositoryMock.Setup(b => b.Get(It.IsAny<Guid>())).Returns(oneProduct.Brand);

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);
            colorRepositoryMock.Setup(c => c.Get(It.IsAny<Guid>())).Returns(oneColor);

            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            var productService = new ProductLogic(productRepositoryMock.Object, brandRepositoryMock.Object, colorRepositoryMock.Object);
            productService.Create(oneProduct);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddProductWithInvalidDescriptionTest()
        {
            Product oneProduct = InitOneProductComplete();
            Color oneColor = InitOneColorComplete();
            oneProduct.Description = "";

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);
            brandRepositoryMock.Setup(b => b.Get(It.IsAny<Guid>())).Returns(oneProduct.Brand);

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);
            colorRepositoryMock.Setup(c => c.Get(It.IsAny<Guid>())).Returns(oneColor);

            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            var productService = new ProductLogic(productRepositoryMock.Object, brandRepositoryMock.Object, colorRepositoryMock.Object);
            productService.Create(oneProduct);
        }
        [ExpectedException(typeof(IncorrectBrandForProductException))]
        [TestMethod]
        public void AddProductWithNullBrandTest()
        {
            Product oneProduct = InitOneProductComplete();
            oneProduct.Brand = null;
            Color oneColor = InitOneColorComplete();

            var brandRepositoryMock = new Mock<IBrandRepository>(MockBehavior.Strict);
            brandRepositoryMock.Setup(b => b.Exists(It.IsAny<Brand>())).Returns(false);
            brandRepositoryMock.Setup(b => b.Get(It.IsAny<Guid>())).Returns(oneProduct.Brand);

            var colorRepositoryMock = new Mock<IColorRepository>(MockBehavior.Strict);
            colorRepositoryMock.Setup(c => c.Get(It.IsAny<Guid>())).Returns(oneColor);

            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            var productService = new ProductLogic(productRepositoryMock.Object, brandRepositoryMock.Object, colorRepositoryMock.Object);
            productService.Create(oneProduct);
        }
    }
}
