using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
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

        public HashSet<int> GetPropertySet(int setId) =>
            propertySetDict[setId];

        public int GetTileWithComponent<T>() =>
            tileDict.FirstOrDefault(x => x.Value.GetType() == typeof(T)).Key;
    }
}
