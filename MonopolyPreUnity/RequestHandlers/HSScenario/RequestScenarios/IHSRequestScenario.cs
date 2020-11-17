using MonopolyPreUnity.Components;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HSScenario
{
    interface IHSRequestScenario
    {
        public void RunScenario(IRequest request, Player player);
    }
}
