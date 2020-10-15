using MonopolyPreUnity.Components;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class ActionManager
    {
        #region Dependencies
        private readonly PlayerManager _playerManager;
        #endregion

        Dictionary<MonopolyAction, Action<int, IAction>> actionDictionary;

        void ChangeBalanceAction(int playerId, IAction action)
        {

        }

        public void ExecuteAction(int playerId, IAction action)
        {

        }
    }
}
