using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Scanner : Gamedata
    {
        /// <summary>
        /// Returns a scanner item with name.
        /// </summary>
        /// <param name="IDS">IDS of the projectile item</param>
        public Scanner(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATASCANNERS, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new scanner item</param>
        /// <returns>scanner object</returns>
        public Scanner setIDS(string IDS)
        {
            return new Scanner(IDS, base.di);
        }
        #endregion
    }
}
