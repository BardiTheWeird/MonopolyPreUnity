using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class AiInfo
    {
        public ChaosFactor ChaosFactor { get; set; }
        public bool DidPropertyActionsThisTurn { get; set; }
        public Dictionary<int, int> TradeCooldowns { get; set; }

        public void Nullify()
        {
            DidPropertyActionsThisTurn = false;

            TradeCooldowns = TradeCooldowns
                .Where(x => x.Value > 1)
                .Select(x => new KeyValuePair<int, int>(x.Key, x.Value - 1))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public AiInfo(ChaosFactor chaosFactor)
        {
            ChaosFactor = chaosFactor;
            TradeCooldowns = new Dictionary<int, int>();
        }
    }
}
