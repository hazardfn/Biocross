using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Turret : Gamedata
    {
        /// <summary>
        /// Returns a turret item with name.
        /// </summary>
        /// <param name="IDS">IDS of the turret</param>
        public Turret(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATATURRETS, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new turret</param>
        /// <returns>Turret object</returns>
        public Turret setIDS(string IDS)
        {
            return new Turret(IDS, base.di);
        }
        #endregion
    }
}
