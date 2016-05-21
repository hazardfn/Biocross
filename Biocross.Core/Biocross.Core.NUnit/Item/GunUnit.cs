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
    public class GunUnit
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

        #region Gun Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Gun")]
        public void testGunBasic()
        {
            Gun gun = new Gun("2244256138", di);
            Assert.AreEqual("Death's Hand Mk I (Class 1)", gun.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Gun")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testGunTypeCheck()
        {
            // This is a projectile, oops! Should error!
            new Gun("3024613711", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Gun")]
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
