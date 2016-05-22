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
    public class ScannerUnit
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

        #region Scanner Class
        [Test]
        [Category("Gamedata")]
        [Category("Scanner")]
        public void testScannerBasic()
        {
            Scanner scanner = new Scanner("2186984325", di);
            Assert.AreEqual("Deep Scanner", scanner.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Scanner")]
        public void testScannerTypeCheck()
        {
            // This is a Power Generator, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Scanner("2182579915", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Scanner")]
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
