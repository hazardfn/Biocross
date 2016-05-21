using System;
using System.IO;
using System.Collections.Generic;
using Biocross.Core.Item;
using Biocross.Data;
using Biocross.Log;
using Biocross.Log.FileBackend;
using System.Linq;
using System.Text;

namespace Biocross.Core
{
    public class Player : IDisposable
    {
        #region Native Methods
        /// <summary>
        /// Native Methods class for P/Invokes
        /// </summary>
        internal static class NativeMethods
        {
            #region FLCodec
            /// <summary>
            /// Import the FLCodec DLL which is used to decipher
            /// the .fl format
            /// </summary>
            /// <param name="nType">1 for decode 0 for encode</param>
            /// <param name="szSource">Source file</param>
            /// <param name="szDest">Destination file</param>
            /// <returns>0 on success</returns>
            [System.Runtime.InteropServices.DllImport("FLCodec.dll", EntryPoint = "flcodec", ExactSpelling = false,
                CharSet = System.Runtime.InteropServices.CharSet.Ansi,
                BestFitMapping = false,
                SetLastError = true)]
            public static extern IntPtr flcodec(IntPtr nType, string szSource, string szDest);
            #endregion
        }
        #endregion

        #region FL File Code
        /// <summary>
        /// Decodes a Freelancer player file in place.
        /// </summary>
        /// <param name="FLFile">Location of the .fl file</param>
        /// <returns>0 on success, any other number on failure</returns>
        public static IntPtr decodePlayerFile(string FLFile)
        {
            IntPtr retVal = NativeMethods.flcodec((IntPtr)1, FLFile, FLFile);
            normalizeFile(FLFile); // Things like FLHook can cause funky fl files

            return retVal;
        }
        /// <summary>
        /// Encodes a Freelancer player file in place.
        /// </summary>
        /// <param name="FLFile">Location of player file</param>
        /// <returns></returns>
        public static IntPtr encodePlayerFile(string FLFile)
        {
            return NativeMethods.flcodec((IntPtr)2, FLFile, FLFile);
        }

        /// <summary>
        /// Normalises the file by trimming spaces and converting
        /// to lower case
        /// </summary>
        /// <param name="FLFile">Location of the fl file</param>
        private static void normalizeFile(string FLFile)
        {
            string contents;

            using (StreamReader sr = new StreamReader(FLFile))
            {
                contents = sr.ReadToEnd()
                             .ToLower()
                             .Trim()
                             .Replace(" ", "");
            }

            using (StreamWriter sw = new StreamWriter(FLFile))
            {
                sw.Write(contents);
                sw.Flush();
            }
        }
        /// <summary>
        /// Checks if an FL file is encoded.
        /// </summary>
        /// <param name="FLFile">Location of the fl file</param>
        /// <returns></returns>
        public static bool isEncoded(string FLFile)
        {
            using (StreamReader f = new StreamReader(FLFile))
            {
                return f.ReadLine().Contains("FLS1");
            }
        }
        #endregion

