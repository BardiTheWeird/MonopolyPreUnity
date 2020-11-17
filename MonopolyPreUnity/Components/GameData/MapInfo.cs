using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    #region Exceptions
    class MapException : Exception
    {
        public MapException() { }
        public MapException(string message) : base(message) { }
    }
    #endregion
    class MapInfo : IEntityComponent
    {
        public int? GoId { get; set; }
        public int? JailId { get; set; }

        public int MapSize { get; set; }

        public MapInfo(int? goId, int? jailId, int mapSize)
        {
            GoId = goId;
            JailId = jailId;
            MapSize = mapSize;
        }

        public MapInfo()
        {
        }
    }
}
