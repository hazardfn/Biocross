using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Misc : Gamedata
    {
        /// <summary>
        /// Returns a misc item with name.
        /// </summary>
        /// <param name="IDS">IDS of the misc item</param>
        public Misc(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATAMISC, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new misc item</param>
        /// <returns>misc object</returns>
        public Misc setIDS(string IDS)
        {
            return new Misc(IDS, base.di);
        }
        #endregion
    }
}
