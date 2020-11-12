﻿using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class PlayerDebtSystem : ISystem
    {
        #region dependencies
        private readonly Context _context;
        #endregion

        public void Execute()
        {
            foreach (var debt in _context.GetComponents<PlayerDebt>())
            {
                var debtor = _context.GetPlayer(debt.DebtorId);
                if (!EnoughPropertyToPayOff(debtor, debt.DebtAmount))
                    _context.Add(new PlayerBankrupt(debt));
                else
                    _context.Add(new PlayerHasToPayOffDebt(debt));
            }
            _context.Remove<PlayerDebt>();
        }

        #region Methods
        bool EnoughPropertyToPayOff(Player player, int debtAmount)
        {
            var playerProperty = player.Properties;
            int sum = 0;
            foreach (int propId in playerProperty)
            {
                var prop = _context.GetTileComponent<Property>(propId);
                var dev = _context.GetTileComponent<PropertyDevelopment>(propId);

                var mortgageFee = _context.GameConfig().MortgageFee;

                if (!prop.IsMortgaged)
                    sum += (int)(prop.BasePrice * mortgageFee);

                if (dev != null)
                    sum += dev.HousesBuilt * dev.HouseSellPrice; 
            }
            if (sum + player.Cash > debtAmount)
                return true;
            return false;
        }
        #endregion

        #region ctor
        public PlayerDebtSystem(Context context) =>
            _context = context;
        #endregion
    }
}
