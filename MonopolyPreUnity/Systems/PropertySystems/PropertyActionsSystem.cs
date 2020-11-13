using MonopolyPreUnity.Components.SystemRequest.PropertyActions;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems.PropertySystems
{
    class PropertyActionsSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            foreach (var action in _context.GetComponentsInterface<IPropertyAction>())
            {
                var player = _context.GetPlayer(action.PlayerId);
                var propId = action.PropertyId;

                switch (action)
                {
                    case MortgageProperty mortgage:
                        _context.Mortgage(player, propId);
                        break;
                    case UnmortgageProperty unmortgage:
                        _context.UnMortgage(player, propId);
                        break;
                    case BuildHouse buyHouse:
                        _context.BuyHouse(player, propId);
                        break;
                    case SellHouse sell:
                        _context.SellHouse(player, propId);
                        break;
                }
            }
            _context.RemoveInterface<IPropertyAction>();
        }

        #region ctor
        public PropertyActionsSystem(Context context) =>
            _context = context;
        #endregion
    }
}
