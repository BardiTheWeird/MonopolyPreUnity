using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers
{
    interface IPlayerScenario
    {
        public void HandleRequest(IRequest request);
    }
}
