using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Faction : Gamedata
    {
        #region Properties
        /// <summary>
        /// Reputation with the faction
        /// </summary>
        public readonly double Reputation;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a new faction
        /// </summary>
        /// <param name="IDS">IDS of the faction</param>
        /// <param name="Reputation">Reputation with the faction</param>
        public Faction(string IDS, double Reputation, DataInterface di) : base(IDS, GamedataSchema.GAMEDATAFACTIONS, di)
        {
            this.Reputation = Reputation;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a new faction with the set reputation
        /// </summary>
        /// <param name="Reputation">New reputation</param>
        /// <returns>Faction</returns>
        public Faction setReputation(double Reputation)
        {
            return new Faction(this.IDS, Reputation, base.di);
        }
        #endregion
    }
}
