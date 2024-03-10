using Entities.Enums;
using TestSetUp;

namespace Entities.Test
{
    [TestClass]
    public class QuantityDiscountTest : TestSetUps
    {
        [TestMethod]
        public void CreateQuantityDiscountOKTest() 
        {
            QuantityDiscount quantityDiscount = new QuantityDiscount();

            Assert.IsNotNull(quantityDiscount);
        }
        [TestMethod]
        public void CreateQuantityDiscountWithNameOKTest()
        {
            QuantityDiscount quantityDiscount = new QuantityDiscount();
            string name = "Total Look";
            quantityDiscount.Name = name;

            Assert.AreEqual(name, quantityDiscount.Name);
        }
        [TestMethod]
        public void CreateQuantityDiscountWithProductCategoryOKTest() 
        {
            QuantityDiscount quantityDiscount = new QuantityDiscount();
            ProductCategory productCategory = ProductCategory.Chandal;
            quantityDiscount.ProductCategory  = productCategory;

            Assert.AreEqual(productCategory,quantityDiscount.ProductCategory);
        }
        [TestMethod]
        public void CreateQuantityDiscountWithMinItemsForPromotionOKTest() 
        {
            QuantityDiscount quantityDiscount = new QuantityDiscount();
            int minProductsNeeded = 3;
            quantityDiscount.MinProductsNeededForDiscount = minProductsNeeded;

            Assert.AreEqual(minProductsNeeded,quantityDiscount.MinProductsNeededForDiscount);
        }
        [TestMethod]
        public void CreateQuantityDiscountWithProductToBeFreeOKTest() 
        {
            QuantityDiscount quantityDiscount = new QuantityDiscount();
            int productToBeFree = 1;
            quantityDiscount.NumberOfProductsToBeFree = productToBeFree;

            Assert.AreEqual(productToBeFree, quantityDiscount.NumberOfProductsToBeFree);
        }
        [TestMethod]
        public void EqualsOKTest() 
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();
            QuantityDiscount anotherQuantityDiscount = InitOneQuantityDiscountComplete();

            Assert.AreEqual(quantityDiscount, anotherQuantityDiscount);
        }
        [TestMethod]
        public void GetHashCodeOkTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();
            var hash = quantityDiscount.Name.GetHashCode();

            Assert.AreEqual(hash, quantityDiscount.GetHashCode());
        }
        [TestMethod]
        public void CreateQuantityDiscountWithProductToBeDiscountedOKTest() 
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();
            string productToBeDiscounted = "MaxValue";
            quantityDiscount.ProductToBeDiscounted = productToBeDiscounted;

            Assert.AreEqual(productToBeDiscounted, quantityDiscount.ProductToBeDiscounted);
        }
        [TestMethod]
        public void CalculateDiscountOKTest()
        {
            QuantityDiscount quantityDiscount = InitOneQuantityDiscountComplete();
            Purchase quantityDiscountPurchase = InitPurchaseForQuantityDiscountComplete();

            double discountExpected = 19.99;

            Assert.AreEqual(discountExpected, quantityDiscount.CalculateDiscount(quantityDiscountPurchase.Products));

        }
        [TestMethod]
        public void CalculateDiscountMaxValueTest()
        {
            QuantityDiscount quantityDiscount = InitThirdQuantityDiscountComplete();
            Purchase quantityDiscountPurchase = InitPurchaseForQuantitySameCategoryDiscountComplete();

            double discountExpected = 120.99;

            Assert.AreEqual(discountExpected, quantityDiscount.CalculateDiscount(quantityDiscountPurchase.Products));

        }
    }
}
