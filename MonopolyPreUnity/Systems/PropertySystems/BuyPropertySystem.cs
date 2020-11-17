using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.Cash;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput.Property;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems.PropertySystems
{
    class BuyPropertySystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            var buyProperty = _context.GetComponent<BuyProperty>();
            if (buyProperty == null)
                return;

            var player = _context.GetPlayer(buyProperty.BuyerId);
            var prop = _context.GetTileComponent<Property>(buyProperty.PropertyId);

            _context.Add(new ChargeCash(prop.BasePrice, player.Id, message: "for property acquisition"));
            _context.Add(new PropertyTransferRequest(buyProperty.PropertyId, player.Id));

            _context.Remove<BuyProperty>();
        }

        public BuyPropertySystem(Context context)
        {
            _context = context;
        }
    }
}
