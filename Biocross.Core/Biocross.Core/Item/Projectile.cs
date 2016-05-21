using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Projectile : Gamedata
    {
        /// <summary>
        /// Returns a projectile item with name.
        /// </summary>
        /// <param name="IDS">IDS of the projectile item</param>
        public Projectile(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATAPROJECTILES, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new projectile item</param>
        /// <returns>projectile object</returns>
        public Projectile setIDS(string IDS)
        {
            return new Projectile(IDS, base.di);
        }
        #endregion
    }
}
