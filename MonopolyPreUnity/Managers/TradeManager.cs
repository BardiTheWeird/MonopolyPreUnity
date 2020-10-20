using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{

    class NotEnoughMoney: Exception
    {
        public override string ToString()
            => "Not enough money.Sorry=)";
    }
    class TradeManager
    {
        #region Variables
        private readonly int _initiatorPlayerId;
        private readonly int _recepientPlayerId;
        private LinkedList <int> _initiatorOffer;
        private LinkedList <int> _recepientOffer;
        private int _initiatorAmount = 0;
        private int _recepientAmount = 0;
        #endregion

        private readonly PlayerManager _playerManager;

        #region Constructor
        public TradeManager(int initiatorPlayerId, int recepientPlayerId,PlayerManager playerManager)
        {
            _initiatorPlayerId = initiatorPlayerId;
            _recepientPlayerId = recepientPlayerId;
            _playerManager = playerManager;
        }
        #endregion

        public void AddPropertyForInitiator(TileIdentityComponent tileIdentityComponent)=>
            _initiatorOffer.AddLast(tileIdentityComponent.Id);

        public void AddPropertyForRecepient(TileIdentityComponent tileIdentityComponent) =>
            _recepientOffer.AddLast(tileIdentityComponent.Id);

        public void RemovePropertyForInitiator(TileIdentityComponent tileIdentityComponent) =>
            _initiatorOffer.Remove(tileIdentityComponent.Id);

        public void RemovePropertyForRecepient(TileIdentityComponent tileIdentityComponent) =>
            _initiatorOffer.Remove(tileIdentityComponent.Id);

        public void SetInitiatorMoney(int amount)
        {
            if (_playerManager.GetPlayerCash(_initiatorPlayerId) > amount)
                _initiatorAmount = amount;
            else throw new NotEnoughMoney();
        }

        public void AddInitiatorMoney(int amount)
        {
            if (_playerManager.GetPlayerCash(_initiatorPlayerId) > amount+_initiatorAmount)
                _initiatorAmount += amount;
            else throw new NotEnoughMoney();
        }

        public void SetRecepientMoney(int amount)
        {
            if (_playerManager.GetPlayerCash(_recepientPlayerId) > amount)
                _recepientAmount = amount;
            else throw new NotEnoughMoney();
        }
            
        public void AddRecepientMoney(int amount)
        {
            if (_playerManager.GetPlayerCash(_recepientPlayerId) > amount + _recepientAmount)
                _recepientAmount += amount;
            else throw new NotEnoughMoney();
        }


        public void ExchangeItems()
        {
            _playerManager.PlayerCashCharge(_initiatorPlayerId, _initiatorAmount);
            _playerManager.PlayerCashGive(_recepientPlayerId, _initiatorAmount);
            _playerManager.PlayerCashCharge(_recepientPlayerId, _initiatorAmount);
            _playerManager.PlayerCashGive(_initiatorPlayerId, _initiatorAmount);


        }


    }
}
