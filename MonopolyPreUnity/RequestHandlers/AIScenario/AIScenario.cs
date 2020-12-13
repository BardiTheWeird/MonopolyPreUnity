
using Autofac.Features.Indexed;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.RequestHandlers.AIScenario.RequestScenarios;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.AIScenario
{
    class AIScenario : IPlayerScenario
    {
        private readonly IIndex<Type, IAIRequestScenario> _index;
        private readonly Player _player;
        private readonly AiInfo _aiInfo;

        public void HandleRequest(IRequest request) =>
            _index[request.GetType()].RunScenario(request, _player, _aiInfo);

        public AIScenario(IIndex<Type, IAIRequestScenario> index, Player player, AiInfo aiInfo)
        {
            _index = index;
            _player = player;
            _aiInfo = aiInfo;
        }
    }
}
