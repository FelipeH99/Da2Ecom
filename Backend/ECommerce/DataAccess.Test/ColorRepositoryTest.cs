using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using TestSetUp;
using Entities;

namespace DataAccess.Test
{
    [TestClass]
    public class ColorRepositoryTest : TestSetUps
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
        public void GetColorsOKTest() 
        {
            Color oneColor = InitOneColorComplete();
            context.Set<Color>().Add(oneColor);
            context.SaveChanges();

            ColorRepository colorRepository = new ColorRepository(context);

            int actualResult = colorRepository.Get().Count();
            Assert.AreEqual(1, actualResult);
        }
        [TestMethod]
        public void GetColorByIdOKTest() 
        {
            Color oneColor = InitOneColorComplete();
            context.Set<Color>().Add(oneColor);
            context.SaveChanges();

            ColorRepository colorRepository = new ColorRepository(context);

            Color colorById = colorRepository.Get(oneColor.Id);
            Assert.AreEqual(oneColor, colorById);
        }
        [TestMethod]
        public void GetColorNameOKTest()
        {
            Color oneColor = InitOneColorComplete();
            context.Set<Color>().Add(oneColor);
            context.SaveChanges();

            ColorRepository colorRepository = new ColorRepository(context);

            Color colorByName = colorRepository.GetByName(oneColor.Name);
            Assert.AreEqual(oneColor, colorByName);
        }
        [TestMethod]
        public void GetColorByIdNotOKTest() 
        {
            Color oneColor = InitOneColorComplete();


            ColorRepository colorRepository = new ColorRepository(context);

            Color colorById = colorRepository.Get(oneColor.Id);
            Assert.IsNull(colorById);
        }
        [TestMethod]
        public void AddColorOKTest() 
        {
            Color oneColor = InitOneColorComplete();

            ColorRepository colorRepository = new ColorRepository(context);
            colorRepository.Add(oneColor);
            context.SaveChanges();

            Assert.AreEqual(1, colorRepository.Get().ToList().Count);
         }
        [TestMethod]
        public void ColorRepositoryExistsOKTest() 
        {
            Color oneColor = InitOneColorComplete();

            ColorRepository colorRepository = new ColorRepository(context);
            context.Set<Color>().Add(oneColor);
            context.SaveChanges();

            Assert.IsTrue(colorRepository.Exists(oneColor));
        }
    }
}
