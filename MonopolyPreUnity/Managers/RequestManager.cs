using Autofac.Features.Indexed;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.RequestHandlers;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class RequestManager
    {
        #region fields
        private readonly IIndex<int, IPlayerScenario> _scenarioIndex;
        #endregion

        public void SendRequest(int receiverId, IRequest request) =>
            _scenarioIndex[receiverId].HandleRequest(request);

        #region Constructor
        public RequestManager(IIndex<int, IPlayerScenario> scenarioIndex)
        {
            _scenarioIndex = scenarioIndex;
        }
        #endregion
    }
}
