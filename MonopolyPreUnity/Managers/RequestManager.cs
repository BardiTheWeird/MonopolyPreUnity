using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class RequestManager
    {
        PlayerManager _playerManager;

        public TInput SendRequest<TInput>(int receiverId, Request<TInput> request)
        {
            var userScenario = _playerManager.GetUserScenario(receiverId);
            return userScenario.HandleRequest(request);
        }
    }
}