        #region Properties
        #region General Player Data
        /// <summary>
        /// Players file location
        /// </summary>
        public readonly string FLFile;
        /// <summary>
        /// Actually seems to be the date the player
        /// was created... Leaving it as description
        /// so it matches the fl file.
        /// </summary>
        public const string PLAYERDESCRIPTION = "description";
        public readonly string Description;
        /// <summary>
        /// An unknown but required
        /// so it can be written back to the file.
        /// </summary>
        public const string PLAYERTIMESTAMP = "tstamp";
        public readonly string Timestamp;
        /// <summary>
        /// Player Name
        /// </summary>
        public const string PLAYERNAME = "name";
        public readonly string Name;
        /// <summary>
        /// Player Rank
        /// </summary>
        public const string PLAYERRANK = "rank";
        public readonly int Rank;
        /// <summary>
        /// Players current worth
        /// </summary>
        public const string PLAYERMONEY = "money";
        public readonly double Money;
        /// <summary>
        /// Number of kills by the player
        /// </summary>
        public const string PLAYERKILLS = "num_kills";
        public readonly int Kills;
        /// <summary>
        /// Number of missions player has completed
        /// successfully
        /// </summary>
        public const string PLAYERMISNSUCCESSES = "num_misn_successes";
        public readonly int SuccessfulMissions;
        /// <summary>
        /// Number of missions player has failed
        /// miserably
        /// </summary>
        public const string PLAYERMISNFAILURES = "num_misn_failures";
        public readonly int FailedMissons;
        /// <summary>
        /// Current system the player is in
        /// </summary>
        public const string PLAYERSYSTEM = "system";
        public readonly Item.System CurrentSystem;
        /// <summary>
        /// The players best faction alignment
        /// </summary>
        public const string PLAYERREPGROUP = "rep_group";
        public readonly Faction Alignment;
        /// <summary>
        /// Current base (if any) the player is in
        /// </summary>
        public const string PLAYERBASE = "base";
        public readonly Base CurrentBase;
        /// <summary>
        /// If player is not at a base, these are their
        /// coords in space
        /// </summary>
        public const string PLAYERPOS = "pos";
        public readonly string PositionData;
        /// <summary>
        /// If player is not at a base, this is how
        /// they are currently rotated in space
        /// </summary>
        public const string PLAYERROTATE = "rotate";
        public readonly string RotationData;
        /// <summary>
        /// The players last docked at base.
        /// </summary>
        public const string PLAYERLASTBASE = "last_base";
        public readonly Base LastBase;
        /// <summary>
        /// The players current ship
        /// </summary>
        public const string PLAYERSHIP = "ship_archetype";
        public readonly Ship Ship;
        /// <summary>
        /// Total cash earned
        /// </summary>
        public const string PLAYERTOTALCASH = "total_cash_earned";
        public readonly double totalCashEarned;
        /// <summary>
        /// Timespan indicating the time a player
        /// has spent in space.
        /// </summary>
        public const string PLAYERTOTALTIME = "total_time_played";
        public readonly TimeSpan PlayerTime;
        #endregion

        #region Factions, Equipment and Cargo
        public const string PLAYERCARGO = "cargo";
        public const string PLAYEREQUIPMENT = "equip";
        public const string PLAYERFACTION = "house";

        /// <summary>
        /// Ammo currently on the players ship
        /// </summary>
        public readonly List<Ammo> Ammo = new List<Item.Ammo>();
        /// <summary>
        /// Current reputation with all known factions
        /// </summary>
        public readonly List<Faction> Factions = new List<Faction>();
        /// <summary>
        /// Cargo currently on the players ship
        /// </summary>
        public readonly List<Cargo> Cargo = new List<Item.Cargo>();
        /// <summary>
        /// Countermeasures currently on the players ship
        /// </summary>
        public readonly List<Countermeasure> Countermeasures = new List<Countermeasure>();
        /// <summary>
        /// Engines currently on the players ship
        /// </summary>
        public readonly List<Engine> Engines = new List<Engine>();
        /// <summary>
        /// Guns currently on the players ship
        /// </summary>
        public readonly List<Gun> Guns = new List<Gun>();
        /// <summary>
        /// Lights currently on the players ship
        /// </summary>
        public readonly List<Light> Lights = new List<Light>();
        /// <summary>
        /// Mines currently on the players ship
        /// </summary>
        public readonly List<Mine> Mines = new List<Mine>();
        /// <summary>
        /// Misc. equipment on the players ship
        /// </summary>
        public readonly List<Misc> Misc = new List<Item.Misc>();
        /// <summary>
        /// Power generator(s) fitted to the ship
        /// </summary>
        public readonly List<Power> Power = new List<Item.Power>();
        /// <summary>
        /// Projectile launchers fitted to the ship
        /// </summary>
        public readonly List<Projectile> Projectiles = new List<Projectile>();
        /// <summary>
        /// Scanner(s) fitted to the ship
        /// </summary>
        public readonly List<Scanner> Scanners = new List<Scanner>();
        /// <summary>
        /// Shield(s) fitted to the ship
        /// </summary>
        public readonly List<Shield> Shields = new List<Shield>();
        /// <summary>
        /// Thrusters fitted to the ship
        /// </summary>
        public readonly List<Thruster> Thrusters = new List<Thruster>();
        /// <summary>
        /// Tractor Beam fitted to the ship
        /// </summary>
        public readonly List<TractorBeam> TractorBeams = new List<TractorBeam>();
        /// <summary>
        /// Turret(s) fitted to the ship
        /// </summary>
        public readonly List<Turret> Turrets = new List<Turret>();
        #endregion

