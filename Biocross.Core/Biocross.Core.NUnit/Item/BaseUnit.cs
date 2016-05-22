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
    public class BaseUnit
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

        #region Base Class
        [Test]
        [Category("Gamedata")]
        [Category("Base")]
        public void testBaseBasic()
        {
            Base station = new Base("li01_03_base", di);
            Assert.AreEqual("Battleship Missouri", station.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Base")]
        public void testBaseCaseInsensitive()
        {
            Base station = new Base("Li01_03_BaSe", di);
            Assert.AreEqual("Battleship Missouri", station.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Base")]
        
        public void testBaseTypeCheck()
        {
            // This is a projectile, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Base("3024613711", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Base")]
        public void testBaseSetIDS()
        {
            Base station = new Base("Li01_03_Base", di);
            Assert.AreEqual("Battleship Missouri", station.Name);

            station = station.setIDS("Li01_04_Base");
            Assert.AreEqual("Benford Station", station.Name);
        }
        #endregion
    }
}
