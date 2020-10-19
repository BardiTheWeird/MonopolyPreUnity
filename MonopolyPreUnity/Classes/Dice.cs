using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class Dice
    {
        public int Die1 { get; set; }
        public int Die2 { get; set; }

        public int Sum => Die1 + Die2;

        public void Throw()
        {
            var rand = new Random();
            Die1 = rand.Next(1, 7);
            Die2 = rand.Next(1, 7);
        }

        /*
        static public (int, int) Throw()
        {
            System.Random dice = new Random();
            return (dice.Next(1, 7), dice.Next(1, 7));
        }
        */
    }
}
