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
    public class LightUnit
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

        #region Light Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Light")]
        public void testLightBasic()
        {
            Light light = new Light("2636123526", di);
            Assert.AreEqual("SmallOrange", light.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Light")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testLightTypeCheck()
        {
            // This is a projectile, oops! Should error!
            new Light("3024613711", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Light")]
        public void testLightSetIDS()
        {
            Light light = new Light("2636123526", di);
            Assert.AreEqual("SmallOrange", light.Name);

            light = light.setIDS("2896257926");
            Assert.AreEqual("SmallYellow", light.Name);
        }
        #endregion
    }
}
