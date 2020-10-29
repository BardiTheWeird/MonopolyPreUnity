using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Actions
{
    class GiftFromPlayersAction : IMonopolyAction
    {
        public int Amount { get; set; }

        public GiftFromPlayersAction(int amount)
        {
            Amount = amount;
        }
    }
}
