using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Light : Gamedata
    {
        /// <summary>
        /// Returns a light item with name.
        /// </summary>
        /// <param name="IDS">IDS of the light</param>
        public Light(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATALIGHTS, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new light</param>
        /// <returns>Light object</returns>
        public Light setIDS(string IDS)
        {
            return new Light(IDS, base.di);
        }
        #endregion
    }
}
