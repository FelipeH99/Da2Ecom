using DataAccess.Contexts;
using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using TestSetUp;

namespace DataAccess.Test
{
    [TestClass]
    public class ColorDiscountRepositoryTest : TestSetUps
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
        public void GetColorDiscountsOKTest()
        {
            ColorDiscount colorDiscount = InitOneColorDiscountComplete();
            context.Set<ColorDiscount>().Add(colorDiscount);
            context.SaveChanges();

            ColorDiscountRepository colorDiscountRepository = new ColorDiscountRepository(context);

            int actualResult = colorDiscountRepository.Get().Count();
            Assert.AreEqual(1, actualResult);
        }
        [TestMethod]
        public void GetColorDiscountByIdOKTest()
        {
            ColorDiscount colorDiscount = InitOneColorDiscountComplete();
            context.Set<ColorDiscount>().Add(colorDiscount);
            context.SaveChanges();

            ColorDiscountRepository colorDiscountRepository = new ColorDiscountRepository(context);

            ColorDiscount colorDiscountById = colorDiscountRepository.Get(colorDiscount.Id);
            Assert.AreEqual(colorDiscount, colorDiscountById);
        }
        [TestMethod]
        public void GetColorDiscountByIdNotOKTest()
        {
            ColorDiscount colorDiscount = InitOneColorDiscountComplete();

            ColorDiscountRepository colorDiscountRepository = new ColorDiscountRepository(context);

            ColorDiscount colorDiscountById = colorDiscountRepository.Get(colorDiscount.Id);
            Assert.IsNull(colorDiscountById);
        }
        [TestMethod]
        public void AddColorDiscountOKTest()
        {
            ColorDiscount colorDiscount = InitOneColorDiscountComplete();

            ColorDiscountRepository colorDiscountRepository = new ColorDiscountRepository(context);

            colorDiscountRepository.Add(colorDiscount);
            context.SaveChanges();

            Assert.AreEqual(1, colorDiscountRepository.Get().ToList().Count);
        }
        [TestMethod]
        public void ColorDiscountRepositoryExistsOKTest()
        {
            ColorDiscount colorDiscount = InitOneColorDiscountComplete();

            ColorDiscountRepository colorDiscountRepository = new ColorDiscountRepository(context);

            colorDiscountRepository.Add(colorDiscount);
            context.SaveChanges();

            Assert.IsTrue(colorDiscountRepository.Exists(colorDiscount));
        }
        [TestMethod]
        public void UpdateColorDiscountRepositoryOKTest()
        {
            ColorDiscountRepository colorDiscountRepository = new ColorDiscountRepository(context);

            ColorDiscount colorDiscount = InitOneColorDiscountComplete();
            ColorDiscount anotherColorDiscount = InitAnotherColorDiscountComplete();

            colorDiscountRepository.Add(colorDiscount);
            colorDiscountRepository.Save();

            colorDiscountRepository.Update(colorDiscount, anotherColorDiscount);

            Assert.AreEqual(colorDiscountRepository.Get(colorDiscount.Id),
               anotherColorDiscount);
        }
        [TestMethod]
        public void DeleteColorDiscountTest()
        {

            ColorDiscount colorDiscount = InitOneColorDiscountComplete();

            context.Set<ColorDiscount>().Add(colorDiscount);

            ColorDiscountRepository colorDiscountRepository = new ColorDiscountRepository(context);
            colorDiscountRepository.Save();
            colorDiscountRepository.Remove(colorDiscount);

            Assert.AreEqual(colorDiscountRepository.Get().Count, 0);
        }

        [ExpectedException(typeof(IncorrectRequestException))]
        [TestMethod]
        public void DeleteInvalidOkTest()
        {
            ColorDiscount colorDiscount = InitOneColorDiscountComplete();
            ColorDiscount anotherColorDiscount = InitAnotherColorDiscountComplete();

            ColorDiscountRepository colorDiscountRepository = new ColorDiscountRepository(context);
            colorDiscountRepository.Add(colorDiscount);
            colorDiscountRepository.Remove(anotherColorDiscount);
        }
    }
}
