using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Shield : Gamedata
    {
        /// <summary>
        /// Returns a shield item with name.
        /// </summary>
        /// <param name="IDS">IDS of the shield item</param>
        public Shield(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATASHIELDS, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new shield item</param>
        /// <returns>shield object</returns>
        public Shield setIDS(string IDS)
        {
            return new Shield(IDS, base.di);
        }
        #endregion
    }
}
