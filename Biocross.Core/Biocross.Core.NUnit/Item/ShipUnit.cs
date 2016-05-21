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
    public class ShipUnit
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

        #region Ship Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Ship")]
        public void testShipBasic()
        {
            Ship ship = new Ship("2701984835", di);
            Assert.AreEqual("Hawk (Civilian Light Fighter)", ship.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Ship")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testShipTypeCheck()
        {
            // This is a Power Generator, oops! Should error!
            new Ship("2182579915", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Ship")]
        public void testShipSetIDS()
        {
            Ship ship = new Ship("2701984835", di);
            Assert.AreEqual("Hawk (Civilian Light Fighter)", ship.Name);

            ship = ship.setIDS("2165116995");
            Assert.AreEqual("Falcon (Civilian Heavy Fighter)", ship.Name);
        }
        #endregion
    }
}
