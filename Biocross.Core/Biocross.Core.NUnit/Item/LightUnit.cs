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
    public class LightUnit
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

        #region Light Class
        [Test]
        [Category("Gamedata")]
        [Category("Light")]
        public void testLightBasic()
        {
            Light light = new Light("2636123526", di);
            Assert.AreEqual("SmallOrange", light.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Light")]
        
        public void testLightTypeCheck()
        {
            // This is a projectile, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Light("3024613711", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Light")]
        public void testLightSetIDS()
        {
            Light light = new Light("2636123526", di);
            Assert.AreEqual("SmallOrange", light.Name);

            light = light.setIDS("2896257926");
            Assert.AreEqual("SmallYellow", light.Name);
        }
        #endregion
    }
}