        #region Unused Data
        private string pInterface;
        private string pCanDock;
        private string pCanTl;
        private string sysVisited;
        private string baseVisited;
        private List<string> baseData = new List<string>();
        private List<string> visitedData = new List<string>();
        private List<string> lockedData = new List<string>();
        #endregion

        /// <summary>
        /// Data interface provides
        /// access to the database.
        /// </summary>
        internal DataInterface di;
        /// <summary>
        /// The raw key value pair list representing the file
        /// </summary>
        internal List<KeyValuePair<string, string>> currentFileRepRaw;
        /// <summary>
        /// Representation of current file.
        /// </summary>
        private Lookup<string, string> currentFileRep;
        /// <summary>
        /// File event handler used to write
        /// to the player log.
        /// </summary>
        internal static FileEventHandler feh = new FileEventHandler("player.log");
        /// <summary>
        /// 6 level default logger.
        /// </summary>
        internal Logger log = new Logger(feh);
        #endregion

        #region File Parser
        /// <summary>
        /// Decodes the players name in the FL File.
        /// </summary>
        /// <param name="Name">Unicode bytes of the players name</param>
        /// <returns>Players Name</returns>
        public static string decodeName(string Name)
        {
            if (Name.Length % 4 != 0)
            {
                return "";
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i <= Name.Length - 1; i += 4)
            {
                sb.Append(Convert.ToChar(
                          char.ConvertFromUtf32((
                          int.Parse((
                          Name.Substring(i, 4)),
                          System.Globalization.NumberStyles.HexNumber)))));
            }

            return sb.ToString();
        }
        /// <summary>
        /// Parses an FL File.
        /// </summary>
        /// <param name="FLFile">Location of FL File to parse</param>
        private static List<KeyValuePair<string, string>> parseFile(string FLFile)
        {
            List<KeyValuePair<string, string>> retVal = new List<KeyValuePair<string, string>>();

            using (StreamReader sr = new StreamReader(FLFile))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] lineSplit = line.Split('=');

