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
    public class AmmoUnit
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

        #region Ammo Class
        [Test]
        [Category("Gamedata")]
        [Category("Ammo")]
        public void testAmmoBasic()
        {
            Ammo ammo = new Ammo("3149084366", 1, di);
            Assert.AreEqual("Eraser Missile Ammo (Class 1)", ammo.Name);
            Assert.AreEqual(1, ammo.Quantity);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Ammo")]
        public void testAmmoTypeCheck()
        {
            // This is a projectile, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Ammo("3024613711", 0, di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Ammo")]
        public void testAmmoSetQuantity()
        {
            Ammo ammo = new Ammo("3149084366", 1, di);
            Assert.AreEqual("Eraser Missile Ammo (Class 1)", ammo.Name);
            Assert.AreEqual(1, ammo.Quantity);

            ammo = ammo.setQuantity(2);
            Assert.AreEqual("Eraser Missile Ammo (Class 1)", ammo.Name);
            Assert.AreEqual(2, ammo.Quantity);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Ammo")]
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
