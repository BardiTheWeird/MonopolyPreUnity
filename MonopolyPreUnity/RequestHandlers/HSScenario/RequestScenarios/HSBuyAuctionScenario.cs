using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HSScenario
{
    class HSBuyAuctionScenario : IHSRequestScenario
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public void RunScenario(IRequest requestIn, Player player)
        {
            var request = requestIn as BuyAuctionRequest;
            var availableActions = _context.GetBuyAuctionCommands(player, request.PropertyId);

            _context.Add(new HSCommandChoiceRequest(availableActions, player.Id));
            _context.Add(new PrintCommands(availableActions));
        }

        #region ctor
        public HSBuyAuctionScenario(Context context) =>
            _context = context;
        #endregion
    }
}
