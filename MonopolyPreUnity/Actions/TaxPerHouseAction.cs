using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Actions
{
    class TaxPerHouseAction : IMonopolyAction
    {
        public int Amount { get; set; }

        public string Descsription { get; set; }

        public TaxPerHouseAction(int amount) => Amount = amount;

        public TaxPerHouseAction(int amount, string description)
        {
            Amount = amount;
            Descsription = description;
        }
    }
}
