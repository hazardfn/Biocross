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
    public class ShipUnit
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

        #region Ship Class
        [Test]
        [Category("Gamedata")]
        [Category("Ship")]
        public void testShipBasic()
        {
            Ship ship = new Ship("2701984835", di);
            Assert.AreEqual("Hawk (Civilian Light Fighter)", ship.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Ship")]
        
        public void testShipTypeCheck()
        {
            // This is a Power Generator, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Ship("2182579915", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Ship")]
        public void testShipSetIDS()
        {
            Ship ship = new Ship("2701984835", di);
            Assert.AreEqual("Hawk (Civilian Light Fighter)", ship.Name);

            ship = ship.setIDS("2165116995");
            Assert.AreEqual("Falcon (Civilian Heavy Fighter)", ship.Name);
        }
        #endregion
    }
}
