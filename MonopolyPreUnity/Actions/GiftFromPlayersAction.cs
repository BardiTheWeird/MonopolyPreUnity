using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Actions
{
    class GiftFromPlayersAction : IMonopolyAction
    {
        public int Amount { get; set; }

        public string Descsription { get; set; }

        public GiftFromPlayersAction(int amount, string description)
        {
            Amount = amount;
            Descsription = description;

        }
    }
}
