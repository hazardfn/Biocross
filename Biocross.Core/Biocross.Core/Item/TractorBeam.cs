using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class TractorBeam : Gamedata
    {
        /// <summary>
        /// Returns a tractor beam item with name.
        /// </summary>
        /// <param name="IDS">IDS of the tractor beam</param>
        public TractorBeam(string IDS, DataInterface di) : base(IDS, GamedataSchema.GAMEDATATRACTORBEAMS, di)
        {
        }

        #region Methods
        /// <summary>
        /// Changes this item entirely.
        /// </summary>
        /// <param name="IDS">ID of the new tractor beam</param>
        /// <returns>A tractor beam</returns>
        public TractorBeam setIDS(string IDS)
        {
            return new TractorBeam(IDS, base.di);
        }
        #endregion
    }
}
