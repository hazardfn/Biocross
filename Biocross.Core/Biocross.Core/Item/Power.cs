using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Power : Gamedata
    {
        /// <summary>
        /// Returns a power item with name.
        /// </summary>
        /// <param name="IDS">IDS of the power item</param>
        public Power(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATAPOWER, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new power item</param>
        /// <returns>power object</returns>
        public Power setIDS(string IDS)
        {
            return new Power(IDS, base.di);
        }
        #endregion
    }
}
