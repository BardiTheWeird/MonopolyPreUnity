using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerInput.InJail
{
    class JailPayFine : IInJailInput
    {
        public int PlayerId { get; set; }

        public JailPayFine(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
