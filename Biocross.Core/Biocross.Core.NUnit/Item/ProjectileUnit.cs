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
    public class ProjectileUnit
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

        #region Projectile Class
        [Test]
        [Category("Gamedata")]
        [Category("Projectile")]
        public void testProjectileBasic()
        {
            Projectile projectile = new Projectile("2487743822", di);
            Assert.AreEqual("Stalker Missile Launcher (Class 1)", projectile.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Projectile")]
        
        public void testProjectileTypeCheck()
        {
            // This is a Power Generator, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Projectile("2182579915", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Projectile")]
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
