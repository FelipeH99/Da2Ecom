using Entities.Enums;
using TestSetUp;

namespace Entities.Test
{
    [TestClass]
    public class ProductTest : TestSetUps
    {
        [TestMethod]
        public void CreateProductOkTest()
        {
            Product oneProduct = new Product();

            Assert.IsNotNull(oneProduct);
        }
        [TestMethod]
        public void CreateProductWithPriceOKTest() 
        {
            Product oneProduct = new Product();
            oneProduct.Price = 12.5;

            Assert.AreEqual(oneProduct.Price, 12.5);
        }
        [TestMethod]
        public void CreateProductWithStockOKTest()
        {
            Product oneProduct = new Product();
            oneProduct.Stock = 5;

            Assert.AreEqual(oneProduct.Stock, 5);
        }
        [TestMethod]
        public void CreateProductWithNameOKTest() 
        {
            Product oneProduct = new Product();
            oneProduct.Name = "Desodorante";

            Assert.AreEqual(oneProduct.Name, "Desodorante");
        }
        [TestMethod]
        public void CreateProductWithDescriptionOKTest()
        {
            Product oneProduct = new Product();
            string oneDescription = "Un desodorante es una sustancia que se aplica al cuerpo principalmente en las axilas y los pies," +
                " para reducir el olor de la transpiración.";
            oneProduct.Description = oneDescription;

            Assert.AreEqual(oneProduct.Description, oneDescription);
        }
        [TestMethod]
        public void CreateProductWithCategoryOKTest()
        {
            Product oneProduct = new Product();
            ProductCategory oneCategory = ProductCategory.Abrigos;
            oneProduct.ProductCategory = oneCategory;

            Assert.AreEqual(ProductCategory.Abrigos, oneProduct.ProductCategory);
        }

        [TestMethod]
        public void CreateProductWithBrandOKTest()
        {
            Product oneProduct = new Product();
            Brand oneBrand = InitOneBrandComplete();
            oneProduct.Brand = oneBrand;

            Assert.AreEqual(oneProduct.Brand, oneBrand);
        }
        [TestMethod]
        public void CreateProductWithColorsOKTest()
        {
            Product oneProduct = new Product();
            Color oneColor = InitOneColorComplete();
            List<Color> colorList = new List<Color>();
            colorList.Add(oneColor);
            oneProduct.Colors = colorList;


            Assert.AreEqual(1,oneProduct.Colors.Count);
        }
        [TestMethod]
        public void EqualsOKTest()
        {
            Product oneProduct = InitOneProductComplete();
            Product anotherProduct = InitOneProductComplete();
            

            Assert.AreEqual(oneProduct, anotherProduct);
        }
        [TestMethod]
        public void GetHashCodeOkTest()
        {
            Product oneProduct = InitOneProductComplete();
            var hash = oneProduct.Name.GetHashCode();

            Assert.AreEqual(hash, oneProduct.GetHashCode());
        }
        [TestMethod]
        public void SearchOKTest()
        {
            // Arrange
            var product1 = InitOneProductComplete();

            var product2 = InitSecondProductComplete();
            

            var searchResult1 = new SearchResult(product1);
            var searchResult2 = new SearchResult(product1);
            var searchResult3 = new SearchResult(product2);

            // Act
            var result1 = searchResult1.Equals(searchResult2);
            var result2 = searchResult1.Equals(searchResult3);

            // Assert
            Assert.IsTrue(result1);
            Assert.IsFalse(result2);
        }
    }
}
