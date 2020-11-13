using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.Move;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
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
                    case MoveType moveType:
                        MoveType(player, moveType);
                        break;
                }
                _context.Add(new PlayerLanded(player.Id));
            }
            _context.RemoveInterface<IMoveRequest>();
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
            if (goId != null && countGoPassed && (fullLap || IsGoPassed(player.CurTileId, newTileId)))
                _context.Add(new GoPassed(player.Id));

            // actual move
            player.CurTileId = newTileId;
        }

        public void MoveBySteps(Player player, MoveSteps moveSteps) =>
            MoveBySteps(player, moveSteps.Steps, moveSteps.CountGoPassed);

        public void MoveBySteps(Player player, int steps, bool countGoPassed)
        {
            var mapSize = _context.MapInfo().MapSize;

            int curTilePosition = _context.GetTileId(player.CurTileId).MapPosition;
            int newTilePosition = (curTilePosition + steps) % mapSize;
            int newTileId = _context.GetTilePosition(newTilePosition).Id;

            MoveToTile(player, newTileId, countGoPassed, steps >= mapSize);
        }

        public void MoveDice(Player player, MoveDice moveDice)
        {
            var dice = _context.GetComponent<Dice>();
            player.CanMove = false;
            if (dice.IsPair && moveDice.MoveAgainAllowed)
            {
                player.CanMove = true;
                _context.Add(new PrintLine("You got a pair, so you can move again", OutputStream.GameLog));
            }
            MoveBySteps(player, dice.Sum, moveDice.CountGoPassed);
        }

        public void MoveType(Player player, MoveType moveType)
        {
            Func<Tile, bool> pred = x => 
                x.ContainsComponent(moveType.ComponentType, _context);

            MoveByFunc(player, pred, moveType.CountGoPassed);
        }

        public void MoveByFunc(Player player, Func<Tile, bool> predicate, bool countGoPassed)
        {
            int curTilePosition = _context.GetPosition(player.CurTileId);
            int mapSize = _context.MapInfo().MapSize;

            for (int i = (curTilePosition + 1) % mapSize; i != curTilePosition; i = (i + 1) % mapSize)
            {
                var curTile = _context.GetTilePosition(i);
                if (predicate(curTile))
                {
                    MoveToTile(player, curTile.Id, countGoPassed);
                    return;
                }
            }
            _context.Add(new PrintLine("No tile found for a given predicate", OutputStream.GameLog));
        }
        #endregion

        #region ctor
        public MoveSystem(Context context) =>
            _context = context;
        #endregion
    }
}
