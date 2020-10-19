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
        #region fields
        private readonly Dictionary<int, Tile> _tileDict;
        private readonly Dictionary<int, HashSet<int>> _propertySetDict;
        #endregion

        #region GetTile
        /// <summary>
        /// Get Tile. General Tile use, I guess?
        /// </summary>
        /// <param name="tileId"></param>
        /// <returns></returns>
        public Tile GetTile(int tileId) =>
            _tileDict[tileId];

        public T GetTileComponent<T>(int tileId) where T : ITileComponent
        {
            T component;
            if ((component = (T)_tileDict[tileId].Components.Find(x => x.GetType() == typeof(T))) == null)
            {
                return component;
            }
            return default;
        }

        public bool GetTileComponent<T>(int tileId, out T component) where T : ITileComponent
        {
            component = GetTileComponent<T>(tileId);
            if (component == null)
                return false;
            return true;
        }

        public int GetTileWithComponent<T>() where T : ITileComponent =>
            _tileDict.FirstOrDefault(x => x.Value.GetType() == typeof(T)).Key;

        public bool ContainsComponent<T>(int tileId) where T : ITileComponent =>
            _tileDict[tileId].Components.FirstOrDefault(x => x.GetType() == typeof(T)) != null;

        public List<int> GetAllTilesWithComponent<T>() where T : ITileComponent =>
            _tileDict
            .Where(x => ContainsComponent<T>(x.Key))
            .Select(x => x.Key)
            .ToList();
        #endregion

        #region GetPropertySet
        public HashSet<int> GetPropertySet(int setId) =>
            _propertySetDict[setId];
        #endregion

        #region Constructor
        public TileManager(GameData gameData)
        {
            _tileDict = gameData.TileDict;
            _propertySetDict = gameData.PropertySetDict;
        }
        #endregion
    }
}
