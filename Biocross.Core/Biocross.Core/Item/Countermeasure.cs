using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Countermeasure : Gamedata
    {
        /// <summary>
        /// Returns a countermeasure item with name.
        /// </summary>
        /// <param name="IDS">IDS of the countermeasure</param>
        public Countermeasure(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATACM, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new countermeasure</param>
        /// <returns>The new countermeasure</returns>
        public Countermeasure setIDS(string IDS)
        {
            return new Countermeasure(IDS, base.di);
        }
        #endregion
    }
}
