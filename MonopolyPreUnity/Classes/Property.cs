using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    abstract class Property : ITileComponent
    {
        #region Dependencies
        private readonly RequestManager _requestManager;
        private readonly PlayerManager _playerManager;
        #endregion

        #region Properties
        public string Name { get; }
        public string Set { get; }
        public int BasePrice { get; }
        public int? OwnerId { get; set; } = null;
        public bool IsMortgaged { get; set; } = false;
        #endregion

        protected Property(string name, string set, int basePrice, RequestManager requestManager, PlayerManager playerManager)
        {
            Name = name;
            Set = set;
            BasePrice = basePrice;
            _requestManager = requestManager;
            _playerManager = playerManager;
        }

        abstract public void ChargeRent(int playerId);

        public void OnPlayerLanded(int playerId)
        {
            if (OwnerId == null)
            {
                /*var command = MonopolyCommand.TileLandedPropertyAuction;
                if (_playerManager.GetPlayer(playerId).Cash >= BasePrice)
                {
                    var request = new Request(playerId,
                        MonopolyCommand.)
                    command = _requestManager.SendRequest(playerId, );
                }*/
            }
            else if (OwnerId != playerId)
            {
                ChargeRent(playerId);
            }
            else
            {
                // do nothing
                // maybe send a message like "it's your own property, dumbass!"
            }
        }
    }
}
