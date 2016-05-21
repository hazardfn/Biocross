using System;
using System.IO;
using System.Reflection;
using Biocross.Data;
using Biocross.Core.Item;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace Biocross.Core.NUnit
{
    [TestClass]
    public class PlayerUnit
    {
        /// <summary>
        /// Location of an fl file for testing.
        /// Basically just the initial, default MP player.
        /// </summary>
        private static readonly string flFileLocation = @"test.fl";
        /// <summary>
        /// The default fl file but in space! :O
        /// </summary>
        private static readonly string flFileLocationSpace = @"testspace.fl";
        /// <summary>
        /// Access to the database
        /// </summary>
        private static DataInterface di = new DataInterface(@Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\biodata.mdf", "TESTDATA/");
        private static Player player;
        private static Player playerSpace;

        #region Setup and Teardown
        [ClassInitialize]
        public static void init(TestContext c)
        {
            if (Player.isEncoded(PlayerUnit.flFileLocation))
                Assert.AreEqual((IntPtr)0, Player.decodePlayerFile(PlayerUnit.flFileLocation));

            if (Player.isEncoded(PlayerUnit.flFileLocationSpace))
                Assert.AreEqual((IntPtr)0, Player.decodePlayerFile(PlayerUnit.flFileLocationSpace));

            PlayerUnit.di.init();

            PlayerUnit.player = new Player(PlayerUnit.flFileLocation, di);
            PlayerUnit.playerSpace = new Player(PlayerUnit.flFileLocationSpace, di);
        }

        [ClassCleanup]
        public static void cleanup()
        {
            PlayerUnit.di.Dispose();
        }

        [TestCleanup]
        public void testcleanup()
        {
            if (Player.isEncoded(PlayerUnit.flFileLocation))
                Assert.AreEqual((IntPtr)0, Player.decodePlayerFile(PlayerUnit.flFileLocation));

            if (Player.isEncoded(PlayerUnit.flFileLocationSpace))
                Assert.AreEqual((IntPtr)0, Player.decodePlayerFile(PlayerUnit.flFileLocationSpace));

            PlayerUnit.player = new Player(PlayerUnit.flFileLocation, PlayerUnit.di);
            PlayerUnit.playerSpace = new Player(PlayerUnit.flFileLocationSpace, PlayerUnit.di);
        }
        #endregion
        
        #region FLCodec
        /// <summary>
        /// Tests we can decode a player file.
        /// </summary>
        [TestMethod]
        [TestCategory("FL File")]
        public void canDecodeFLFile()
        {
            Assert.AreEqual((IntPtr)0, Player.encodePlayerFile(PlayerUnit.flFileLocation));
            Assert.AreEqual((IntPtr)0, Player.decodePlayerFile(PlayerUnit.flFileLocation));
            Assert.AreNotEqual(true, Player.isEncoded(PlayerUnit.flFileLocation));
        }

        /// <summary>
        /// Tests we can encode a player file.
        /// </summary>
        [TestMethod]
        [TestCategory("FL File")]
        public void canEncodeFLFile()
        {
            Assert.AreEqual((IntPtr)0, Player.encodePlayerFile(PlayerUnit.flFileLocation));
            Assert.AreEqual(true, Player.isEncoded(PlayerUnit.flFileLocation));
        }
        #endregion

        #region Player Tests
        #region Read Tests
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadName()
        {
            Assert.AreEqual(PlayerUnit.player.Name, "Test");
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadRank()
        {
            Assert.AreEqual(PlayerUnit.player.Rank, 1);
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadFactionData()
        {
            foreach(Faction f in player.Factions)
            {
                Assert.IsTrue(f.Reputation >=-1 && f.Reputation <=1);
                Assert.AreEqual(GamedataSchema.GAMEDATAFACTIONS, di.getTypeFromIDS(f.IDS));
            }
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadAlignment()
        {
            Faction f = player.Alignment;

            Assert.IsTrue(f.Reputation >= -1 && f.Reputation <= 1);
            Assert.AreEqual(GamedataSchema.GAMEDATAFACTIONS, di.getTypeFromIDS(f.IDS));
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadMoney()
        {
            Assert.AreEqual(2000, PlayerUnit.player.Money);
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadKills()
        {
            Assert.AreEqual(0, PlayerUnit.player.Kills);
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadMissionSuccess()
        {
            Assert.AreEqual(0, PlayerUnit.player.SuccessfulMissions);
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadFailedMissions()
        {
            Assert.AreEqual(0, PlayerUnit.player.FailedMissons);
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadSystem()
        {
            Assert.AreEqual(new Core.Item.System("li01", di).Name, PlayerUnit.player.CurrentSystem.Name);
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadCurrentBase()
        {
            Assert.AreEqual(new Base("li01_01_base", di).Name, PlayerUnit.player.CurrentBase.Name);
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadShip()
        {
            Assert.AreEqual(new Ship("2151746432", di).Name, PlayerUnit.player.Ship.Name);
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadEquipment()
        {
            Assert.AreEqual(new Shield("3088051465", di).Name, PlayerUnit.player.Shields[0].Name);
            Assert.AreEqual(new Engine("2493263750", di).Name, PlayerUnit.player.Engines[0].Name);
            Assert.AreEqual(new Power("2779996489", di).Name, PlayerUnit.player.Power[0].Name);
            Assert.AreEqual(new Scanner("2723858309", di).Name, PlayerUnit.player.Scanners[0].Name);
            Assert.AreEqual(new TractorBeam("2799531210", di).Name, PlayerUnit.player.TractorBeams[0].Name);
            Assert.AreEqual(new Thruster("2314753344", di).Name, PlayerUnit.player.Thrusters[0].Name);

            Assert.AreEqual(new Gun("3219265993", di).Name, PlayerUnit.player.Guns[0].Name);
            Assert.AreEqual(new Gun("3219265993", di).Name, PlayerUnit.player.Guns[1].Name);

            Assert.AreEqual(new Light("2265531853", di).Name, PlayerUnit.player.Lights[0].Name);
            Assert.AreEqual(new Light("2802020621", di).Name, PlayerUnit.player.Lights[1].Name);
            Assert.AreEqual(new Light("2802020621", di).Name, PlayerUnit.player.Lights[2].Name);
            Assert.AreEqual(new Light("2802020621", di).Name, PlayerUnit.player.Lights[3].Name);
            Assert.AreEqual(new Light("2494027655", di).Name, PlayerUnit.player.Lights[4].Name);
            Assert.AreEqual(new Light("2494027655", di).Name, PlayerUnit.player.Lights[5].Name);

            Assert.AreEqual(new Misc("2500775748", di).Name, PlayerUnit.player.Misc[0].Name);
            Assert.AreEqual(new Misc("2500775748", di).Name, PlayerUnit.player.Misc[1].Name);
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadCargo()
        {
            Assert.AreEqual(new Cargo("2596081674",3, di).Name, PlayerUnit.player.Cargo[0].Name);
            Assert.AreEqual(3, PlayerUnit.player.Cargo[0].Quantity);

            Assert.AreEqual(new Cargo("2911012559", 3, di).Name, PlayerUnit.player.Cargo[1].Name);
            Assert.AreEqual(3, PlayerUnit.player.Cargo[1].Quantity);
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadLastBase()
        {
            Assert.AreEqual(new Base("li01_01_base", di).Name, PlayerUnit.player.LastBase.Name);
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadTotalCashEarned()
        {
            Assert.AreEqual(0, PlayerUnit.player.totalCashEarned);
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadTimePlayed()
        {
            Assert.AreEqual(11, PlayerUnit.player.PlayerTime.Seconds);
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadPositionalData()
        {
            Assert.AreEqual(PlayerUnit.playerSpace.PositionData, "-33167.9,196.429,-27367.6");
            Assert.AreEqual(PlayerUnit.playerSpace.RotationData, "-156.426,-23.1356,178.895");
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadDescription()
        {
            Assert.AreEqual(PlayerUnit.player.Description, "09/27/15 14:38:22");
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Read Tests")]
        public void canReadTimestamp()
        {
            Assert.AreEqual(PlayerUnit.player.Timestamp, "30472498,690674592");
        }
        #endregion

        #region Write Tests
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Write Tests")]
        public void writeNameFails()
        {
            Assert.AreEqual(PlayerUnit.player.Name, "Test");
            List<KeyValuePair<string, string>> newName = new List<KeyValuePair<string, string>>();
            newName.Add(new KeyValuePair<string, string>("name", "BoyGeorge"));
            Player newPlayer = Player.alterPlayer(PlayerUnit.player, newName, di);
            Assert.AreEqual(Player.savePlayer("test_name.fl", newPlayer, di).Name, "Test");
            File.Delete("test_name.fl");
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Write Tests")]
        public void canWriteRank()
        {
            Assert.AreEqual(PlayerUnit.player.Rank, 1);
            List<KeyValuePair<string, string>> newRank = new List<KeyValuePair<string, string>>();
            string randomRank = new Random(1).Next(2, 100).ToString();
            newRank.Add(new KeyValuePair<string, string>("rank", randomRank));
            Player newPlayer = Player.alterPlayer(PlayerUnit.player, newRank, di);
            Assert.AreEqual(Player.savePlayer("test_rank.fl", newPlayer, di).Rank, Int32.Parse(randomRank));
            File.Delete("test_rank.fl");
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Write Tests")]
        public void canWriteFactionData()
        {
            List<KeyValuePair<string, string>> newFactions = new List<KeyValuePair<string, string>>();
            List<KeyValuePair<string, double>> IDSRepLink = new List<KeyValuePair<string, double>>();
            Random random = new Random();

            foreach(Faction f in PlayerUnit.player.Factions)
            {
                double newRep = Math.Round(random.NextDouble(),2);
                KeyValuePair<string, double> repLink = new KeyValuePair<string, double>(f.IDS, newRep);
                KeyValuePair<string, string> newFaction = new KeyValuePair<string, string>("house", string.Format("{0},{1}", newRep.ToString(), f.IDS));

                IDSRepLink.Add(repLink);
                newFactions.Add(newFaction);
            }

            Player newPlayer = Player.savePlayer("test_factions.fl", Player.alterPlayer(PlayerUnit.player, newFactions, di), di);

            foreach(Faction f in newPlayer.Factions)
            {
                KeyValuePair<string, double> factionSearch = new KeyValuePair<string, double>(f.IDS, f.Reputation);
                Assert.IsTrue(IDSRepLink.Contains(factionSearch));
            }

            File.Delete("test_factions.fl");
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Write Tests")]
        public void canWriteAlignment()
        {
            List<KeyValuePair<string, string>> newAlignment = new List<KeyValuePair<string, string>>();

            Faction f = player.Factions[player.Factions.Count -1];
            Assert.IsTrue(f.Reputation >= -1 && f.Reputation <= 1);
            Assert.AreEqual(GamedataSchema.GAMEDATAFACTIONS, di.getTypeFromIDS(f.IDS));

            KeyValuePair<string, string> alignment = new KeyValuePair<string, string>("rep_group", f.IDS);
            newAlignment.Add(alignment);

            Player newPlayer = Player.alterPlayer(PlayerUnit.player, newAlignment, di);
            Assert.AreEqual(Player.savePlayer("test_alignment.fl", newPlayer, di).Alignment.IDS,f.IDS);
            File.Delete("test_alignment.fl");
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Write Tests")]
        public void canWriteMoney()
        {
            Assert.AreEqual(PlayerUnit.player.Money, 2000);
            List<KeyValuePair<string, string>> newMoney = new List<KeyValuePair<string, string>>();
            string randomMoney = new Random(1).Next(2000, 999999).ToString();
            newMoney.Add(new KeyValuePair<string, string>("money", randomMoney));
            Player newPlayer = Player.alterPlayer(PlayerUnit.player, newMoney, di);
            Assert.AreEqual(Player.savePlayer("test_money.fl", newPlayer, di).Money, Int32.Parse(randomMoney));
            File.Delete("test_money.fl");
        }
        [TestMethod]
        [TestCategory("Player")]
        [TestCategory("Player Write Tests")]
        public void canWriteKills()
        {
            Assert.AreEqual(PlayerUnit.player.Kills, 0);
            List<KeyValuePair<string, string>> newKills = new List<KeyValuePair<string, string>>();
            string randomKills = new Random(1).Next(1, 1000).ToString();
            newKills.Add(new KeyValuePair<string, string>("num_kills", randomKills));
            Player newPlayer = Player.alterPlayer(PlayerUnit.player, newKills, di);
            Assert.AreEqual(Player.savePlayer("test_kills.fl", newPlayer, di).Kills, Int32.Parse(randomKills));
            File.Delete("test_kills.fl");
        }
        //[TestMethod]
        //[TestCategory("Player")]
        //[TestCategory("Player Read Tests")]
        //public void canReadMissionSuccess()
        //{
        //    Assert.AreEqual(0, PlayerUnit.player.SuccessfulMissions);
        //}
        //[TestMethod]
        //[TestCategory("Player")]
        //[TestCategory("Player Read Tests")]
        //public void canReadFailedMissions()
        //{
        //    Assert.AreEqual(0, PlayerUnit.player.FailedMissons);
        //}
        //[TestMethod]
        //[TestCategory("Player")]
        //[TestCategory("Player Read Tests")]
        //public void canReadSystem()
        //{
        //    Assert.AreEqual(new Core.Item.System("li01", di).Name, PlayerUnit.player.CurrentSystem.Name);
        //}
        //[TestMethod]
        //[TestCategory("Player")]
        //[TestCategory("Player Read Tests")]
        //public void canReadCurrentBase()
        //{
        //    Assert.AreEqual(new Base("li01_01_base", di).Name, PlayerUnit.player.CurrentBase.Name);
        //}
        //[TestMethod]
        //[TestCategory("Player")]
        //[TestCategory("Player Read Tests")]
        //public void canReadShip()
        //{
        //    Assert.AreEqual(new Ship("2151746432", di).Name, PlayerUnit.player.Ship.Name);
        //}
        //[TestMethod]
        //[TestCategory("Player")]
        //[TestCategory("Player Read Tests")]
        //public void canReadEquipment()
        //{
        //    Assert.AreEqual(new Shield("3088051465", di).Name, PlayerUnit.player.Shields[0].Name);
        //    Assert.AreEqual(new Engine("2493263750", di).Name, PlayerUnit.player.Engines[0].Name);
        //    Assert.AreEqual(new Power("2779996489", di).Name, PlayerUnit.player.Power[0].Name);
        //    Assert.AreEqual(new Scanner("2723858309", di).Name, PlayerUnit.player.Scanners[0].Name);
        //    Assert.AreEqual(new TractorBeam("2799531210", di).Name, PlayerUnit.player.TractorBeams[0].Name);
        //    Assert.AreEqual(new Thruster("2314753344", di).Name, PlayerUnit.player.Thrusters[0].Name);

        //    Assert.AreEqual(new Gun("3219265993", di).Name, PlayerUnit.player.Guns[0].Name);
        //    Assert.AreEqual(new Gun("3219265993", di).Name, PlayerUnit.player.Guns[1].Name);

        //    Assert.AreEqual(new Light("2265531853", di).Name, PlayerUnit.player.Lights[0].Name);
        //    Assert.AreEqual(new Light("2802020621", di).Name, PlayerUnit.player.Lights[1].Name);
        //    Assert.AreEqual(new Light("2802020621", di).Name, PlayerUnit.player.Lights[2].Name);
        //    Assert.AreEqual(new Light("2802020621", di).Name, PlayerUnit.player.Lights[3].Name);
        //    Assert.AreEqual(new Light("2494027655", di).Name, PlayerUnit.player.Lights[4].Name);
        //    Assert.AreEqual(new Light("2494027655", di).Name, PlayerUnit.player.Lights[5].Name);

        //    Assert.AreEqual(new Misc("2500775748", di).Name, PlayerUnit.player.Misc[0].Name);
        //    Assert.AreEqual(new Misc("2500775748", di).Name, PlayerUnit.player.Misc[1].Name);
        //}
        //[TestMethod]
        //[TestCategory("Player")]
        //[TestCategory("Player Read Tests")]
        //public void canReadCargo()
        //{
        //    Assert.AreEqual(new Cargo("2596081674", 3, di).Name, PlayerUnit.player.Cargo[0].Name);
        //    Assert.AreEqual(3, PlayerUnit.player.Cargo[0].Quantity);

        //    Assert.AreEqual(new Cargo("2911012559", 3, di).Name, PlayerUnit.player.Cargo[1].Name);
        //    Assert.AreEqual(3, PlayerUnit.player.Cargo[1].Quantity);
        //}
        //[TestMethod]
        //[TestCategory("Player")]
        //[TestCategory("Player Read Tests")]
        //public void canReadLastBase()
        //{
        //    Assert.AreEqual(new Base("li01_01_base", di).Name, PlayerUnit.player.LastBase.Name);
        //}
        //[TestMethod]
        //[TestCategory("Player")]
        //[TestCategory("Player Read Tests")]
        //public void canReadTotalCashEarned()
        //{
        //    Assert.AreEqual(0, PlayerUnit.player.totalCashEarned);
        //}
        //[TestMethod]
        //[TestCategory("Player")]
        //[TestCategory("Player Read Tests")]
        //public void canReadTimePlayed()
        //{
        //    Assert.AreEqual(11, PlayerUnit.player.PlayerTime.Seconds);
        //}
        //[TestMethod]
        //[TestCategory("Player")]
        //[TestCategory("Player Read Tests")]
        //public void canReadPositionalData()
        //{
        //    Assert.AreEqual(PlayerUnit.playerSpace.PositionData, "-33167.9,196.429,-27367.6");
        //    Assert.AreEqual(PlayerUnit.playerSpace.RotationData, "-156.426,-23.1356,178.895");
        //}
        //[TestMethod]
        //[TestCategory("Player")]
        //[TestCategory("Player Read Tests")]
        //public void canReadDescription()
        //{
        //    Assert.AreEqual(PlayerUnit.player.Description, "09/27/15 14:38:22");
        //}
        //[TestMethod]
        //[TestCategory("Player")]
        //[TestCategory("Player Read Tests")]
        //public void canReadTimestamp()
        //{
        //    Assert.AreEqual(PlayerUnit.player.Timestamp, "30472498,690674592");
        //}
        #endregion

        #endregion
    }
}
