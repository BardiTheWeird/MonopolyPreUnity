using Autofac.Features.Indexed;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.RequestHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class PlayerInputSystem : ISystem
    {
        private readonly Context _context;
        private readonly IIndex<int, IPlayerScenario> _scenarioIndex;

        public void Execute()
        {
            foreach (var request in _context.GetComponents<PlayerInputRequest>())
                _scenarioIndex[request.PlayerId].HandleRequest(request.Request);
            _context.Remove<PlayerInputRequest>();
        }

        #region ctor
        public PlayerInputSystem(Context context, IIndex<int, IPlayerScenario> scenarioIndex)
        {
            _context = context;
            _scenarioIndex = scenarioIndex;
        }
        #endregion
    }
}
