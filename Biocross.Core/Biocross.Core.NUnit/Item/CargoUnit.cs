using System;
using System.IO;
using System.Reflection;
using Biocross.Data;
using Biocross.Core.Item;
using NUnit.Framework;
using Biocross.Core.Exceptions;

namespace Biocross.Core.NUnit.Item
{
    [TestFixture]
    public class CargoUnit
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

        #region Cargo Class
        [Test]
        [Category("Gamedata")]
        [Category("Cargo")]
        public void testCargoBasic()
        {
            Cargo cargo = new Cargo("2878617798", 1, di);
            Assert.AreEqual("Artifacts", cargo.Name);
            Assert.AreEqual(1, cargo.Quantity);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Cargo")]
        public void testCargoTypeCheck()
        {
            // This is a projectile, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Cargo("3024613711", 0, di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Cargo")]
        public void testCargoSetQuantity()
        {
            Cargo cargo = new Cargo("2878617798", 1, di);
            Assert.AreEqual("Artifacts", cargo.Name);
            Assert.AreEqual(1, cargo.Quantity);

            cargo = cargo.setQuantity(2);
            Assert.AreEqual("Artifacts", cargo.Name);
            Assert.AreEqual(2, cargo.Quantity);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Cargo")]
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
