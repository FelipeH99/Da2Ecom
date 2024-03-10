using TestSetUp;

namespace Entities.Test
{
    [TestClass]
    public class PurchaseTest : TestSetUps
    {
        [TestMethod]
        public void CreatePurchaseOKTest() 
        {
            Purchase onePurchase = new Purchase();

            Assert.IsNotNull(onePurchase);
        }
        [TestMethod]
        public void CreatePurchaseWithUserOKTest() 
        {
            Purchase onePurchase = new Purchase();
            User oneUser = InitOneUserComplete();
            onePurchase.User = oneUser;

            Assert.AreEqual(onePurchase.User,oneUser);
        }
        [TestMethod]
        public void CreatePurchaseWithDateOKTest() 
        {
            Purchase onePurchase = new Purchase();
            DateTime purchaseDate = new DateTime(2020, 11, 11);
            onePurchase.PurchaseDate = purchaseDate;

            Assert.AreEqual(purchaseDate, onePurchase.PurchaseDate);
        }
        [TestMethod]
        public void CretePurchaseWithProductListOKTest() 
        {
            Purchase onePurchase = new Purchase();
            List<Product> products = new List<Product>();
            Product product = InitOneProductComplete();
            products.Add(product);
            onePurchase.Products = products;

            Assert.AreEqual(1,onePurchase.Products.Count);

        }
        [TestMethod]
        public void CretePurchaseWithFinalPriceOKTest()
        {
            Purchase onePurchase = new Purchase();
            double price = 198.28;
            onePurchase.FinalPrice = price;

            Assert.AreEqual(price, onePurchase.FinalPrice);

        }
        [TestMethod]
        public void CretePurchaseWithProductsPriceOKTest()
        {
            Purchase onePurchase = InitOnePurchaseComplete();
            var products = onePurchase.Products;
            double price = products.AsQueryable().Sum(p => p.Price);
            onePurchase.ProductsPrice = price;

            Assert.AreEqual(price, onePurchase.ProductsPrice);

        }
        [TestMethod]
        public void EqualsOKTest()
        {
            Purchase onePurchase = InitOnePurchaseComplete();
            Purchase anotherPurchase = InitOnePurchaseComplete();

            Assert.AreEqual(onePurchase, anotherPurchase);
        }
        [TestMethod]
        public void GetHashCodeOkTest()
        {
            Purchase onePurchase = InitOnePurchaseComplete();
            var hash = onePurchase.User.GetHashCode() + onePurchase.PurchaseDate.GetHashCode();

            Assert.AreEqual(hash, onePurchase.GetHashCode());
        }
        [TestMethod]
        public void CretePurchaseWithDiscountAppliedOKTest()
        {
            Purchase onePurchase = new Purchase();
            string discountApplied = "3x2";
            onePurchase.DiscountApplied = discountApplied;
            Assert.AreEqual(discountApplied, onePurchase.DiscountApplied);
        }
    }
}
