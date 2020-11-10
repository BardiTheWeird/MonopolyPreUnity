using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.Cash;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class TaxPerHouseActionBehavior : IActionBehavior
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public void Execute(int playerId, IMonopolyAction action)
        {
            int sum = 0;
            foreach (var propId in _context.GetPlayer(playerId).Properties)
            {
                var dev = _context.GetTileComponent<PropertyDevelopment>(propId);
                if (dev != null)
                    sum += dev.HousesBuilt * (action as TaxPerHouseAction).Amount;
            }
            _context.Add(new ChargeCash(sum, playerId, message: "for houses built"));
        }

        public TaxPerHouseActionBehavior(Context context) =>
            _context = context;
    }
}
