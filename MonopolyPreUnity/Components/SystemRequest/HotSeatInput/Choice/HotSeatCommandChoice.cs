using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.HotSeatInput.Choice
{
    class HotSeatCommandChoice : IHotSeatChoice
    {
        public int PlayerId { get; set; }
        public MonopolyCommand Command { get; set; }

        public HotSeatCommandChoice(MonopolyCommand command, int playerId)
        {
            PlayerId = playerId;
            Command = command;
        }
    }
}
