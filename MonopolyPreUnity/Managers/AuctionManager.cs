using MonopolyPreUnity.Classes;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class AuctionManager
    {
        #region Dependencies
        private readonly PropertyManager _propertyManager;
        private readonly PlayerManager _playerManager;
        private readonly ConsoleUI _consoleUI;
        #endregion

        public void StartAuction(int tileId)
        {
            _consoleUI.Print("Auction is not yet implemented, so a random player gets this property for free (;");
            var players = _playerManager.GetAllPlayerId();
            var luckyPlayer = players[new Random().Next(0, players.Count)];
            _consoleUI.PrintFormatted($"|player:{luckyPlayer}| is lucky today");

            _propertyManager.TransferProperty(tileId, luckyPlayer);
        }

        public void Bid(int playerId, int amount)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromAuction(int playerId)
        {
            throw new NotImplementedException();
        }

        #region Constructor
        public AuctionManager(PropertyManager propertyManager, 
            PlayerManager playerManager,
            ConsoleUI consoleUI)
        {
            _propertyManager = propertyManager;
            _playerManager = playerManager;
            _consoleUI = consoleUI;
        }
        #endregion
    }
}
