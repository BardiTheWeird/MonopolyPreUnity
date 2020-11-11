using Autofac.Features.Indexed;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HSScenario
{
    class HSScenario : IPlayerScenario
    {
        private readonly IIndex<Type, IHSRequestScenario> _index;
        private readonly Player _player;

        public void HandleRequest(IRequest request) =>
            _index[request.GetType()].RunScenario(request, _player);

        public HSScenario(IIndex<Type, IHSRequestScenario> index, Player player)
        {
            _index = index;
            _player = player;
        }
    }
}
