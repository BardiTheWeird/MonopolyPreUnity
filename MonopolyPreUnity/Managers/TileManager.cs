using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class TileManager
    {
        private readonly Dictionary<int, Tile> tileDict;
        private readonly Dictionary<int, HashSet<int>> propertySetDict;
        private readonly Dictionary<SpecialTile, int> specialTileDict;

        #region GetTile
        /// <summary>
        /// Get Tile. General Tile use, I guess?
        /// </summary>
        /// <param name="tileId"></param>
        /// <returns></returns>
        public Tile GetTile(int tileId) =>
            tileDict[tileId];

        public T GetTileComponent<T>(int tileId)
        {
            T component;
            if ((component = (T)tileDict[tileId].Components.Find(x => x.GetType() == typeof(T))) == null)
            {
                return component;
            }
            return default(T);
        }

        public bool GetTileComponent<T>(int tileId, out T component)
        {
            component = GetTileComponent<T>(tileId);
            if (component == null)
                return false;
            return true;
        }
        #endregion

        /// <summary>
        /// Get all the ids of properties in the given set
        /// </summary>
        /// <param name="setId"></param>
        /// <returns></returns>
        public HashSet<int> GetPropertySet(int setId) =>
            propertySetDict[setId];

        public int GetSpecialTileId(SpecialTile specialTile) =>
            specialTileDict[specialTile];
    }
}
