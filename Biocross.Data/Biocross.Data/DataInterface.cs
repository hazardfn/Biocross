using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Biocross.Data
{
    public class DataInterface : IDisposable
    {
        #region Database Connection Objects
        private System.Data.SqlClient.SqlConnection con;
        private System.Data.SqlClient.SqlCommand com;

        private readonly string DbLocation = @Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\biodata.mdf";
        private readonly string GamedataLocation = "GAMEDATA/";
        #endregion

        /// <summary>
        /// Create a new connection to the database
        /// </summary>
        public DataInterface()
        {
            openConnection();
        }
        /// <summary>
        /// Creates a new connection to a specified DB
        /// </summary>
        /// <param name="DbLocation">MDF location</param>
        public DataInterface(string DbLocation)
        {
            this.DbLocation = DbLocation;

            openConnection();
        }
        /// <summary>
        /// Creates a new connection to a specified DB
        /// and also changes the location where Gamedata is stored
        /// </summary>
        /// <param name="DbLocation">MDF Location</param>
        /// <param name="GamedataLocation">Gamedata folder</param>
        public DataInterface(string DbLocation, string GamedataLocation)
        {
            this.DbLocation = DbLocation;
            this.GamedataLocation = GamedataLocation;

            openConnection();
        }

        private void openConnection()
        {
            con = new System.Data.SqlClient.SqlConnection();
            con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + DbLocation + ";Integrated Security=True";
            con.Open();
        }

        /// <summary>
        /// Initialise a clean database
        /// </summary>
        public void init()
        {
            cleanDB();
            importGamedata(GamedataSchema.GAMEDATAAMMO);
            importGamedata(GamedataSchema.GAMEDATABASES);
            importGamedata(GamedataSchema.GAMEDATACARGO);
            importGamedata(GamedataSchema.GAMEDATACM);
            importGamedata(GamedataSchema.GAMEDATAENGINES);
            importGamedata(GamedataSchema.GAMEDATAFACTIONS);
            importGamedata(GamedataSchema.GAMEDATAGUNS);
            importGamedata(GamedataSchema.GAMEDATALIGHTS);
            importGamedata(GamedataSchema.GAMEDATAMINES);
            importGamedata(GamedataSchema.GAMEDATAMISC);
            importGamedata(GamedataSchema.GAMEDATAPOWER);
            importGamedata(GamedataSchema.GAMEDATAPROJECTILES);
            importGamedata(GamedataSchema.GAMEDATASCANNERS);
            importGamedata(GamedataSchema.GAMEDATASHIELDS);
            importGamedata(GamedataSchema.GAMEDATASHIPS);
            importGamedata(GamedataSchema.GAMEDATASYSTEMS);
            importGamedata(GamedataSchema.GAMEDATATHRUSTERS);
            importGamedata(GamedataSchema.GAMEDATATRACTORBEAMS);
            importGamedata(GamedataSchema.GAMEDATATURRETS);
        }

        /// <summary>
        /// Cleans the Gamedata
        /// </summary>
        private void cleanDB()
        {
            com = new SqlCommand("DELETE FROM Gamedata", con);
            com.ExecuteNonQuery();
        }

        /// <summary>
        /// Retrieves a name from the GAMEDATA
        /// using the IDS.
        /// </summary>
        /// <param name="IDS">ID of the item</param>
        /// <returns>Name of the object</returns>
        public string getNameFromIDS(string IDS)
        {
            SqlDataReader sqlReader;

            List<string> Names = new List<string>();

            com = new SqlCommand("SELECT Name FROM Gamedata WHERE IDS=@IDS", con);
            com.Parameters.Add("IDS", SqlDbType.VarChar);
            com.Parameters["IDS"].Value = IDS.ToLower();

            using (sqlReader = com.ExecuteReader())
            {
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        Names.Add(sqlReader.GetString(0));
                    }
                }
            }

            if(Names.Count > 1)
            {
                throw new DuplicateNameException("There is a duplicate IDS entry in your data: " + IDS);
            } else if(Names.Count == 0)
            {
                throw new NullReferenceException("IDS: " + IDS + " not found!");
            }

            return Names[0];
        }

        /// <summary>
        /// Retrieves the type of the GAMEDATA using the IDS.
        /// </summary>
        /// <param name="IDS">ID of the item</param>
        /// <returns></returns>
        public string getTypeFromIDS(string IDS)
        {
            SqlDataReader sqlReader;

            List<string> Types = new List<string>();

            com = new SqlCommand("SELECT Type FROM Gamedata WHERE IDS=@IDS", con);
            com.Parameters.Add("IDS", SqlDbType.VarChar);
            com.Parameters["IDS"].Value = IDS.ToLower();

            using (sqlReader = com.ExecuteReader())
            {
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        Types.Add(sqlReader.GetString(0));
                    }
                }
            }

            if (Types.Count > 1)
            {
                throw new DuplicateNameException("There is a duplicate IDS entry in your data: " + IDS);
            }
            else if (Types.Count == 0)
            {
                throw new NullReferenceException("IDS: " + IDS + " not found!");
            }

            return Types[0];
        }

        /// <summary>
        /// Imports gamedata from a given XML
        /// </summary>
        /// <param name="xml">Name of xml file to import</param>
        private void importGamedata(string xml)
        {
            DataSet DS = new DataSet();
            DS.ReadXml(GamedataLocation + xml + ".xml");

            foreach(DataRow dr in DS.Tables[0].Rows)
            {
                string IDS = dr["IDS"].ToString().ToLower();
                string Name = dr["RealName"].ToString();
                com = new SqlCommand("INSERT INTO Gamedata(IDS, Name, Type) " +
                                     "VALUES(@IDS, @Name, @Type)",
                                     con);

                com.Parameters.Add("IDS", SqlDbType.VarChar);
                com.Parameters["IDS"].Value = IDS;
                com.Parameters.Add("Name", SqlDbType.VarChar);
                com.Parameters["Name"].Value = Name;
                com.Parameters.Add("Type", SqlDbType.VarChar);
                com.Parameters["Type"].Value = xml;

                com.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Disposes the connection on GC
        /// </summary>
        public void Dispose()
        {
            if (com != null)
            {
                com.Dispose();
            }

            if (con != null)
            {
                con.Close();
                con.Dispose();
            }
        }
    }
}
