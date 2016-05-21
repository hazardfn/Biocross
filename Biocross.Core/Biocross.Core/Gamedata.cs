using System;
using Biocross.Data;
using Biocross.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biocross.Core.Item
{
    public abstract class Gamedata
    {
        #region Properties
        /// <summary>
        /// IDS of the item
        /// </summary>
        public readonly string IDS;
        /// <summary>
        /// The derived name of the item
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// The derived type of the item
        /// </summary>
        public readonly string Type;
        /// <summary>
        /// Interface to the database
        /// </summary>
        internal readonly DataInterface di;
        #endregion

        /// <summary>
        /// Any item which requires GAMEDATA in order to derive 
        /// the name. This is the base class of many others. 
        /// </summary>
        /// <param name="IDS">ID of the item</param>
        /// <param name="deriveName">Function which can derive the name
        /// given the IDS</param>
        internal Gamedata(string IDS, string expectedType, DataInterface di)
        {
            this.di = di;

            if (IDS != "-1")
            {
                this.IDS = IDS;
                this.Name = deriveName(IDS);
                this.Type = deriveType(IDS);

                typeCheck(expectedType);
            } else
            {
                this.Name = "No data found.";
            }
        }

        /// <summary>
        /// Does a check to see if the IDS received 
        /// is of the expected type
        /// </summary>
        /// <param name="expectedType"></param>
        public void typeCheck(string expectedType)
        {
            if (this.Type != expectedType)
            {
                throw new GamedataMismatchException("Item: " + this.IDS + " does not appear to be cargo");
            }
        }

        /// <summary>
        /// Gets the gamedata type based on the IDS
        /// </summary>
        /// <param name="IDS">ID of item</param>
        public string deriveType(string IDS)
        {
            return di.getTypeFromIDS(IDS);
        }

        /// <summary>
        /// Derives the name from the IDS
        /// </summary>
        /// <param name="IDS">ID of the item</param>
        /// <returns></returns>
        private string deriveName(string IDS)
        {
            return di.getNameFromIDS(IDS);
        }
    }
}
