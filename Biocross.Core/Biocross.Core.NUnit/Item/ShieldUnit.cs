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
    public class ShieldUnit
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

        #region Shield Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Shield")]
        public void testShieldBasic()
        {
            Shield shield = new Shield("3090148620", di);
            Assert.AreEqual("Sentinel L. F. Shield (Class 5 Graviton)", shield.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Shield")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testShieldTypeCheck()
        {
            // This is a Power Generator, oops! Should error!
            new Shield("2182579915", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Shield")]
        public void testShieldSetIDS()
        {
            Shield shield = new Shield("3090148620", di);
            Assert.AreEqual("Sentinel L. F. Shield (Class 5 Graviton)", shield.Name);

            shield = shield.setIDS("3090672911");
            Assert.AreEqual("Adv. Sentinel L. F. Shield (Class 6 Graviton)", shield.Name);
        }
        #endregion
    }
}
