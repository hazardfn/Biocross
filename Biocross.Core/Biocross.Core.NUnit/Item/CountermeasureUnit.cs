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
    public class CountermeasureUnit
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

        #region Countermeasure Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("CM")]
        public void testCountermeasureBasic()
        {
            Countermeasure cm = new Countermeasure("2736156677", di);
            Assert.AreEqual("Adv. Countermeasure Dropper", cm.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("CM")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testCountermeasureTypeCheck()
        {
            // This is a projectile, oops! Should error!
            new Countermeasure("3024613711", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("CM")]
        public void testCountermeasureSetIDS()
        {
            Countermeasure cm = new Countermeasure("2736156677", di);
            Assert.AreEqual("Adv. Countermeasure Dropper", cm.Name);

            cm = cm.setIDS("2199282693");
            Assert.AreEqual("Imp. Countermeasure Dropper", cm.Name);
        }
        #endregion
    }
}
