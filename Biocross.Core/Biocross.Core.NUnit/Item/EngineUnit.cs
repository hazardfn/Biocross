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
    public class EngineUnit
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

        #region Engine Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Engine")]
        public void testEngineBasic()
        {
            Engine engine = new Engine("3147096135", di);
            Assert.AreEqual("Titan Engine", engine.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Engine")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testEngineTypeCheck()
        {
            // This is a projectile, oops! Should error!
            new Engine("3024613711", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Engine")]
        public void testEngineSetIDS()
        {
            Engine engine = new Engine("3147096135", di);
            Assert.AreEqual("Titan Engine", engine.Name);

            engine = engine.setIDS("2893613511");
            Assert.AreEqual("Hammerhead Engine", engine.Name);
        }
        #endregion
    }
}
