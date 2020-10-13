using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class TileManager
    {
        private readonly Dictionary<int, ITile> tileDict;
        private readonly Dictionary<int, HashSet<int>> setDict;

        #region GetTile
        /// <summary>
        /// Get barebones ITile. Use for getting generic attributes like Name
        /// </summary>
        /// <param name="tileId"></param>
        /// <returns></returns>
        public ITile GetTile(int tileId) =>
            tileDict[tileId];

        /// <summary>
        /// Get an instance of class that implements ITile. Use for getting class-specific attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tileId"></param>
        /// <returns></returns>
        public T GetTile<T>(int tileId) where T : ITile
        {
            return (T)tileDict[tileId];
        }
        #endregion

        public HashSet<int> GetSet(int setId) =>
            setDict[setId];
    }
}
