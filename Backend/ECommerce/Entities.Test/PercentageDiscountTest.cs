using TestSetUp;

namespace Entities.Test
{
    [TestClass]
    public class PercentageDiscountTest : TestSetUps
    {
        [TestMethod]
        public void CreatePorcentageDiscountOKTest()
        {
            PercentageDiscount porcentageDiscount = new PercentageDiscount();
            Assert.IsNotNull(porcentageDiscount);
        }
        [TestMethod]
        public void CreatePercentageDiscountWithNameOKTest()
        {
            PercentageDiscount percentageDiscount = new PercentageDiscount();
            string name = "20%";
            percentageDiscount.Name = name;

            Assert.AreEqual(name,percentageDiscount.Name);
        }
        [TestMethod]
        public void CreatePercentageWithPercentageOKTest() 
        {
            PercentageDiscount percentageDiscount = new PercentageDiscount();
            double percentage = 0.20;
            percentageDiscount.PercentageDiscounted = percentage;

            Assert.AreEqual(percentage,percentageDiscount.PercentageDiscounted);
        }
        [TestMethod]
        public void CreatePercentageDiscountWithProductToBeDiscountedOKT() 
        {
            PercentageDiscount percentageDiscount = new PercentageDiscount();
            string productToBeDiscounted = "MaxValue";
            percentageDiscount.ProductToBeDiscounted = productToBeDiscounted;

            Assert.AreEqual(productToBeDiscounted, percentageDiscount.ProductToBeDiscounted);
        }
        [TestMethod]
        public void EqualsOKTest() 
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();
            PercentageDiscount anotherPercentageDiscount = InitOnePercentageDiscountComplete();

            Assert.AreEqual(percentageDiscount, anotherPercentageDiscount);
        }
        [TestMethod]
        public void GetHashCodeOKTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();
            var hash = percentageDiscount.Name.GetHashCode();

            Assert.AreEqual(hash, percentageDiscount.GetHashCode());
        }
        [TestMethod]
        public void CreatePercentageDiscountWithMinProductsNeededForPromotionOKTest() 
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();
            int minProductsNeeded = 2;
            percentageDiscount.MinProductsNeededForDiscount = minProductsNeeded;

            Assert.AreEqual(minProductsNeeded, percentageDiscount.MinProductsNeededForDiscount);

        }
        [TestMethod]
        public void CalculateDiscountOKTest()
        {
            PercentageDiscount percentageDiscount = InitOnePercentageDiscountComplete();
            Purchase percentageDiscountPurchase = InitPurchaseForPercentageDiscountComplete();

            double discountExpected = 24.198;

            Assert.AreEqual(discountExpected, percentageDiscount.CalculateDiscount(percentageDiscountPurchase.Products));

        }
        [TestMethod]
        public void CalculateDiscountNotMaxOKTest()
        {
            PercentageDiscount percentageDiscount = InitAnotherPercentageNotMaxValueDiscountComplete();
            Purchase percentageDiscountPurchase = InitPurchaseForPercentageDiscountComplete();

            double discountExpected = 7.9959999999999996;

            Assert.AreEqual(discountExpected, percentageDiscount.CalculateDiscount(percentageDiscountPurchase.Products));

        }
    }
}
