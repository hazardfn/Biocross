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
    public class ThrusterUnit
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

        #region Thruster Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Thruster")]
        public void testThrusterBasic()
        {
            Thruster thruster = new Thruster("2851612992", di);
            Assert.AreEqual("Deluxe Thruster", thruster.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Thruster")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testThrusterTypeCheck()
        {
            // This is a Power Generator, oops! Should error!
            new Thruster("2182579915", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Thruster")]
        public void testThrusterSetIDS()
        {
            Thruster thruster = new Thruster("2851612992", di);
            Assert.AreEqual("Deluxe Thruster", thruster.Name);

            thruster = thruster.setIDS("2314747200");
            Assert.AreEqual("Heavy Thruster", thruster.Name);
        }
        #endregion
    }
}
