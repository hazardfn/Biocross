using Biocross.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public class Ammo : Gamedata
    {        
        /// <summary>
        /// The quantity of the item.
        /// </summary>
        public readonly int Quantity;

        /// <summary>
        /// Returns the ammo item with name.
        /// </summary>
        /// <param name="IDS">IDS of the ammo item</param>
        /// <param name="Quantity">Quantity in the hold</param>
        public Ammo(string IDS, int Quantity, DataInterface di) : base(IDS, GamedataSchema.GAMEDATAAMMO, di)
        {
            this.Quantity = Quantity;
        }

        #region Methods
        /// <summary>
        /// Set the quantity of this item.
        /// </summary>
        /// <param name="Quantity">New Quantity</param>
        /// <returns>New ammo object with set quantity</returns>
        public Ammo setQuantity(int Quantity)
        {
            return new Ammo(this.IDS, Quantity, base.di);
        }
        /// <summary>
        /// Changes this item entirely but keeps the quantity
        /// good for swapping things out
        /// </summary>
        /// <param name="IDS">ID of the new ammo item</param>
        /// <returns>New ammo item</returns>
        public Ammo setIDS(string IDS)
        {
            return new Ammo(IDS, this.Quantity, base.di);
        }
        #endregion
    }
}
