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
    public class ScannerUnit
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

        #region Scanner Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Scanner")]
        public void testScannerBasic()
        {
            Scanner scanner = new Scanner("2186984325", di);
            Assert.AreEqual("Deep Scanner", scanner.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Scanner")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testScannerTypeCheck()
        {
            // This is a Power Generator, oops! Should error!
            new Scanner("2182579915", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Scanner")]
        public void testScannerSetIDS()
        {
            Scanner scanner = new Scanner("2186984325", di);
            Assert.AreEqual("Deep Scanner", scanner.Name);

            scanner = scanner.setIDS("2723852165");
            Assert.AreEqual("ge_s_scanner_03", scanner.Name);
        }
        #endregion
    }
}
