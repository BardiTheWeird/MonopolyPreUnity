using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.Move;
using MonopolyPreUnity.Components.SystemState;
using MonopolyPreUnity.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class MoveSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            var requests = _context.GetComponentsInterface<IMoveRequest>();

            foreach (var request in requests)
            {
                var player = _context.GetPlayer(request.PlayerId);
                switch (request)
                {
                    case MoveSteps steps:
                        MoveBySteps(player, steps);
                        break;
                    case MoveTileId tileId:
                        MoveToTile(player, tileId);
                        break;
                    case MoveDice moveDice:
                        MoveDice(player, moveDice);
                        break;
                }
                _context.Add(new PlayerLanded(player.Id));
            }
            _context.RemoveEntitiesInterface<IMoveRequest>();
        }

        #region GoPassed
        // Full "laps" are handled in MoveBySteps
        private bool IsGoPassed(int tileStartId, int tileEndId)
        {
            int goPosition = _context.GetPosition((int)_context.MapInfo().GoId);
            int tileStartPosition = _context.GetPosition(tileStartId);
            int tileEndPosition = _context.GetPosition(tileEndId);

            if (goPosition <= tileEndPosition
                && (tileStartPosition < goPosition || tileEndPosition < tileStartPosition))
                return true;

            return false;
        }
        #endregion

        #region Move methods
        public void MoveToTile(Player player, MoveTileId moveTileId) =>
            MoveToTile(player, moveTileId.TileId, moveTileId.CountGoPassed);

        public void MoveToTile(Player player, int newTileId, bool countGoPassed = true, bool fullLap = false)
        {
            // check if passed GO
            var goId = _context.MapInfo().GoId;
            if (goId != null && countGoPassed && (fullLap || IsGoPassed(player.CurrentTileId, newTileId)))
                _context.Add(new GoPassed(player.Id));

            // actual move
            player.CurrentTileId = newTileId;
        }

        public void MoveBySteps(Player player, MoveSteps moveSteps) =>
            MoveBySteps(player, moveSteps.Steps, moveSteps.CountGoPassed);

        public void MoveBySteps(Player player, int steps, bool countGoPassed)
        {
            var mapSize = _context.MapInfo().MapSize;

            int curTilePosition = _context.GetTileId(player.CurrentTileId).MapPosition;
            int newTilePosition = (curTilePosition + steps) % mapSize;
            int newTileId = _context.GetTilePosition(newTilePosition).Id;

            MoveToTile(player, newTileId, countGoPassed, steps >= mapSize);
        }

        public void MoveDice(Player player, MoveDice moveDice)
        {
            var dice = _context.GetComponent<Dice>();
            if (!dice.IsPair)
                player.CanMove = false;
            MoveBySteps(player, dice.Sum, moveDice.CountGoPassed);
        }

        //public int MoveByFunc(int playerId, Func<Tile, bool> predicate, bool giveGOCash = true)
        //{
        //    Player player = _playerManager.GetPlayer(playerId);
        //    int currentTileIndex = mapIndex[player.CurrentTileId];

        //    for (int i = (currentTileIndex + 1) % map.Count; i != currentTileIndex; i = (i + 1) % map.Count)
        //    {
        //        if (predicate(_tileManager.GetTile(mapIndex[i])))
        //            return MoveToTile(playerId, map[i], giveGOCash);
        //    }

        //    throw new TileNotFoundException("No tile found for a given predicate.");
        //}
        #endregion

        #region ctor
        public MoveSystem(Context context) =>
            _context = context;
        #endregion
    }
}
