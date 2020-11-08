using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.PlayerLanded
{
    class FreeParkingBehavior : IPlayerLandedBehavior
    {
        #region Dependencies
        private readonly ConsoleUI _consoleUI;
        #endregion

        public void PlayerLanded(int playerId, ITileComponent tileComponent, int tileId)
        {
            _consoleUI.Print("It's free parking, so you don't get to do nothing");
        }

        public FreeParkingBehavior(ConsoleUI consoleUI)
        {
            _consoleUI = consoleUI;
        }
    }
}
