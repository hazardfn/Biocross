using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Cargo : Gamedata
    {        
        /// <summary>
        /// The quantity of the item.
        /// </summary>
        public readonly int Quantity;

        /// <summary>
        /// Returns a cargo item with name.
        /// </summary>
        /// <param name="IDS">IDS of the cargo item</param>
        /// <param name="Quantity">Quantity in the hold</param>
        public Cargo(string IDS, int Quantity, DataInterface di) : base(IDS, GamedataSchema.GAMEDATACARGO, di)
        {
            this.Quantity = Quantity;
        }

        #region Methods
        /// <summary>
        /// Set the quantity of this item.
        /// </summary>
        /// <param name="Quantity">New Quantity</param>
        /// <returns>New cargo object with set quantity</returns>
        public Cargo setQuantity(int Quantity)
        {
            return new Cargo(this.IDS, Quantity, base.di);
        }
        /// <summary>
        /// Changes this item entirely but keeps the quantity
        /// good for swapping things out
        /// </summary>
        /// <param name="IDS">ID of the new cargo item</param>
        /// <returns></returns>
        public Cargo setIDS(string IDS)
        {
            return new Cargo(IDS, this.Quantity, base.di);
        }
        #endregion
    }
}
