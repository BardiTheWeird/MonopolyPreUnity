using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class PlayerLandedManager
    {
        #region Property
        public void OnPlayerLanded(int playerId)
        {
            if (OwnerId == null)
            {
                var command = MonopolyCommand.TileLandedPropertyAuction;
                if (_playerManager.GetPlayerCash(playerId) >= BasePrice)
                {
                    var request = new Request<MonopolyCommand>(
                        MonopolyRequest.TileLandedPropertyChoice,
                        new List<MonopolyCommand>()
                        {
                            MonopolyCommand.TileLandedPropertyBuy,
                            MonopolyCommand.TileLandedPropertyAuction
                        });
                    command = _requestManager.SendRequest(playerId, request);
                }

                if (command == MonopolyCommand.TileLandedPropertyBuy)
                {
                    _playerManager.PlayerCashCharge(playerId, BasePrice);
                    _propertyTransferManager.TransferProperty(this, playerId);
                }
            }
            else if (OwnerId != playerId)
            {
                var rent = RentComponent.GetRent();
                _playerManager.PlayerCashCharge(playerId, rent);
            }
            else
            {
                // do nothing
                // maybe send a message like "it's your own property, dumbass!"
            }
        }
        #endregion
    }
}
