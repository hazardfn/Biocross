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
    public class GunUnit
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

        #region Gun Class
        [Test]
        [Category("Gamedata")]
        [Category("Gun")]
        public void testGunBasic()
        {
            Gun gun = new Gun("2244256138", di);
            Assert.AreEqual("Death's Hand Mk I (Class 1)", gun.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Gun")]      
        public void testGunTypeCheck()
        {
            // This is a projectile, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Gun("3024613711", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Gun")]
        public void testGunSetIDS()
        {
            Gun gun = new Gun("2244256138", di);
            Assert.AreEqual("Death's Hand Mk I (Class 1)", gun.Name);

            gun = gun.setIDS("3219265993");
            Assert.AreEqual("Justice Mk I (Class 1)", gun.Name);
        }
        #endregion
    }
}
