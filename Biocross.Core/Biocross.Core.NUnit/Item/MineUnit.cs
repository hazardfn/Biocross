using System;
using System.IO;
using System.Reflection;
using Biocross.Data;
using Biocross.Core.Item;
using Biocross.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biocross.Core.NUnit.Item
{
    [TestClass]
    public class MineUnit
    {

        private static DataInterface di = new DataInterface(@Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\biodata.mdf", "TESTDATA/");

        #region Test Setup
        [ClassInitialize]
        public static void setup(TestContext c)
        {
            di.init();
        }
        [ClassCleanup]
        public static void cleanup()
        {
            di.Dispose();
        }
        #endregion

        #region Mine Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Mine")]
        public void testMineBasic()
        {
            Mine mine = new Mine("2836977929", di);
            Assert.AreEqual("Seeker Mine Dropper", mine.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Mine")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testMineTypeCheck()
        {
            // This is a projectile, oops! Should error!
            new Mine("3024613711", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Mine")]
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
