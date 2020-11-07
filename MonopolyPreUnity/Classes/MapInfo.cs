using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    #region Exceptions
    class MapException : Exception
    {
        public MapException() { }
        public MapException(string message) : base(message) { }
    }
    #endregion
    class MapInfo
    {
        public int? GoId { get; set; }
        public int? JailId { get; set; }

        public MapInfo(GameData gameData)
        {
            foreach (var tile in gameData.TileDict)
            {
                foreach (var component in tile.Value.Components)
                {
                    switch (component)
                    {
                        case GoComponent go:
                            if (GoId == null)
                                GoId = tile.Key;
                            else
                                throw new MapException("More than one tile with GoComponent");
                            break;
                        case JailComponent jail:
                            if (JailId == null)
                                JailId = tile.Key;
                            else
                                throw new MapException("More than one tile with JailComponent");
                            break;
                    }
                }
            }
        }
    }
}
