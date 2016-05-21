using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Base : Gamedata
    {
        /// <summary>
        /// Returns a base item with name.
        /// </summary>
        /// <param name="IDS">IDS of the base</param>
        public Base(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATABASES, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new base</param>
        /// <returns>New base</returns>
        public Base setIDS(string IDS)
        {
            return new Base(IDS, base.di);
        }
        #endregion
    }
}
