using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.Jail
{
    class JailPayFine : IJailRequest
    {
        public int PlayerId { get; set; }

        public JailPayFine(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
