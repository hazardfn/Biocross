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
    public class ThrusterUnit
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

        #region Thruster Class
        [Test]
        [Category("Gamedata")]
        [Category("Thruster")]
        public void testThrusterBasic()
        {
            Thruster thruster = new Thruster("2851612992", di);
            Assert.AreEqual("Deluxe Thruster", thruster.Name);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Thruster")]
        
        public void testThrusterTypeCheck()
        {
            // This is a Power Generator, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Thruster("2182579915", di));
        }

        [Test]
        [Category("Gamedata")]
        [Category("Thruster")]
        public void testThrusterSetIDS()
        {
            Thruster thruster = new Thruster("2851612992", di);
            Assert.AreEqual("Deluxe Thruster", thruster.Name);

            thruster = thruster.setIDS("2314747200");
            Assert.AreEqual("Heavy Thruster", thruster.Name);
        }
        #endregion
    }
}
