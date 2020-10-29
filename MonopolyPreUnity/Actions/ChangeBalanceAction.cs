using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Actions
{
    class ChangeBalanceAction : IMonopolyAction
    {
        public int Amount { get; set; }

        public ChangeBalanceAction(int amount)
        {
            Amount = amount;
        }
    }
}
