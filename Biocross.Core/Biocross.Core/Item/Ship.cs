using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Ship : Gamedata
    {
        /// <summary>
        /// Returns a ship item with name.
        /// </summary>
        /// <param name="IDS">IDS of the ship item</param>
        public Ship(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATASHIPS, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new ship item</param>
        /// <returns>ship object</returns>
        public Ship setIDS(string IDS)
        {
            return new Ship(IDS, base.di);
        }
        #endregion
    }
}
