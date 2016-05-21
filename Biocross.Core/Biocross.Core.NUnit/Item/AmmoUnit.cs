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
    public class AmmoUnit
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

        #region Ammo Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Ammo")]
        public void testAmmoBasic()
        {
            Ammo ammo = new Ammo("3149084366", 1, di);
            Assert.AreEqual("Eraser Missile Ammo (Class 1)", ammo.Name);
            Assert.AreEqual(1, ammo.Quantity);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Ammo")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testAmmoTypeCheck()
        {
            // This is a projectile, oops! Should error!
            new Ammo("3024613711", 0, di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Ammo")]
        public void testAmmoSetQuantity()
        {
            Ammo ammo = new Ammo("3149084366", 1, di);
            Assert.AreEqual("Eraser Missile Ammo (Class 1)", ammo.Name);
            Assert.AreEqual(1, ammo.Quantity);

            ammo = ammo.setQuantity(2);
            Assert.AreEqual("Eraser Missile Ammo (Class 1)", ammo.Name);
            Assert.AreEqual(2, ammo.Quantity);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Ammo")]
        public void testAmmoSetIDS()
        {
            Ammo ammo = new Ammo("3149084366", 1, di);
            Assert.AreEqual("Eraser Missile Ammo (Class 1)", ammo.Name);
            Assert.AreEqual(1, ammo.Quantity);

            ammo = ammo.setIDS("3130291785");
            Assert.AreEqual("Slingshot Missile Ammo (Class 3)", ammo.Name);
            Assert.AreEqual(1, ammo.Quantity);
        }
        #endregion
    }
}
