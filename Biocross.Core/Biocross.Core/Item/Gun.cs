using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Gun : Gamedata
    {
        /// <summary>
        /// Returns a gun item with name.
        /// </summary>
        /// <param name="IDS">IDS of the gun</param>
        public Gun(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATAGUNS, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new gun</param>
        /// <returns>Gun object</returns>
        public Gun setIDS(string IDS)
        {
            return new Gun(IDS, base.di);
        }
        #endregion
    }
}
