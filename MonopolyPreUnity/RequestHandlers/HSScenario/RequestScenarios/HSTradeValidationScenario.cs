using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HSScenario
{
    class HSTradeValidationScenario : IHSRequestScenario
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public void RunScenario(IRequest request, Player player)
        {
            _context.HSInputState().Set(HSState.TradeValidation, player.Id);
        }

        #region ctor
        public HSTradeValidationScenario(Context context) =>
            _context = context;
        #endregion
    }
}
