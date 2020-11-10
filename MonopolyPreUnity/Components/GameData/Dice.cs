using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class Dice : IEntityComponent
    {
        public int Die1 { get; set; }
        public int Die2 { get; set; }
        public int DieSides { get; }

        public int Sum => Die1 + Die2;
        public bool IsPair => Die1 == Die2;

        public void Throw()
        {
            var rand = new Random();
            Die1 = rand.Next(1, DieSides + 1);
            Die2 = rand.Next(1, DieSides + 1);
        }

        public Dice(int dieSides)
        {
            DieSides = dieSides;
        }
    }
}
