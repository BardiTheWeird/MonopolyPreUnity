using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class TileManager
    {
        private readonly Dictionary<int, Tile> tileDict;
        private readonly Dictionary<int, HashSet<int>> propertySetDict;

        #region GetTile
        /// <summary>
        /// Get Tile. General Tile use, I guess?
        /// </summary>
        /// <param name="tileId"></param>
        /// <returns></returns>
        public Tile GetTile(int tileId) =>
            tileDict[tileId];

        /// <summary>
        /// Get an Identity component. For stuff like Id and Name
        /// </summary>
        /// <param name="tileId"></param>
        /// <returns></returns>
        public TileIdentityComponent GetTileIdentity(int tileId) =>
            tileDict[tileId].IdentityComponent;

        /// <summary>
        /// Get a Content component of a Tile
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tileId"></param>
        /// <returns></returns>
        public T GetTileContent<T>(int tileId) where T : ITileContentComponent =>
            (T)tileDict[tileId].ContentComponent;
        #endregion

        /// <summary>
        /// Get all the ids of properties in the given set
        /// </summary>
        /// <param name="setId"></param>
        /// <returns></returns>
        public HashSet<int> GetPropertySet(int setId) =>
            propertySetDict[setId];

        public int GetSpecialTileId<T>() where T : ITileContentComponent
        {
            throw new NotImplementedException();
        }
    }
}
