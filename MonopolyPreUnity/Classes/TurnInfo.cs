using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    [Serializable]
    class TurnInfo
    {
        public List<int> turnOrder { get; }
        public int curTurnPlayer { get; set; }

        public TurnInfo(List<int> turnOrder, int curTurnPlayer)
        {
            this.turnOrder = turnOrder;
            this.curTurnPlayer = curTurnPlayer;
        }
    }
}
