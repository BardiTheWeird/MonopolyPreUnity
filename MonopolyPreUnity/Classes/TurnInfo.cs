using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    [Serializable]
    class TurnInfo
    {
        public List<int> TurnOrder { get; set; }
        public int CurTurnPlayer { get; set; }
        public int CurTurnPlayerId => TurnOrder[CurTurnPlayer];

        public TurnInfo(List<int> turnOrder, int curTurnPlayer)
        {
            this.TurnOrder = turnOrder;
            this.CurTurnPlayer = curTurnPlayer;
        }

        public TurnInfo()
        {
            TurnOrder = new List<int>();
        }
    }
}
