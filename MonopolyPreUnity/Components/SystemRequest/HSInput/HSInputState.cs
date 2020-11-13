using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.HSInput
{
    enum HSState
    {
        TurnChoice,
        BuyAuctionChoice,

        PropManageChooseProperty,
        PropManageChooseAction,
    }

    class HSInputState : IEntityComponent
    {
        public HSState? CurState { get; set; }
        public int? PlayerId { get; set; }
        public bool IsNull => CurState == null;

        public void Nullify()
        {
            CurState = null;
            PlayerId = null;
        }

        public void Set(HSState state, int playerId)
        {
            CurState = state;
            PlayerId = playerId;
        }

        public HSInputState(HSState? curState, int curPlayerId)
        {
            CurState = curState;
            PlayerId = curPlayerId;
        }

        public HSInputState()
        {
        }
    }
}
