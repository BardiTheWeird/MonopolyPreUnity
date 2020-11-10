using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerInput.InJail
{
    class JailDiceRoll : IInJailInput
    {
        public int PlayerId { get; set; }

        public JailDiceRoll(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
