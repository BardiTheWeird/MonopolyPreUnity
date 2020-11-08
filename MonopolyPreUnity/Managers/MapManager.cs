using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.UI;

namespace MonopolyPreUnity.Managers
{
    class MapManager
    {
        #region CustomException
        class TileNotFoundException : Exception
        {
            public TileNotFoundException()
            {

            }

            public TileNotFoundException(string message) : base(message)
            {

            }
        }
        #endregion

        #region fields
        private List<int> map; 
        private Dictionary<int, int> mapIndex; // <tileId, tileIndex in MapIdSequence>
        #endregion

        #region constants
        private readonly int _cashPerLap;
        #endregion

        #region dependencies
        private readonly PlayerManager _playerManager;
        private readonly TileManager _tileManager;
        private readonly MapInfo _mapInfo;
        private readonly ConsoleUI _consoleUI;
        #endregion

        #region Constructor
        public MapManager(PlayerManager playerManager, 
            TileManager tileManager,
            GameData gameData,
            GameConfig config,
            MapInfo mapInfo,
            ConsoleUI consoleUI)
        {
            _playerManager = playerManager;
            _tileManager = tileManager;
            map = gameData.MapIdSequence;
            mapIndex = gameData.MapIndex;
            _cashPerLap = config.CashPerLap;
            _mapInfo = mapInfo;
            _consoleUI = consoleUI;
        }
        #endregion

        #region Map methods
        private void BuildMap()
        {
            throw new NotImplementedException();
        }

        private void IndexMap()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GoPassed
        // Full "laps" are handled in MoveBySteps
        private bool GoPassed(int tileStartId, int tileEndId)
        {
            if (_mapInfo.GoId == null)
                return false;

            int goIndex = mapIndex[(int)_mapInfo.GoId];
            int tileStartIndex = mapIndex[tileStartId];
            int tileEndIndex = mapIndex[tileEndId];

            if (goIndex <= tileEndIndex
                && (tileStartIndex < goIndex || tileEndIndex < tileStartIndex))
                return true;

            return false;
        }

        private void OnGoPassed(int playerId)
        {
            _consoleUI.PrintFormatted($"|player:{playerId}| has passed a GO Tile!");
            _playerManager.PlayerCashGive(playerId, _cashPerLap);
        }
        #endregion

        #region Move methods
        public int MoveToTile(int playerId, int tileId, bool giveGOCash = true, bool fullLap = false)
        {
            Player player = _playerManager.GetPlayer(playerId);

            if (giveGOCash && (fullLap || GoPassed(player.CurrentTileId, tileId)))
                OnGoPassed(playerId);

            return player.CurrentTileId = tileId;
        }

        public int MoveBySteps(int playerId, int steps, bool giveGOCash = true)
        {
            Player player = _playerManager.GetPlayer(playerId);
            int tileIndex = mapIndex[player.CurrentTileId];
            int newTileIndex = (tileIndex + steps) % map.Count;
            
            return MoveToTile(playerId, map[newTileIndex], giveGOCash, steps >= map.Count);
        }

        public int MoveByFunc(int playerId, Func<Tile, bool> predicate, bool giveGOCash=true)
        {
            Player player = _playerManager.GetPlayer(playerId);
            int currentTileIndex = mapIndex[player.CurrentTileId];

            for (int i = (currentTileIndex + 1) % map.Count; i != currentTileIndex ; i = (i + 1) % map.Count)
            {
                if (predicate(_tileManager.GetTile(mapIndex[i])))
                    return MoveToTile(playerId, map[i], giveGOCash);
            }

            throw new TileNotFoundException("No tile found for a given predicate.");
        }
        #endregion
    }
}
