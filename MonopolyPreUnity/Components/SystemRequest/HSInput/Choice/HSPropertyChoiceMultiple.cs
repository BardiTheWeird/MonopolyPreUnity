using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.HSInput.Choice
{
    class HSPropertyChoiceMultiple : IHSChoice
    {
        public int PlayerId { get; set; }
        public List<int> PropertiesChosen { get; set; }

        public HSPropertyChoiceMultiple(int playerId, List<int> propertiesChosen)
        {
            PlayerId = playerId;
            PropertiesChosen = propertiesChosen;
        }
    }
}
