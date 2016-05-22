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
    public class ShieldUnit
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

        #region Shield Class
        [Test]
        [Category("Gamedata")]
        [Category("Shield")]
        public void testShieldBasic()
        {
            Shield shield = new Shield("3090148620", di);
            Assert.AreEqual("Sentinel L. F. Shield (Class 5 Graviton)", shield.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Shield")]
        
        public void testShieldTypeCheck()
        {
            // This is a Power Generator, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Shield("2182579915", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Shield")]
        public void testShieldSetIDS()
        {
            Shield shield = new Shield("3090148620", di);
            Assert.AreEqual("Sentinel L. F. Shield (Class 5 Graviton)", shield.Name);

            shield = shield.setIDS("3090672911");
            Assert.AreEqual("Adv. Sentinel L. F. Shield (Class 6 Graviton)", shield.Name);
        }
        #endregion
    }
}
