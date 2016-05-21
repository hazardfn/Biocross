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
    public class ProjectileUnit
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

        #region Projectile Class
        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Projectile")]
        public void testProjectileBasic()
        {
            Projectile projectile = new Projectile("2487743822", di);
            Assert.AreEqual("Stalker Missile Launcher (Class 1)", projectile.Name);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Projectile")]
        [ExpectedException(typeof(GamedataMismatchException))]
        public void testProjectileTypeCheck()
        {
            // This is a Power Generator, oops! Should error!
            new Projectile("2182579915", di);
        }

        [TestMethod]
        [TestCategory("Gamedata")]
        [TestCategory("Projectile")]
        public void testProjectileSetIDS()
        {
            Projectile projectile = new Projectile("2487743822", di);
            Assert.AreEqual("Stalker Missile Launcher (Class 1)", projectile.Name);

            projectile = projectile.setIDS("3024613711");
            Assert.AreEqual("Eraser Missile Launcher (Class 1)", projectile.Name);
        }
        #endregion
    }
}
