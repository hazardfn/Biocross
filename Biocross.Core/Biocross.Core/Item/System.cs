using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class System : Gamedata
    {
        /// <summary>
        /// Returns a system with name.
        /// </summary>
        /// <param name="IDS">IDS of the system</param>
        public System(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATASYSTEMS, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new system</param>
        /// <returns>New system</returns>
        public System setIDS(string IDS)
        {
            return new System(IDS, base.di);
        }
        #endregion
    }
}
