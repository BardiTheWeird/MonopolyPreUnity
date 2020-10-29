using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;

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

        private List<int> map; 
        private Dictionary<int, int> mapIndex; // <tileId, tileIndex in MapIdSequence>

        #region dependencies
        private readonly PlayerManager _playerManager;
        private readonly TileManager _tileManager;
        private readonly PlayerLandedManager _playerLandedManager;
        #endregion

        #region Constructor
        public MapManager(PlayerManager playerManager, 
            TileManager tileManager,
            GameData gameData,
            PlayerLandedManager playerLandedManager)
        {
            _playerManager = playerManager;
            _tileManager = tileManager;
            map = gameData.MapIdSequence;
            mapIndex = gameData.MapIndex;
            _playerLandedManager = playerLandedManager;
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

        #region Misc Methods
        private bool GOPassed(int tileStartId, int tileEndId, int playerId)
        {
            return true;
            //int tileStartIndex = mapIndex[tileStartId];
            //int tileEndIndex = mapIndex[tileEndId];

            //if (_tileManager.ContainsComponent<GoComponent>(tileEndId))
            //{
            //    return true;
            //}
            //else if (tileEndIndex < tileStartIndex)
            //{
            //    int GOTileId = _tileManager.GetTileWithComponent<GoComponent>();
            //    _playerLandedManager.PlayerLanded(playerId, GOTileId);

            //    return true;
            //}
            
            //return false;
        }
        #endregion

        #region Move methods
        public int MoveBySteps(int playerId, int steps, bool giveGOCash=true)
        {
            Player player = _playerManager.GetPlayer(playerId);
            int tileIndex = mapIndex[player.CurrentTileId];

            int newTileIndex = (tileIndex + steps) % map.Count;
            
            if (giveGOCash)
            {
                GOPassed(player.CurrentTileId, map[newTileIndex], playerId);
            }
            player.CurrentTileId = map[newTileIndex];

            return player.CurrentTileId;
        }

        public int MoveToTile(int playerId, int tileId, bool giveGOCash=true)
        {
            Player player = _playerManager.GetPlayer(playerId);

            if(giveGOCash)
                GOPassed(player.CurrentTileId, tileId, playerId);

            player.CurrentTileId = tileId;
            return player.CurrentTileId;
        }

        public int MoveByFunc(int playerId, Func<Tile, bool> predicate, bool giveGOCash=true)
        {
            Player player = _playerManager.GetPlayer(playerId);

            int currentTileIndex = mapIndex[player.CurrentTileId], i = currentTileIndex;
            do
            {
                i %= map.Count;
                if (predicate(_tileManager.GetTile(mapIndex[i])))
                {
                    break;
                }
                i++;

            } while(i != currentTileIndex);

            if (i == currentTileIndex)
            {
                throw new TileNotFoundException("Woops, this is a nasty BUG. There is no such tile on the map.");
            }
            else
            {   
                if(giveGOCash)
                    GOPassed(player.CurrentTileId, map[i], playerId);
                player.CurrentTileId = map[i];
            }
            return map[i];
        }
        #endregion
    }
}
