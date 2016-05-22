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
    public class PowerUnit
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

        #region Power Class
        [Test]
        [Category("Gamedata")]
        [Category("Power")]
        public void testPowerBasic()
        {
            Power power = new Power("2182579915", di);
            Assert.AreEqual("Startracker Power Generator", power.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Power")]
        
        public void testPowerTypeCheck()
        {
            // This is a projectile, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Power("3024613711", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Power")]
        public void testPowerSetIDS()
        {
            Power power = new Power("2182579915", di);
            Assert.AreEqual("Startracker Power Generator", power.Name);

            power = power.setIDS("2194637003");
            Assert.AreEqual("Hawk Power Generator", power.Name);
        }
        #endregion
    }
}
