using MonopolyPreUnity.Components;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.PlayerLanded
{
    class JailLandedBehavior : IPlayerLandedBehavior
    {
        #region Dependencies
        private readonly ConsoleUI _consoleUI;
        #endregion

        public void PlayerLanded(int playerId, ITileComponent tileComponent, int tileId)
        {
            _consoleUI.Print("Just visiting");
        }

        public JailLandedBehavior(ConsoleUI consoleUI)
        {
            _consoleUI = consoleUI;
        }
    }
}
