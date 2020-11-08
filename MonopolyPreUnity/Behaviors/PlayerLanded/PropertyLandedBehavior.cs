using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors
{
    class PropertyLandedBehavior : IPlayerLandedBehavior
    {
        #region Dependencies
        private readonly RentManager _rentManager;
        private readonly RequestManager _requestManager;
        private readonly ConsoleUI _consoleUI;
        #endregion

        #region Behavior
        public void PlayerLanded(int playerId, ITileComponent tileComponent, int tileId)
        {
            var property = (PropertyComponent)tileComponent;

            if (property.OwnerId == null)
            {
                _requestManager.SendRequest(playerId, new BuyAuctionRequest(tileId));
            }
            else if (property.OwnerId != playerId && property.IsMortgaged == false)
            {
                _rentManager.CollectRent(playerId, tileId, (int)property.OwnerId);
            }
            else
                _consoleUI.Print("It's their own property");
        }

        #endregion

        #region Constructor
        public PropertyLandedBehavior(RentManager rentManager,
            RequestManager requestManager,
            ConsoleUI consoleUI)
        {
            _rentManager = rentManager;
            _requestManager = requestManager;
            _consoleUI = consoleUI;
        }
        #endregion
    }
}
