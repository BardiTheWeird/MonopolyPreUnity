using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class ThrowDiceSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            foreach (var throwRequest in _context.GetComponents<ThrowDice>())
            {
                var dice = _context.Dice();
                dice.Throw();

                _context.Add(new PrintLine($"The dice were thrown. Values: {dice.Die1}, {dice.Die2}"));
                if (dice.IsPair)
                    _context.Add(new PrintLine($"It's a pair!"));
            }
            _context.Remove<ThrowDice>();
        }

        public ThrowDiceSystem(Context context)
        {
            _context = context;
        }
    }
}
