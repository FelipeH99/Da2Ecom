using Entities;
using TestSetUp;
using Exceptions;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Test
{
    [TestClass]
    public class ProductRepositoryTest : TestSetUps
    {
        private DbContextOptions options;
        private DbContext context;

        [TestInitialize]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "ECommerce").Options;
            context = new DataBaseContext(options);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            context.Database.EnsureDeleted();
        }
        [TestMethod]
        public void GetProductsOkTest()
        {
            Product oneProduct = InitOneProductComplete();
            context.Set<Product>().Add(oneProduct);
            context.SaveChanges();

            ProductRepository productRepository = new ProductRepository(context);

            int actualResult = productRepository.Get().Count();
            Assert.AreEqual(1, actualResult);
        }
        [TestMethod]
        public void GetProductByIdOkTest()
        {
            Product oneProduct = InitOneProductComplete();
            context.Set<Product>().Add(oneProduct);
            context.SaveChanges();

            ProductRepository productRepository = new ProductRepository(context);

            Product productById = productRepository.Get(oneProduct.Id);
            Assert.AreEqual(productById, oneProduct);
        }
        [TestMethod]
        public void AddProductToRepositoryOkTest()
        {
            Product oneProduct = InitOneProductComplete();

            ProductRepository productRepository = new ProductRepository(context);
            productRepository.Add(oneProduct);
            context.SaveChanges();

            Assert.AreEqual(productRepository.Get().ToList().Count, 1);
        }
        [TestMethod]
        public void ProductRepositoryExistsOkTest()
        {
            ProductRepository productRepository = new ProductRepository(context);

            Product oneProduct = InitOneProductComplete();

            context.Set<Product>().Add(oneProduct);
            context.SaveChanges();

            Assert.IsTrue(productRepository.Exists(oneProduct));
        }
        [TestMethod]
        public void UpdateProductTest()
        {
            ProductRepository productRepository = new ProductRepository(context);

            Product oneProduct = InitOneProductComplete();
            Product anotherProduct = InitSecondProductComplete();

            productRepository.Add(oneProduct);
            productRepository.Save();


            productRepository.Update(oneProduct, anotherProduct);
            Assert.AreEqual(productRepository.Get(oneProduct.Id), anotherProduct);
        }
        [TestMethod]
        public void RemoveProductOkTest()
        {
            ProductRepository productRepository = new ProductRepository(context);

            Product oneProduct = InitOneProductComplete();

            productRepository.Add(oneProduct);
            productRepository.Save();

            productRepository.Remove(oneProduct);
            productRepository.Save();

            Assert.AreEqual(productRepository.Get().Count(), 0);
        }

        [ExpectedException(typeof(IncorrectRequestException))]
        [TestMethod]
        public void RemoveProductInvalidOkTest()
        {
            ProductRepository productRepository = new ProductRepository(context);

            Product oneProduct = InitOneProductComplete();

            productRepository.Save();
            productRepository.Remove(oneProduct);
        }
    }
}
