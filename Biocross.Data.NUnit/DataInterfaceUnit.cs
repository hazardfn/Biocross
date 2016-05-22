using System;
using Biocross.Data;
using Biocross.Log;
using NUnit.Framework;
using System.Reflection;
using System.IO;

namespace Biocross.Data.NUnit
{
    [TestFixture]
    public class DataInterfaceUnit
    {
        private DataInterface di = new DataInterface(@Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location
            ) + @"\biodata.mdf", "TESTDATA/");

        #region Setup and Teardown
        [OneTimeSetUp]
        public void setup()
        {
            di.init();
        }

        [OneTimeTearDown]
        public void cleanup()
        {
            di.Dispose();
        }
        #endregion

        /// <summary>
        /// Checks we can retrieve a name from the IDS.
        /// </summary>
        [Test]
        [Category("Data Interface")]
        public void testGetName()
        {
            Assert.AreEqual("Slingshot Missile Ammo (Class 3)", di.getNameFromIDS("3130291785"));
        }

        /// <summary>
        /// Checks we can get a type from the IDS.
        /// </summary>
        [Test]
        [Category("Data Interface")]
        public void testGetType()
        {
            Assert.AreEqual("Ammo", di.getTypeFromIDS("3130291785"));
        }
    }
}
