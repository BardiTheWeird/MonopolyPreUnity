using MonopolyPreUnity.Components.SystemRequest.Cash;
using MonopolyPreUnity.Components.SystemState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class OnGoPassedSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            foreach (var goPassed in _context.GetComponents<GoPassed>())
            {
                var amount = _context.GameConfig().CashPerLap;
                _context.Add(new GiveCash(amount, goPassed.PlayerId, "passed go"));
            }
            _context.Remove<GoPassed>();
        }

        public OnGoPassedSystem(Context context) =>
            _context = context;
    }
}
