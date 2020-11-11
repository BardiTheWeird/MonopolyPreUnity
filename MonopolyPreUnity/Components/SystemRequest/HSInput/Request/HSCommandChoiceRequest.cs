using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.HSInput
{
    class HSCommandChoiceRequest : IHSRequest
    {
        public int PlayerId { get; set; }
        public List<MonopolyCommand> Commands { get; set; }

        public HSCommandChoiceRequest(List<MonopolyCommand> commands, int playerId)
        {
            PlayerId = playerId;
            Commands = commands;
        }
    }
}
