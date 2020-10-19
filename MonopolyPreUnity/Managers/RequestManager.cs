using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class RequestManager
    {
        #region Dependencies
        private readonly PlayerManager _playerManager;
        #endregion

        public TInput SendRequest<TInput>(int receiverId, Request<TInput> request)
        {
            var userScenario = _playerManager.GetUserScenario(receiverId);
            return userScenario.HandleRequest(request);
        }

        #region Constructor
        public RequestManager(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
        #endregion
    }
}
