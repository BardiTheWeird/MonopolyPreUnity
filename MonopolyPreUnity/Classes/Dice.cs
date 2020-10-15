using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class Dice
    {
        static public (int, int) Throw()
        {
            System.Random dice = new Random();
            return (dice.Next(1, 7), dice.Next(1, 7));
        }
    }
}
