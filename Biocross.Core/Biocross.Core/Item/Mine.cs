using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Mine : Gamedata
    {
        /// <summary>
        /// Returns a mine item with name.
        /// </summary>
        /// <param name="IDS">IDS of the mine</param>
        public Mine(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATAMINES, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new mine</param>
        /// <returns>mine object</returns>
        public Mine setIDS(string IDS)
        {
            return new Mine(IDS, base.di);
        }
        #endregion
    }
}
