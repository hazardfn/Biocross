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
    public class SystemUnit
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

        #region System Class
        [Test]
        [Category("Gamedata")]
        [Category("System")]
        public void testSystemBasic()
        {
            Core.Item.System system = new Core.Item.System("Li02", di);
            Assert.AreEqual("California", system.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("System")]
        public void testSystemCaseInsensitive()
        {
            Core.Item.System system = new Core.Item.System("lI02", di);
            Assert.AreEqual("California", system.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("System")]
        
        public void testSystemTypeCheck()
        {
            // This is a Power Generator, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Core.Item.System("2182579915", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("System")]
        public void testSystemSetIDS()
        {
            Core.Item.System system = new Core.Item.System("Li02", di);
            Assert.AreEqual("California", system.Name);

            system = system.setIDS("Li01");
            Assert.AreEqual("New York", system.Name);
        }
        #endregion
    }
}
