using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.HSInput.Choice
{
    class HSIntChoice : IHSChoice
    {
        public int PlayerId { get; set; }
        public int Choice { get; set; }

        public HSIntChoice(int playerId, int choice)
        {
            PlayerId = playerId;
            Choice = choice;
        }

        public static implicit operator int(HSIntChoice intChoice) => intChoice.Choice;
    }
}
