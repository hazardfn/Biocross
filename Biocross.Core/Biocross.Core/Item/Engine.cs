using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Engine : Gamedata
    {
        /// <summary>
        /// Returns an engine item with name.
        /// </summary>
        /// <param name="IDS">IDS of the engine</param>
        public Engine(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATAENGINES, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new engine</param>
        /// <returns>An engine</returns>
        public Engine setIDS(string IDS)
        {
            return new Engine(IDS, base.di);
        }
        #endregion
    }
}
