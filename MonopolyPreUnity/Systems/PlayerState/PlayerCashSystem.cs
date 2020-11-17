using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.Cash;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            foreach (var give in _context.GetComponents<GiveCash>())
                PlayerCashGive(give);

            foreach (var charge in _context.GetComponents<ChargeCash>())
                PlayerCashCharge(charge);

            _context.RemoveInterface<ICashOperation>();
        }

        #region charge cash
        void PlayerCashCharge(ChargeCash charge)
        {
            var player = _context.GetPlayer(charge.PlayerChargedId);
            if (player.Cash >= charge.Amount)
            {
                player.Cash -= charge.Amount;
                _context.Add(new PrintCashCharge(charge));

                if (charge.PlayerChargerId != null)
                    PlayerCashGive((int)charge.PlayerChargerId, charge.Amount);
            }
            else
            {
                Debug.WriteLine($"{player.DisplayName} doesn't have enough money to pay, so now he's got a debt");
                _context.Add(new PrintLine($"{player.DisplayName} doesn't have enough money to pay, so now he's got a debt",
                    OutputStream.GameLog));

                if (!EnoughPropertyToPayOff(player, charge.Amount))
                {
                    Debug.WriteLine($"{player.DisplayName} is bankrupt");
                    _context.Add(new PrintLine($"{player.DisplayName} is bankrupt", OutputStream.GameLog));

                    _context.Add(new PlayerBankrupt(player.Id, charge.PlayerChargerId));
                }
                else
                {
                    Debug.WriteLine($"{player.DisplayName} has enough property to pay off so now he has to do just that");
                    _context.Add(new PrintLine($"{player.DisplayName} has enough property to pay off so now he has to do just that",
                        OutputStream.GameLog));

                    _context.Add(new PlayerDebt(player.Id, charge.Amount, charge.PlayerChargerId));
                }
            }
        }
        #endregion

        #region give cash
        void PlayerCashGive(GiveCash give) =>
            PlayerCashGive(give.PlayerId, give.Amount, give.Message);

        void PlayerCashGive(int playerId, int amount, string message = "")
        {
            var player = _context.GetPlayer(playerId);
            player.Cash += amount;

            _context.Add(new PrintCashGive(playerId, amount, message));
        }
        #endregion

        #region enough property to pay off
        bool EnoughPropertyToPayOff(Player player, int debtAmount)
        {
            var playerProperty = player.Properties;
            int sum = 0;
            foreach (int propId in playerProperty)
            {
                var prop = _context.GetTileComponent<Property>(propId);
                var dev = _context.GetTileComponent<PropertyDevelopment>(propId);

                if (!prop.IsMortgaged)
                    sum += (int)(prop.BasePrice * _context.GameConfig().MortgageFee);

                if (dev != null)
                    sum += dev.HousesBuilt * dev.HouseSellPrice;
            }
            if (sum + player.Cash > debtAmount)
                return true;
            return false;
        }
        #endregion

        #region ctor
        public PlayerCashSystem(Context context) =>
            _context = context;
        #endregion
    }
}
