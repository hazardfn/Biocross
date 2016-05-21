using System;
using Biocross.Data;
using Biocross.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;

namespace Biocross.Data.NUnit
{
    [TestClass]
    public class DataInterfaceUnit
    {
        private static DataInterface di = new DataInterface(@Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location
            ) + @"\biodata.mdf", "TESTDATA/");

        #region Setup and Teardown
        [ClassInitialize]
        public static void setup(TestContext c)
        {
            di.init();
        }

        [ClassCleanup]
        public static void cleanup()
        {
            di.Dispose();
        }
        #endregion

        /// <summary>
        /// Checks we can retrieve a name from the IDS.
        /// </summary>
        [TestMethod]
        [TestCategory("Data Interface")]
        public void testGetName()
        {
            Assert.AreEqual("Slingshot Missile Ammo (Class 3)", di.getNameFromIDS("3130291785"));
        }

        /// <summary>
        /// Checks we can get a type from the IDS.
        /// </summary>
        [TestMethod]
        [TestCategory("Data Interface")]
        public void testGetType()
        {
            Assert.AreEqual("Ammo", di.getTypeFromIDS("3130291785"));
        }
    }
}
