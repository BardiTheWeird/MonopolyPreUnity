using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.HotSeatInput
{
    class HotSeatCommandChoiceRequest : IHotSeatRequest
    {
        public int PlayerId { get; set; }
        public List<MonopolyCommand> Commands { get; set; }

        public HotSeatCommandChoiceRequest(List<MonopolyCommand> commands, int playerId)
        {
            PlayerId = playerId;
            Commands = commands;
        }
    }
}