                    if (lineSplit.Count() == 2)
                    {
                        string Key = lineSplit[0];
                        string Value = lineSplit[1];

                        retVal.Add(
                            new KeyValuePair<string, string>(Key, Value)
                        );
                    } else if (lineSplit.Count() == 1)
                    {
                        string Key = lineSplit[0];

                        if (Key != "")
                            retVal.Add(new KeyValuePair<string, string>(Key, ""));
                    }
                }
            }


            return retVal;
        }
        #endregion

        #region API
        /// <summary>
        /// Populates the class based on a key value dictionary
        /// same structure is used as found in FL Files
        /// e.g house=0.91,li_n_grp, house is the Key
        /// the rest is the value
        /// </summary>
        /// <param name="parsedFile">A dictionary constructed from a player-file</param>
        internal Player(string fileLocation, List<KeyValuePair<string, string>> parsedFile, DataInterface di)
        {
            initLogger();

            this.di = di;
            this.FLFile = fileLocation;
            this.currentFileRepRaw = parsedFile;
            this.currentFileRep = (Lookup<string, string>)parsedFile.ToLookup(
                (item) => item.Key,
                (item) => item.Value
           );

            this.Description = decodeName(this.currentFileRep["description"].First());
            writeLog(Logger.LogLevels.DEBUG, "ReadDescription", "Description: " + this.Description);
            this.Timestamp = this.currentFileRep["tstamp"].First();
            writeLog(Logger.LogLevels.DEBUG, "ReadTimestamp", "Timestamp: " + this.Timestamp);

            // Parsing is done in the order
            // they appear in a default
            // FL File
            this.Name = decodeName(this.currentFileRep["name"].First());
            writeLog(Logger.LogLevels.DEBUG, "ReadPlayerName", "Name: " + this.Name);
            this.Rank = int.Parse(this.currentFileRep["rank"].First());
            writeLog(Logger.LogLevels.DEBUG, "ReadPlayerRank", "Rank: " + this.Rank);

            // Factions
            foreach (string s in this.currentFileRep["house"])
            {
                string[] factionSplit = s.Split(',');
                string Rep = factionSplit[0];
                string IDS = factionSplit[1];
                Faction f = new Faction(IDS, double.Parse(Rep), di);

                writeLog(Logger.LogLevels.DEBUG, "ReadPlayerFaction",
                    "Reputation: " +
                    f.Reputation +
                    " Faction: " +
                    f.Name);

                Factions.Add(f);
            }

            // Alignment
            string alignmentIDS = this.currentFileRep["rep_group"].First();
            this.Alignment = Factions.Find(x =>
                x.IDS == alignmentIDS
            );

            writeLog(Logger.LogLevels.DEBUG, "ReadPlayerAlignment",
            "Reputation: " +
            Alignment.Reputation +
            " Faction: " +
            Alignment.Name);

            // Money
            this.Money = double.Parse(this.currentFileRep["money"].First());
            writeLog(Logger.LogLevels.DEBUG, "ReadPlayerMoney",
            "Money: " +
            Money);

            // Kills
            this.Kills = int.Parse(this.currentFileRep["num_kills"].First());
            writeLog(Logger.LogLevels.DEBUG, "ReadPlayerKills",
            "Kills: " +
            Kills);

            // Missions
            this.SuccessfulMissions = int.Parse(this.currentFileRep["num_misn_successes"].First());
            this.FailedMissons = int.Parse(this.currentFileRep["num_misn_failures"].First());

            writeLog(Logger.LogLevels.DEBUG, "ReadMissionData",
            "Failed: " +
            FailedMissons +
            " Success: " +
            SuccessfulMissions);

            // System
            this.CurrentSystem = new Item.System(this.currentFileRep["system"].First(), di);

            writeLog(Logger.LogLevels.DEBUG, "ReadCurrentSystem", "Current System: " + this.CurrentSystem.Name);

            // Base
            try
            {
                this.CurrentBase = new Base(this.currentFileRep["base"].First(), di);
                writeLog(Logger.LogLevels.DEBUG, "ReadCurrentBase", "Current Base: " + this.CurrentBase.Name);
            } catch (InvalidOperationException)
            {
                this.CurrentBase = new Base("-1", di);
                writeLog(Logger.LogLevels.DEBUG, "ReadCurrentBase", "Current Base: " + this.CurrentBase.Name);
                this.PositionData = this.currentFileRep["pos"].First();
                writeLog(Logger.LogLevels.DEBUG, "ReadPositionData", "Position Data: " + this.PositionData);
                this.RotationData = this.currentFileRep["rotate"].First();
                writeLog(Logger.LogLevels.DEBUG, "ReadRotationData", "Rotation Data: " + this.RotationData);

                writeLog(Logger.LogLevels.INFO, "PlayerReadCurrentBase", "Couldn't find current base, player likely in space");
            }

            this.Ship = new Ship(this.currentFileRep["ship_archetype"].First(), di);
            writeLog(Logger.LogLevels.DEBUG, "ReadShip", "Current Ship: " + this.Ship.Name);

            // Equipment
            foreach (string s in this.currentFileRep["equip"])
            {
                string[] equipmentSplit = s.Split(',');
                string equipmentIDS = equipmentSplit[0];
                int equipmentQuantity = int.Parse(equipmentSplit[2]);

                addEquipment(di.getTypeFromIDS(equipmentIDS), equipmentIDS, equipmentQuantity);
            }

            // Cargo
            foreach (string s in this.currentFileRep["cargo"])
            {
                string[] cargoSplit = s.Split(',');
                string cargoIDS = cargoSplit[0];
                int cargoQuantity = int.Parse(cargoSplit[1]);

                Cargo c = new Cargo(cargoIDS, cargoQuantity, di);
                Cargo.Add(c);

                writeLog(Logger.LogLevels.DEBUG, "ReadCargo", "Cargo: " + c.Name + " Quantity: " + c.Quantity);
            }

            // Last base
            string lastbaseIDS = this.currentFileRep["last_base"].First();
            this.LastBase = new Base(lastbaseIDS, di);
            writeLog(Logger.LogLevels.DEBUG, "ReadLastBase", "Last Base: " + this.LastBase.Name);

            // Total Cash Earned
            double totalcashearned = double.Parse(this.currentFileRep["total_cash_earned"].First());
            this.totalCashEarned = totalcashearned;
            writeLog(Logger.LogLevels.DEBUG, "ReadTotalCashEarned", "Total Cash: " + this.totalCashEarned);

            // Total Time Played
            double timePlayed = double.Parse(this.currentFileRep["total_time_played"].First());
            PlayerTime = new TimeSpan(0, 0, (int)timePlayed);
            writeLog(Logger.LogLevels.DEBUG, "ReadTimePlayed", "Hours: " + this.PlayerTime.Hours + " Minutes: " + this.PlayerTime.Minutes + " Seconds: " + this.PlayerTime.Seconds);

            //Useless shit but needed to write the file back
            baseData.Add(this.currentFileRep["base_hull_status"].First());
            baseData.AddRange(this.currentFileRep["base_collision_group"]);
            baseData.AddRange(this.currentFileRep["base_equip"]);
            baseData.AddRange(this.currentFileRep["base_cargo"]);

            visitedData.AddRange(this.currentFileRep["visit"]);

            lockedData.AddRange(this.currentFileRep["locked_gate"]);

            pInterface = this.currentFileRep["interface"].First();
            pCanDock = this.currentFileRep["can_dock"].First();
            pCanTl = this.currentFileRep["can_tl"].First();
            sysVisited = this.currentFileRep["sys_visited"].First();
            baseVisited = this.currentFileRep["base_visited"].First();
        }
        /// <summary>
        /// Reads player information from an FL File.
        /// </summary>
        /// <param name="FLFile">Location of the player file</param>
        public Player(string FLFile, DataInterface di) : this(FLFile, parseFile(FLFile), di)
        {
        }
        /// <summary>
        /// Saves a player file when given a key value pair file representation
        /// e.g. {name, 0004081279} translates to name=0004081279. 
        /// </summary>
        /// <param name="SaveLocation">File location to save to</param>
        /// <param name="player">Player to save</param>
        public static Player savePlayer(string SaveLocation, Player player, DataInterface di)
        {
            using (StreamWriter sw = new StreamWriter(SaveLocation))
            {
                foreach (KeyValuePair<string, string> s in player.currentFileRepRaw)
                {
                    if (s.Value != "")
                    {
                        sw.WriteLine(s.Key + "=" + s.Value);
                    } else
                    {
                        sw.WriteLine(s.Key);
                    }
                }
            }

            return new Player(SaveLocation, di);
        }
        /// <summary>
        /// Saves a player file when given a key value pair file representation
        /// e.g. {name, 0004081279} translates to name=0004081279. 
        /// 
        /// This call saves in the location contained in the Player
        /// </summary>
        /// <param name="player">Player to save</param>
        /// <param name="di">Data Interface</param>
        public static Player savePlayer(Player player, DataInterface di)
        {
            using (StreamWriter sw = new StreamWriter(player.FLFile))
            {
                foreach (KeyValuePair<string, string> s in player.currentFileRepRaw)
                {
                    if (s.Value != "")
                    {
                        sw.WriteLine(s.Key + "=" + s.Value);
                    }
                    else
                    {
                        sw.WriteLine(s.Key);
                    }
                }
            }

            return new Player(player.FLFile, di);
        }
        /// <summary>
        /// Updates a players information then saves them.
        /// </summary>
        /// <param name="SaveLocation">Location to save the file</param>
        /// <param name="UpdatedKeys">A list of keys and their changed values</param>
        /// <param name="di"></param>
        /// <returns></returns>
        public static Player alterPlayer(Player player, List<KeyValuePair<string, string>> UpdatedKeys, DataInterface di)
        {
            Player retVal = player;
            foreach (KeyValuePair<string, string> kv in UpdatedKeys)
            {
                switch (kv.Key)
                {
                    case "house":
                        string secondHouseKey = kv.Value.Split(',')[1];
                        retVal = updateTwoKeyEntry(retVal, kv.Key, secondHouseKey, kv.Value);
                        break;
                    case "equip":
                        string secondEquipKey = kv.Value.Split(',')[0];
                        retVal = updateTwoKeyEntry(retVal, kv.Key, secondEquipKey, kv.Value);
                        break;
                    case "cargo":
                        string secondCargoKey = kv.Value.Split(',')[0];
                        retVal = updateTwoKeyEntry(retVal, kv.Key, secondCargoKey, kv.Value);
                        break;
                    case "name":
                        player.writeLog(Logger.LogLevels.WARN, "UnsupportedOperation", "Updating a players name is not permitted at this time");
                        break;
                    default:
                        retVal = updateSingleKeyEntry(retVal, kv.Key, kv.Value);
                        break;
                }
            }

            return retVal;
        }
        #endregion

        #region Internal Functions
        private static Player updateSingleKeyEntry(Player player, string key, string newValue)
        {
            foreach (KeyValuePair<string, string> kv in player.currentFileRepRaw)
            {
                if (kv.Key == key)
                {
                    int itemIndex = player.currentFileRepRaw.IndexOf(kv);
                    player.currentFileRepRaw.RemoveAt(itemIndex);
                    player.currentFileRepRaw.Insert(itemIndex, new KeyValuePair<string, string>(key, newValue));
                    return player;
                }
            }

            throw new KeyNotFoundException("Biocross was unable to update a multikey for some reason. Check the code!");
        }
        private static Player updateTwoKeyEntry(Player player, string key, string secondKey, string newValue)
        {
            foreach (KeyValuePair<string,string> kv in player.currentFileRepRaw)
            {
                if(kv.Key == key && kv.Value.Contains(secondKey))
                {
                    int itemIndex = player.currentFileRepRaw.IndexOf(kv);
                    player.currentFileRepRaw.RemoveAt(itemIndex);
                    player.currentFileRepRaw.Insert(itemIndex, new KeyValuePair<string, string>(key, newValue));
                    return player;
                }
            }

            throw new KeyNotFoundException("Biocross was unable to update a multikey for some reason. Check the code!");
        }
        private void addEquipment(string expectedType, string IDS, int Quantity)
        {
            switch(expectedType)
            {
                case GamedataSchema.GAMEDATAAMMO:
                    Ammo a = new Ammo(IDS, Quantity, di);
                    Ammo.Add(a);
                    writeLog(Logger.LogLevels.DEBUG, "ReadAmmo", "Ammo: " + a.Name + "Quantity: " + a.Quantity);
                    break;
                case GamedataSchema.GAMEDATACARGO:
                    Cargo c = new Cargo(IDS, Quantity, di);
                    Cargo.Add(c);
                    writeLog(Logger.LogLevels.DEBUG, "ReadCargo", "Cargo: " + c.Name + "Quantity: " + c.Quantity);
                    break;
                case GamedataSchema.GAMEDATACM:
                    Countermeasure cm = new Countermeasure(IDS, di);
                    Countermeasures.Add(cm);
                    writeLog(Logger.LogLevels.DEBUG, "ReadCM", "CM: " + cm.Name);
                    break;
                case GamedataSchema.GAMEDATAENGINES:
                    Engine e = new Engine(IDS, di);
                    Engines.Add(e);
                    writeLog(Logger.LogLevels.DEBUG, "ReadEngine", "Engine: " + e.Name);
                    break;
                case GamedataSchema.GAMEDATAGUNS:
                    Gun g = new Gun(IDS, di);
                    Guns.Add(g);
                    writeLog(Logger.LogLevels.DEBUG, "ReadGun", "Gun: " + g.Name);
                    break;
                case GamedataSchema.GAMEDATALIGHTS:
                    Light l = new Light(IDS, di);
                    Lights.Add(l);
                    writeLog(Logger.LogLevels.DEBUG, "ReadLight", "Light: " + l.Name);
                    break;
                case GamedataSchema.GAMEDATAMINES:
                    Mine m = new Mine(IDS, di);
                    Mines.Add(m);
                    writeLog(Logger.LogLevels.DEBUG, "ReadMine", "Mine: " + m.Name);
                    break;
                case GamedataSchema.GAMEDATAMISC:
                    Misc mi = new Misc(IDS, di);
                    Misc.Add(mi);
                    writeLog(Logger.LogLevels.DEBUG, "ReadMisc", "Misc: " + mi.Name);
                    break;
                case GamedataSchema.GAMEDATAPOWER:
                    Power p = new Power(IDS, di);
                    Power.Add(p);
                    writeLog(Logger.LogLevels.DEBUG, "ReadPower", "Power: " + p.Name);
                    break;
                case GamedataSchema.GAMEDATAPROJECTILES:
                    Projectile pr = new Projectile(IDS, di);
                    Projectiles.Add(pr);
                    writeLog(Logger.LogLevels.DEBUG, "ReadProjectile", "Projectile: " + pr.Name);
                    break;
                case GamedataSchema.GAMEDATASCANNERS:
                    Scanner s = new Scanner(IDS, di);
                    Scanners.Add(s);
                    writeLog(Logger.LogLevels.DEBUG, "ReadScanner", "Scanner: " + s.Name);
                    break;
                case GamedataSchema.GAMEDATASHIELDS:
                    Shield sh = new Shield(IDS, di);
                    Shields.Add(sh);
                    writeLog(Logger.LogLevels.DEBUG, "ReadShield", "Shield: " + sh.Name);
                    break;
                case GamedataSchema.GAMEDATATHRUSTERS:
                    Thruster t = new Thruster(IDS, di);
                    Thrusters.Add(t);
                    writeLog(Logger.LogLevels.DEBUG, "ReadThruster", "Thruster: " + t.Name);
                    break;
                case GamedataSchema.GAMEDATATRACTORBEAMS:
                    TractorBeam tb = new TractorBeam(IDS, di);
                    TractorBeams.Add(tb);
                    writeLog(Logger.LogLevels.DEBUG, "ReadTractor", "Tractor: " + tb.Name);
                    break;
                case GamedataSchema.GAMEDATATURRETS:
                    Turret tu = new Turret(IDS, di);
                    Turrets.Add(tu);
                    writeLog(Logger.LogLevels.DEBUG, "ReadTurret", "Turret: " + tu.Name);
                    break;
            }
        }
        internal void writeLog(Logger.LogLevels level, string tag, string message)
        {
            if (this.FLFile == null)
            {
                log.log((int)level, tag, message);
            } else
            {
                log.log((int)level, "[" + this.FLFile + "] " + tag, message);
            }
        }
        private void initLogger()
        {
            feh.start();
            feh.setAppend(true);
            log.setMaximumLogLevel(2);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                feh.abort();
                feh.shutdown();
                log.shutdown();
            }
        }

        #endregion
    }
}
