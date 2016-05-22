using System;
using System.IO;
using System.Reflection;
using Biocross.Data;
using Biocross.Core.Item;
using NUnit.Framework;
using Biocross.Core.Exceptions;

namespace Biocross.Core.NUnit.Item
{
    [TestFixture]
    public class FactionUnit
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

        #region Faction Class
        [Test]
        [Category("Gamedata")]
        [Category("Faction")]
        public void testFactionBasic()
        {
            Faction faction = new Faction("li_p_grp", 0.9, di);
            Assert.AreEqual("Liberty Police, Inc.", faction.Name);
            Assert.AreEqual(0.9, faction.Reputation);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Faction")]
        public void testFactionSetReputation()
        {
            Faction faction = new Faction("li_p_grp", 0.9, di);
            Assert.AreEqual("Liberty Police, Inc.", faction.Name);
            Assert.AreEqual(0.9, faction.Reputation);

            faction = faction.setReputation(0.1);
            Assert.AreEqual("Liberty Police, Inc.", faction.Name);
            Assert.AreEqual(0.1, faction.Reputation);
        }

        [Test]
        [Category("Gamedata")]
        [Category("Faction")]
        public void testFactionTypeCheck()
        {
            // This is a system, Should error!
            Assert.Throws<GamedataMismatchException>(() => new Faction("Li05", 0.8, di));
        }
        #endregion
    }
}
