using TestSetUp;

namespace Entities.Test
{
    [TestClass]
    public class ColorDiscountTest : TestSetUps
    {
        [TestMethod]
        public void CreateColorDiscountOKTest()
        {
            ColorDiscount colorDiscount = new ColorDiscount();

            Assert.IsNotNull(colorDiscount);
        }
        [TestMethod]
        public void CreateColorDiscountWithNameOKTest()
        {
            ColorDiscount colorDiscount = new ColorDiscount();
            string name = "Total Look";
            colorDiscount.Name = name;

            Assert.AreEqual(name, colorDiscount.Name);
        }
        [TestMethod]
        public void CreateColorDiscountWithColorOKTest()
        {
            ColorDiscount colorDiscount = new ColorDiscount();
            Color oneColor = InitOneColorComplete();
            colorDiscount.Color = oneColor;

            Assert.AreEqual(oneColor, colorDiscount.Color);
        }
        [TestMethod]
        public void CreateColorDiscountWithPercentageOKTest()
        {
            ColorDiscount colorDiscount = new ColorDiscount();
            double percentage = 0.50;
            colorDiscount.PercentageDiscount = percentage;

            Assert.AreEqual(percentage, colorDiscount.PercentageDiscount);
        }
        [TestMethod]
        public void CreateColorDiscountWithItemToBeAfectedByDiscountOKTest()
        {
            ColorDiscount colorDiscount = new ColorDiscount();
            string itemToBeAfected = "MaxValue";
            colorDiscount.ProductToBeDiscounted = itemToBeAfected;

            Assert.AreEqual(itemToBeAfected, colorDiscount.ProductToBeDiscounted);
        }
        [TestMethod]
        public void EqualsOKTest()
        {
            ColorDiscount colorDiscount = InitOneColorDiscountComplete();
            ColorDiscount anotherColorDiscount = InitOneColorDiscountComplete();

            Assert.AreEqual(colorDiscount, anotherColorDiscount);

        }
        [TestMethod]
        public void GetHashCodeOkTest()
        {
            ColorDiscount colorDiscount = InitOneColorDiscountComplete();
            var hash = colorDiscount.Name.GetHashCode();

            Assert.AreEqual(hash, colorDiscount.GetHashCode());
        }
        [TestMethod]
        public void CreateColorDiscountWithMinNumberOfProductsNeededForPromotionOKTest() 
        {
            ColorDiscount colorDiscount = InitOneColorDiscountComplete();
            int minProductsNeeded = 3;
            colorDiscount.MinProductsNeededForDiscount = minProductsNeeded;

            Assert.AreEqual(minProductsNeeded, colorDiscount.MinProductsNeededForDiscount);
        }
        [TestMethod]
        public void CalculateDiscountOKTest()
        {
            ColorDiscount colorDiscount = InitOneColorDiscountComplete();
            Purchase colorDiscountPurchase = InitPurchaseForColorDiscountComplete();

            double discountExpected = 60.495;

            Assert.AreEqual(discountExpected, colorDiscount.CalculateDiscount(colorDiscountPurchase.Products));

        }
        [TestMethod]
        public void CalculateDiscountMinValueOKTest()
        {
            ColorDiscount colorDiscount = InitAnotherColorDiscountMinValueComplete();
            Purchase colorDiscountPurchase = InitPurchaseForColorDiscountComplete();

            double discountExpected = 3.9979999999999998;

            Assert.AreEqual(discountExpected, colorDiscount.CalculateDiscount(colorDiscountPurchase.Products));

        }
        [TestMethod]
        public void CalculateDiscount_ReturnsZero_WhenProductsListIsEmpty()
        {
            // Arrange
            var discountCalculator = new ColorDiscount();
            var products = new List<Product>();

            // Act
            double discount = discountCalculator.CalculateDiscount(products);

            // Assert
            Assert.AreEqual(0, discount);
        }
    }
}
