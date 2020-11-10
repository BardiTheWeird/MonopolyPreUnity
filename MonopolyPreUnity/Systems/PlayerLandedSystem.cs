using Autofac.Features.Indexed;
using MonopolyPreUnity.Behaviors;
using MonopolyPreUnity.Components.SystemState;
using MonopolyPreUnity.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    sealed class PlayerLandedSystem : ISystem
    {
        #region fields
        private readonly IIndex<Type, IPlayerLandedBehavior> _playerLandedBehaviorindex;
        private readonly Context _context;
        #endregion

        public void Execute()
        {
            foreach (var playerLanded in _context.GetComponents<PlayerLanded>())
            {
                var player = _context.GetPlayer(playerLanded.PlayerId);
                foreach (var component in _context.GetTileComponents(player.CurTileId))
                {
                    if (_playerLandedBehaviorindex.TryGetValue(component.GetType(), out var behavior))
                        behavior.PlayerLanded(player, component);
                }
            }
            _context.Remove<PlayerLanded>();
        }

        #region ctor
        public PlayerLandedSystem(IIndex<Type, IPlayerLandedBehavior> playerLandedBehaviorindex, Context context)
        {
            _playerLandedBehaviorindex = playerLandedBehaviorindex;
            _context = context;
        }
        #endregion
    }
}
