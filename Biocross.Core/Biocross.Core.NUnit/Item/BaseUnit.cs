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
    public class BaseUnit
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

        #region Base Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Base")]
        public void testBaseBasic()
        {
            Base station = new Base("li01_03_base", di);
            Assert.AreEqual("Battleship Missouri", station.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Base")]
        public void testBaseCaseInsensitive()
        {
            Base station = new Base("Li01_03_BaSe", di);
            Assert.AreEqual("Battleship Missouri", station.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Base")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testBaseTypeCheck()
        {
            // This is a projectile, oops! Should error!
            new Base("3024613711", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Base")]
        public void testBaseSetIDS()
        {
            Base station = new Base("Li01_03_Base", di);
            Assert.AreEqual("Battleship Missouri", station.Name);

            station = station.setIDS("Li01_04_Base");
            Assert.AreEqual("Benford Station", station.Name);
        }
        #endregion
    }
}
