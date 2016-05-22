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
    public class TractorBeamUnit
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

        #region TractorBeam Class
        [Test]
        [Category("Gamedata")]
        [Category("Tractor Beam")]
        public void testTractorBeamBasic()
        {
            TractorBeam tb = new TractorBeam("2799531210", di);
            Assert.AreEqual("Tractor Beam", tb.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Tractor Beam")]
        public void testTractorBeamTypeCheck()
        {
            // This is a Power Generator, Should error!
            Assert.Throws<GamedataMismatchException>(() => new TractorBeam("2182579915", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Tractor Beam")]
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
