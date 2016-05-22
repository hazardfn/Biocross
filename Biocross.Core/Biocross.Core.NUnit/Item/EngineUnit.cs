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
    public class EngineUnit
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

        #region Engine Class
        [Test]
        [Category("Gamedata")]
        [Category("Engine")]
        public void testEngineBasic()
        {
            Engine engine = new Engine("3147096135", di);
            Assert.AreEqual("Titan Engine", engine.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Engine")]
        public void testEngineTypeCheck()
        {
            // This is a projectile, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Engine("3024613711", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Engine")]
        public void testEngineSetIDS()
        {
            Engine engine = new Engine("3147096135", di);
            Assert.AreEqual("Titan Engine", engine.Name);

            engine = engine.setIDS("2893613511");
            Assert.AreEqual("Hammerhead Engine", engine.Name);
        }
        #endregion
    }
}
