using Autofac.Features.Indexed;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HotSeatScenario
{
    class HotSeatScenario : IPlayerScenario
    {
        private readonly IIndex<Type, IHotSeatRequestScenario> _index;
        private readonly Player _player;

        public void HandleRequest(IRequest request) =>
            _index[request.GetType()].RunScenario(request, _player);

        public HotSeatScenario(IIndex<Type, IHotSeatRequestScenario> index, Player player)
        {
            _index = index;
            _player = player;
        }
    }
}
