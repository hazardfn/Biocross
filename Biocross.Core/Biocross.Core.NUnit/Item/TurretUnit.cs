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
    public class TurretUnit
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

        #region Turret Class
        [Test]
        [Category("Gamedata")]
        [Category("Turret")]
        public void testTurretBasic()
        {
            Turret turret = new Turret("3161989646", di);
            Assert.AreEqual("Justice Turret Mk I (Class 1)", turret.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Turret")]
        
        public void testTurretTypeCheck()
        {
            // This is a projectile, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Turret("3024613711", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Turret")]
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
