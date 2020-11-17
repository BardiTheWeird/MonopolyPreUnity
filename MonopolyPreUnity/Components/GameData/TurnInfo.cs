using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    [Serializable]
    class TurnInfo : IEntityComponent
    {
        public List<int> TurnOrder { get; set; }
        public int CurTurnPlayer { get; set; }
        public int CurTurnPlayerId => TurnOrder[CurTurnPlayer];
        public int PlayersLeft => TurnOrder.Count;

        public TurnInfo(List<int> turnOrder, int curTurnPlayer)
        {
            TurnOrder = turnOrder;
            CurTurnPlayer = curTurnPlayer;
        }

        public TurnInfo()
        {
            TurnOrder = new List<int>();
        }
    }
}
