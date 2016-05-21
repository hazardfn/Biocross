using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Thruster : Gamedata
    {
        /// <summary>
        /// Returns a thruster item with name.
        /// </summary>
        /// <param name="IDS">IDS of the thruster</param>
        public Thruster(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATATHRUSTERS, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new thruster</param>
        /// <returns>A thruster</returns>
        public Thruster setIDS(string IDS)
        {
            return new Thruster(IDS, base.di);
        }
        #endregion
    }
}
