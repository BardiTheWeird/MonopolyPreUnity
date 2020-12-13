using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.AIScenario.RequestScenarios
{
    interface IAIRequestScenario
    {
        public void RunScenario(IRequest request, Player player, AiInfo aIInfo);
    }
}
