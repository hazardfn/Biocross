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
    public class TurretUnit
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

        #region Turret Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Turret")]
        public void testTurretBasic()
        {
            Turret turret = new Turret("3161989646", di);
            Assert.AreEqual("Justice Turret Mk I (Class 1)", turret.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Turret")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testTurretTypeCheck()
        {
            // This is a projectile, oops! Should error!
            new Turret("3024613711", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Turret")]
        public void testTurretSetIDS()
        {
            Turret turret = new Turret("3161989646", di);
            Assert.AreEqual("Justice Turret Mk I (Class 1)", turret.Name);

            turret = turret.setIDS("2759818753");
            Assert.AreEqual("Starbeam Turret (Class 1)", turret.Name);
        }
        #endregion
    }
}
