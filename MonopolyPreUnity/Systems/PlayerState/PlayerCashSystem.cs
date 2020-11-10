using MonopolyPreUnity.Components.SystemRequest.Cash;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class PlayerCashSystem : ISystem
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public void Execute()
        {
            foreach (var operation in _context.GetComponentsInterface<ICashOperation>())
            {
                switch (operation)
                {
                    case ChargeCash charge:
                        PlayerCashCharge(charge);
                        break;
                    case GiveCash give:
                        PlayerCashGive(give);
                        break;
                }
            }
            _context.RemoveEntitiesInterface<ICashOperation>();
        }

        #region charge cash
        void PlayerCashCharge(ChargeCash charge)
        {
            var player = _context.GetPlayer(charge.PlayerChargedId);
            if (player.Cash >= charge.Amount)
            {
                player.Cash -= charge.Amount;
                if (charge.PlayerChargerId != null)
                    PlayerCashGive((int)charge.PlayerChargerId, charge.Amount);
            }
            else
                _context.Add(new PlayerDebt(player.Id, charge.Amount, charge.PlayerChargerId));
        }
        #endregion

        #region give cash
        void PlayerCashGive(GiveCash give) =>
            PlayerCashGive(give.PlayerId, give.Amount);

        void PlayerCashGive(int playerId, int amount)
        {
            var player = _context.GetPlayer(playerId);
            player.Cash += amount;
        }
        #endregion

        #region ctor
        public PlayerCashSystem(Context context) =>
            _context = context;
        #endregion
    }
}
