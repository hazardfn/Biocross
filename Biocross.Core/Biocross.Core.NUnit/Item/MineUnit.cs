using System;
using System.IO;
using System.Reflection;
using Biocross.Data;
using Biocross.Core.Item;
using Biocross.Core.Exceptions;
using NUnit.Framework;


namespace Biocross.Core.NUnit.Item
{
    [TestFixture]
    public class MineUnit
    {

        private static DataInterface di = new DataInterface(@Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\biodata.mdf", "TESTDATA/");

        #region Test Setup
        [OneTimeSetUp]
        public static void setup()
        {
            di.init();
        }
        [OneTimeTearDown]
        public static void cleanup()
        {
            di.Dispose();
        }
        #endregion

        #region Mine Class
        [Test]
        [Category("Gamedata")]
        [Category("Mine")]
        public void testMineBasic()
        {
            Mine mine = new Mine("2836977929", di);
            Assert.AreEqual("Seeker Mine Dropper", mine.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Mine")]
        
        public void testMineTypeCheck()
        {
            // This is a projectile, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Mine("3024613711", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Mine")]
        public void testMineSetIDS()
        {
            Mine mine = new Mine("2836977929", di);
            Assert.AreEqual("Seeker Mine Dropper", mine.Name);

            mine = mine.setIDS("2836982026");
            Assert.AreEqual("Wardog Mine Dropper", mine.Name);
        }
        #endregion
    }
}
