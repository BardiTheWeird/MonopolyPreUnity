﻿using System;
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
        private Dictionary<int, int> mapIndex;

        #region dependencies
        private readonly PlayerManager _playerManager;
        private readonly TileManager _tileManager;
        #endregion

        public MapManager(PlayerManager playerManager, TileManager tileManager)
        {
            _playerManager = playerManager;
            _tileManager = tileManager;
        }

        private void BuildMap()
        {
            throw new NotImplementedException();
        }

        private void IndexMap()
        {
            throw new NotImplementedException();
        }

        private bool GOPassed(int tileStartId, int tileEndId, int playerId)
        {
            int tileStartIndex = mapIndex[tileStartId];
            int tileEndIndex = mapIndex[tileEndId];


            if (tileEndId == _tileManager.GetSpecialTileId<GoComponent>())
            {
                return true;
            }
            else if (tileEndIndex < tileStartIndex)
            {
                int startTileId = _tileManager.GetSpecialTileId<GoComponent>();
                _tileManager.GetTileContent<GoComponent>(startTileId).OnPlayerLanded(playerId);

                return true;
            }
            
            return false;
        }

        public int MoveBySteps(int playerId, int steps)
        {   
            Player player = _playerManager.GetPlayer(playerId);
            int tileIndex = mapIndex[player.CurrentTileId];

            int newTileIndex = (tileIndex + steps) % map.Count;
            player.CurrentTileId = map[newTileIndex];

            GOPassed(player.CurrentTileId, map[newTileIndex], playerId);
            
            return map[newTileIndex];
        }

        public void MoveToTile(int playerId, int tileId, bool giveGOCash)
        {
            Player player = _playerManager.GetPlayer(playerId);

            if(giveGOCash)
                GOPassed(player.CurrentTileId, tileId, playerId);

            player.CurrentTileId = tileId;
        }

        public int MoveByFunc(int playerId, Func<Tile, bool> predicate)
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
                GOPassed(player.CurrentTileId, map[i], playerId);
                player.CurrentTileId = map[i];
            }
            return map[i];
        }

    }
}
