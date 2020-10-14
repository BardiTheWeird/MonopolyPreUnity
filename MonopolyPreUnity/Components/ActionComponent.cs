using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class ActionComponent : ITileContentComponent
    {
        public IMonopolyAction Action { get; }

        public void OnPlayerLanded(int playerId)
        {
            Action.Execute(playerId);
        }
    }
}
