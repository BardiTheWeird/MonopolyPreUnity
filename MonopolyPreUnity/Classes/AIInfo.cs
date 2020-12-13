using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class AiInfo
    {
        public ChaosFactor ChaosFactor { get; set; }
        public bool DidPropertyActionsThisTurn { get; set; }
        public List<int> TradedWithThisTurn { get; set; }

        public void Nullify()
        {
            DidPropertyActionsThisTurn = false;
            TradedWithThisTurn.Clear();
        }

        public AiInfo(ChaosFactor chaosFactor)
        {
            ChaosFactor = chaosFactor;
            TradedWithThisTurn = new List<int>();
        }
    }
}
