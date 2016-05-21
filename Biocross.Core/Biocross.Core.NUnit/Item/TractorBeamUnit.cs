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
    public class TractorBeamUnit
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

        #region TractorBeam Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Tractor Beam")]
        public void testTractorBeamBasic()
        {
            TractorBeam tb = new TractorBeam("2799531210", di);
            Assert.AreEqual("Tractor Beam", tb.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Tractor Beam")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testTractorBeamTypeCheck()
        {
            // This is a Power Generator, oops! Should error!
            new TractorBeam("2182579915", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Tractor Beam")]
        public void testTractorBeamSetIDS()
        {
            TractorBeam tb = new TractorBeam("2799531210", di);
            Assert.AreEqual("Tractor Beam", tb.Name);

            tb = tb.setIDS("2799531210");
            Assert.AreEqual("Tractor Beam", tb.Name);
        }
        #endregion
    }
}
