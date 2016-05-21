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
    public class PowerUnit
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

        #region Power Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Power")]
        public void testPowerBasic()
        {
            Power power = new Power("2182579915", di);
            Assert.AreEqual("Startracker Power Generator", power.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Power")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testPowerTypeCheck()
        {
            // This is a projectile, oops! Should error!
            new Power("3024613711", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Power")]
        public void testPowerSetIDS()
        {
            Power power = new Power("2182579915", di);
            Assert.AreEqual("Startracker Power Generator", power.Name);

            power = power.setIDS("2194637003");
            Assert.AreEqual("Hawk Power Generator", power.Name);
        }
        #endregion
    }
}
