using Autofac.Features.Indexed;
using MonopolyPreUnity.Behaviors.Rent;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.Cash;
using MonopolyPreUnity.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class RentSystem : ISystem
    {
        #region fields
        private readonly Context _context;
        private readonly IIndex<Type, IRentBehavior> _rentBehaviorIndex;
        #endregion

        public void Execute()
        {
            foreach (var rent in _context.GetComponents<CollectRent>())
            {
                var rentee = _context.GetPlayer(rent.RenteeId);
                var tile = _context.GetTileId(rentee.CurrentTileId);
                var prop = _context.GetTileComponent<Property>(tile.Id);
                foreach (var component in _context.GetTileComponents(tile.Id))
                {
                    if (_rentBehaviorIndex.TryGetValue(component.GetType(), out var behavior))
                    {
                        var rentAmount = behavior.GetRent(rentee.Id, (int)prop.OwnerId, component, tile.Id);
                        _context.Add(new ChargeCash(rentAmount, rentee.Id, prop.OwnerId, "for rent"));
                    }
                }
            }
            _context.Remove<CollectRent>();
        }

        #region ctor
        public RentSystem(Context context, IIndex<Type, IRentBehavior> rentBehaviorIndex)
        {
            _context = context;
            _rentBehaviorIndex = rentBehaviorIndex;
        }
        #endregion
    }
}
