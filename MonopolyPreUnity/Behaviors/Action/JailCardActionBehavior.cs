using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.UI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Action
{
    class JailCardActionBehavior : IActionBehavior
    {
        #region Dependencies
        private readonly PlayerManager _playerManager;
        private readonly ConsoleUI _consoleUI;
        #endregion

        public void Execute(int playerId, IMonopolyAction action)
        {
            var player = _playerManager.GetPlayer(playerId);
            player.JailCards++;
            _consoleUI.Print($"The number of jail cards: {player.JailCards}");
        }

        public JailCardActionBehavior(PlayerManager playerManager, ConsoleUI consoleUI)
        {
            _playerManager = playerManager;
            _consoleUI = consoleUI;
        }
    }
}
