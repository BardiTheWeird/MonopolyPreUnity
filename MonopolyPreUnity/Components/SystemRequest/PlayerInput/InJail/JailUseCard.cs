using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerInput.InJail
{
    class JailUseCard : IInJailInput
    {
        public int PlayerId { get; set; }

        public JailUseCard(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
