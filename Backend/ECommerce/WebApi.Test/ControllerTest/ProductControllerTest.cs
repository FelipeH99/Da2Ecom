using TestSetUp;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using BusinessLogic.Interface;
using WebAPI.Models.Read;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Models.Write;
using DataAccess.Interface;

namespace WebApi.Test.ControllerTest
{
    [TestClass]
    public class ProductControllerTest : TestSetUps
    {
        [TestMethod]
        public void GetAllOkTest()
        {
            Product oneProduct = InitOneProductComplete();
            Color oneColor = InitOneColorComplete();
            SearchResult oneSearchResult = InitOneSearchResultComplete();
            ProductInformation productInfo = InitOneProductInformationComplete();
            List<Product> products = new List<Product>() { oneProduct };
            
            ProductModelRead productModelRead = new ProductModelRead();
            productModelRead.BrandName = oneProduct.Name;
            productModelRead.BrandName = oneProduct.Brand.Name;
            productModelRead.Price = oneProduct.Price;
            productModelRead.Category = oneProduct.ProductCategory.ToString();
            productModelRead.ProductName = "Remera Hombre";

            List<ProductModelRead> productModelResultList = new List<ProductModelRead>();
            productModelResultList.Add(productModelRead);
            List<SearchResult> searchResults = new List<SearchResult>();
            searchResults.Add(oneSearchResult);

            var brandServiceMock = new Mock<IBrandRepository>((MockBehavior.Strict));
            brandServiceMock.Setup(b => b.Get(It.IsAny<Guid>())).Returns(oneProduct.Brand);

            var colorServiceMock = new Mock<IColorRepository>((MockBehavior.Strict));
            colorServiceMock.Setup(c => c.Get(It.IsAny<Guid>())).Returns(oneColor);

            var productServiceMock = new Mock<IProductLogic>(MockBehavior.Strict);
            productServiceMock.Setup(p => p.CreateSearchResult(It.IsAny<ProductInformation>())).Returns(searchResults);
            
            var productController = new ProductController(productServiceMock.Object);

            var result = productController.Get(productInfo);
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            IEnumerable<ProductModelRead> valueEnumerable = value as IEnumerable<ProductModelRead>;
            List<ProductModelRead> productModelList = valueEnumerable.ToList();
            productServiceMock.VerifyAll();
            Assert.IsTrue(productModelResultList.SequenceEqual(productModelList));
        }
        [TestMethod]
        public void GetByIdOkTest()
        {
            Product product = InitOneProductComplete();

            var productServiceMock = new Mock<IProductLogic>(MockBehavior.Strict);
            productServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(product);

            var productController = new ProductController(productServiceMock.Object);

            var result = productController.Get(product.Id);
            var okResult = result as ObjectResult;
            var value = okResult.Value as ProductModel;
            productServiceMock.VerifyAll();
            Assert.AreEqual(value, ProductModel.ToModel(product));
        }
        [TestMethod]
        public void PutOkTest()
        {
            Product product = InitOneProductComplete();
            SearchResult oneSearchResult = InitOneSearchResultComplete();


            List<ProductModelRead> productModelReadResultList = new List<ProductModelRead>();
            productModelReadResultList.Add(ProductModelRead.ToModel(oneSearchResult));
            
            ProductModelWrite productModel = InitOneProductModelWriteComplete(product);
             
            var productServiceMock = new Mock<IProductLogic>(MockBehavior.Strict);
            productServiceMock.Setup(p => p.Update(It.IsAny<Guid>(), It.IsAny<Product>())).Returns(product);
            
            var productController = new ProductController(productServiceMock.Object);

            var result = productController.Put(product.Id, productModel);
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            productServiceMock.VerifyAll();
            Assert.AreEqual(value, "Se actualizo el producto correctamente.");
        }
        [TestMethod]
        public void PostOkTest()
        {
            Product product = InitOneProductComplete();
            List<ProductModelCreatedRead> productModelCreateReadResultList = new List<ProductModelCreatedRead>();
            productModelCreateReadResultList.Add(ProductModelCreatedRead.ToModel(product));
            ProductModelWrite productModelWrite = InitOneProductModelWriteComplete(product);

            var productServiceMock = new Mock<IProductLogic>(MockBehavior.Strict);
            productServiceMock.Setup(p => p.Create(It.IsAny<Product>())).Returns(product);
            var productController = new ProductController(productServiceMock.Object);

            var result = productController.Post(productModelWrite);
            var createdResult = result as CreatedAtRouteResult;
            var value = createdResult.Value;
            productServiceMock.VerifyAll();
            Assert.AreEqual(value, ProductModelCreatedRead.ToModel(product));
        }
        [TestMethod]
        public void DeleteOkTest()
        {
            Product product = InitOneProductComplete();
            product.Id = Guid.NewGuid();

            var productServiceMock = new Mock<IProductLogic>(MockBehavior.Strict);
            productServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(product);
            productServiceMock.Setup(p => p.Remove(It.IsAny<Product>()));

            var productController = new ProductController(productServiceMock.Object);

            var result = productController.Delete(product.Id);
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            productServiceMock.VerifyAll();
            Assert.AreEqual(value, "Se elimino el producto " + product.Name);
        }

        [TestMethod]
        public void DeleteInvalidOkTest()
        {
            Product product = InitOneProductComplete();
            product.Id = Guid.NewGuid();
            Product secondProduct = null;

            var productServiceMock = new Mock<IProductLogic>(MockBehavior.Strict);
            productServiceMock.Setup(p => p.Get(It.IsAny<Guid>())).Returns(secondProduct);

            var productController = new ProductController(productServiceMock.Object);

            var result = productController.Delete(product.Id);
            var okResult = result as ObjectResult;
            var value = okResult.Value;
            productServiceMock.VerifyAll();
            Assert.AreEqual(value, "No existe el producto con Id: " + product.Id);
        }

    }
}
