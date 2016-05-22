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
    public class CountermeasureUnit
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

        #region Countermeasure Class
        [Test]
        [Category("Gamedata")]
        [Category("CM")]
        public void testCountermeasureBasic()
        {
            Countermeasure cm = new Countermeasure("2736156677", di);
            Assert.AreEqual("Adv. Countermeasure Dropper", cm.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("CM")]
        public void testCountermeasureTypeCheck()
        {
            // This is a projectile, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Countermeasure("3024613711", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("CM")]
        public void testCountermeasureSetIDS()
        {
            Countermeasure cm = new Countermeasure("2736156677", di);
            Assert.AreEqual("Adv. Countermeasure Dropper", cm.Name);

            cm = cm.setIDS("2199282693");
            Assert.AreEqual("Imp. Countermeasure Dropper", cm.Name);
        }
        #endregion
    }
}
