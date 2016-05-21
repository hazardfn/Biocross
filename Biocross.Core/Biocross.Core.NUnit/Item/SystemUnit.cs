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
    public class SystemUnit
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

        #region System Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("System")]
        public void testSystemBasic()
        {
            Core.Item.System system = new Core.Item.System("Li02", di);
            Assert.AreEqual("California", system.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("System")]
        public void testSystemCaseInsensitive()
        {
            Core.Item.System system = new Core.Item.System("lI02", di);
            Assert.AreEqual("California", system.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("System")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testSystemTypeCheck()
        {
            // This is a Power Generator, oops! Should error!
            new Core.Item.System("2182579915", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("System")]
        public void testSystemSetIDS()
        {
            Core.Item.System system = new Core.Item.System("Li02", di);
            Assert.AreEqual("California", system.Name);

            system = system.setIDS("Li01");
            Assert.AreEqual("New York", system.Name);
        }
        #endregion
    }
}
