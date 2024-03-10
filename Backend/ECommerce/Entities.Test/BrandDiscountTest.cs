using TestSetUp;

namespace Entities.Test 
{
    [TestClass]
    public class BrandDiscountTest : TestSetUps
    {
        [TestMethod]
        public void createBrandDiscountOKTest() 
        {
            BrandDiscount brandDiscount = new BrandDiscount();

            Assert.IsNotNull(brandDiscount);
        }
        [TestMethod]
        public void CreateBrandDiscountWithNameOKTest() 
        {
            BrandDiscount brandDiscount = new BrandDiscount();
            string name = "3x1 Fidelidad";
            brandDiscount.Name = name;

            Assert.AreEqual(name, brandDiscount.Name);
        }
        [TestMethod]
        public void CreateBrandDiscountWithBrandOKTest() 
        {
            BrandDiscount brandDiscount = new BrandDiscount();
            Brand oneBrand = InitOneBrandComplete();
            brandDiscount.Brand = oneBrand;

            Assert.AreEqual(oneBrand,brandDiscount.Brand);
        }
        [TestMethod]
        public void CreateBrandDiscountWithMinNumberOfProductsForPromotionOKTest() 
        {
            BrandDiscount brandDiscount = new BrandDiscount();
            int minProductsForPromotion = 3;
            brandDiscount.MinProductsForPromotion = minProductsForPromotion;

            Assert.AreEqual(minProductsForPromotion, brandDiscount.MinProductsForPromotion);
        }
        [TestMethod]
        public void CreateBrandDiscountWithNumberOfProductsFreeOKTest() 
        {
            BrandDiscount brandDiscount = new BrandDiscount();
            int numberOfProductsForFree = 2;
            brandDiscount.NumberOfProductsForFree = numberOfProductsForFree;

            Assert.AreEqual(numberOfProductsForFree, brandDiscount.NumberOfProductsForFree);
        }
        [TestMethod]
        public void EqualsOKTest() 
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            BrandDiscount anotherBrandDiscount = InitOneBrandDiscountComplete();

            Assert.AreEqual(brandDiscount, anotherBrandDiscount);
        }
        [TestMethod]
        public void GetHashCodeOKTest() 
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            var hash = brandDiscount.Name.GetHashCode();

            Assert.AreEqual(hash, brandDiscount.GetHashCode());
        }
        [TestMethod]
        public void CreateBrandDiscountWithProductToBeDiscountedOKTest() 
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            string productToBeDiscounted = "MaxValue";
            brandDiscount.ProductToBeDiscounted = productToBeDiscounted;

            Assert.AreEqual(productToBeDiscounted, brandDiscount.ProductToBeDiscounted);
        }
        [TestMethod]
        public void CalculateDiscountOKTest() 
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            Purchase brandDiscountPurchase = InitPurchaseForBrandDiscountComplete();

            double discountExpected = 39.98;
            
            Assert.AreEqual(discountExpected, brandDiscount.CalculateDiscount(brandDiscountPurchase.Products));

        }
        [TestMethod]
        public void CalculateDiscountNoMaxValueTest()
        {
            BrandDiscount brandDiscount = InitAnotherBrandDiscountComplete();
            Purchase brandDiscountPurchase = InitPurchaseForBrandDiscountComplete();

            double discountExpected = 120.99;

            Assert.AreEqual(discountExpected, brandDiscount.CalculateDiscount(brandDiscountPurchase.Products));

        }
        [TestMethod]
        public void CalculateDiscountWithNoProductsTest()
        {
            BrandDiscount brandDiscount = InitOneBrandDiscountComplete();
            Purchase brandDiscountPurchase = new Purchase();

            double discountExpected = 0;

            Assert.AreEqual(discountExpected, brandDiscount.CalculateDiscount(brandDiscountPurchase.Products));

        }
    }
}

