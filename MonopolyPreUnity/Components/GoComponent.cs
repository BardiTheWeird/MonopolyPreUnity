using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class GoComponent : ITileComponent
    {
        public int MoneyRewarded { get; }

        public GoComponent(int moneyRewarded)
        {
            MoneyRewarded = moneyRewarded;
        }
    }
}
