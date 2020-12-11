
using Autofac.Features.Indexed;
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
        private readonly ChaosFactor _chaosFactor;

        public void HandleRequest(IRequest request) =>
            _index[request.GetType()].RunScenario(request, _player, _chaosFactor);

        public AIScenario(IIndex<Type, IAIRequestScenario> index, Player player, ChaosFactor chaosFactor)
        {
            _index = index;
            _player = player;
            _chaosFactor = chaosFactor;
        }
    }
}
