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
    public class MiscUnit
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

        #region Misc Class
        [Test]
        [Category("Gamedata")]
        [Category("Misc")]
        public void testMiscBasic()
        {
            Misc misc = new Misc("2356232010", di);
            Assert.AreEqual("sfx_rumble_battleship Sound", misc.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Misc")]     
        public void testMiscTypeCheck()
        {
            // This is a projectile, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Misc("3024613711", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Misc")]
        public void testMiscSetIDS()
        {
            Misc misc = new Misc("2356232010", di);
            Assert.AreEqual("sfx_rumble_battleship Sound", misc.Name);

            misc = misc.setIDS("3065516173");
            Assert.AreEqual("sfx_rumble_cruiser Sound", misc.Name);
        }
        #endregion
    }
}
