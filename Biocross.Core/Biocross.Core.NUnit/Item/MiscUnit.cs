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
    public class MiscUnit
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

        #region Misc Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Misc")]
        public void testMiscBasic()
        {
            Misc misc = new Misc("2356232010", di);
            Assert.AreEqual("sfx_rumble_battleship Sound", misc.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Misc")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testMiscTypeCheck()
        {
            // This is a projectile, oops! Should error!
            new Misc("3024613711", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Misc")]
        public void testMiscSetIDS()
        {
            Misc misc = new Misc("2356232010", di);
            Assert.AreEqual("sfx_rumble_battleship Sound", misc.Name);

            misc = misc.setIDS("3065516173");
            Assert.AreEqual("sfx_rumble_cruiser Sound", misc.Name);
        }
        #endregion
    }
}
