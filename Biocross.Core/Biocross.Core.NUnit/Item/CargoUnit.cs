using System;
using System.IO;
using System.Reflection;
using Biocross.Data;
using Biocross.Core.Item;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biocross.Core.NUnit.Item
{
    [TestClass]
    public class CargoUnit
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

        #region Cargo Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Cargo")]
        public void testCargoBasic()
        {
            Cargo cargo = new Cargo("2878617798", 1, di);
            Assert.AreEqual("Artifacts", cargo.Name);
            Assert.AreEqual(1, cargo.Quantity);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Cargo")]
        [ExpectedException(typeof(Biocross.Core.Exceptions.GamedataMismatchException))]
        public void testCargoTypeCheck()
        {
            // This is a projectile, oops! Should error!
            new Cargo("3024613711", 0, di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Cargo")]
        public void testCargoSetQuantity()
        {
            Cargo cargo = new Cargo("2878617798", 1, di);
            Assert.AreEqual("Artifacts", cargo.Name);
            Assert.AreEqual(1, cargo.Quantity);

            cargo = cargo.setQuantity(2);
            Assert.AreEqual("Artifacts", cargo.Name);
            Assert.AreEqual(2, cargo.Quantity);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Cargo")]
        public void testCargoSetIDS()
        {
            Cargo cargo = new Cargo("2878617798", 1, di);
            Assert.AreEqual("Artifacts", cargo.Name);
            Assert.AreEqual(1, cargo.Quantity);

            cargo = cargo.setIDS("2723152328");
            Assert.AreEqual("Beryllium", cargo.Name);
            Assert.AreEqual(1, cargo.Quantity);
        }
        #endregion
    }
}
