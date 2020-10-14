using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Components
{
    interface IMonopolyAction
    {
        public string Description { get; }
        public void Execute(int playerId);
    }

    class ChargeAction : IMonopolyAction
    {
        private readonly PlayerManager _playerManager;

        public int AmountCharged { get; }
        public string Description { get; }

        public void Execute(int playerId)
        {
            _playerManager.PlayerCashCharge(playerId, AmountCharged);
        }

        public ChargeAction(int amountCharged, string description, PlayerManager playerManager)
        {
            AmountCharged = amountCharged;
            Description = description;
            _playerManager = playerManager;
        }
    }

    class GoToJailAction : IMonopolyAction
    {
        private readonly MapManager _mapManager;

        public string Description { get; }

        public void Execute(int playerId)
        {
            // move to jail, duh
            throw new NotImplementedException();
        }

        public GoToJailAction(string description, MapManager mapManager)
        {
            Description = description;
            _mapManager = mapManager;
        }
    }

    class GiftFromPlayersAction : IMonopolyAction
    {
        private readonly PlayerManager _playerManager;

        public string Description { get; }
        public int AmountPerPlayer { get; }

        public void Execute(int playerId)
        {
            foreach(int id in _playerManager.GetAllPlayerId().Where(x => x != playerId))
            {
                _playerManager.PlayerCashCharge(playerId, AmountPerPlayer);
            }
        }

        public GiftFromPlayersAction(int amountPerPlayer, string description, PlayerManager playerManager)
        {
            AmountPerPlayer = amountPerPlayer;
            Description = description;
            _playerManager = playerManager;
        }
    }
}
