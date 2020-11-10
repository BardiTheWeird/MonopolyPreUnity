using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.Cash;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput.Property;
using MonopolyPreUnity.Entity;
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

            _context.Add(new ChargeCash(prop.BasePrice, player.Id, message: "property acquisition"));
            player.Properties.Add(buyProperty.PropertyId);
            prop.OwnerId = player.Id;

            _context.Remove<BuyProperty>();
        }

        public BuyPropertySystem(Context context)
        {
            _context = context;
        }
    }
}
