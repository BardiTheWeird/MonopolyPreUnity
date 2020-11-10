using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.HotSeatInput;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HotSeatScenario
{
    class HotSeatBuyAuctionScenario : IHotSeatRequestScenario
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public void RunScenario(IRequest requestIn, Player player)
        {
            var request = requestIn as BuyAuctionRequest;
            var availableActions = _context.GetBuyAuctionCommands(player, request.PropertyId);

            _context.Add(new HotSeatCommandChoiceRequest(availableActions, player.Id));
            _context.Add(new PlayerBusy(player.Id));
        }

        #region ctor
        public HotSeatBuyAuctionScenario(Context context) =>
            _context = context;
        #endregion
    }
}
