using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class ChaosFactor
    {
        public int Factor { get; set; }

        public ChaosFactor(int factor) =>
            Factor = factor;

        public static implicit operator int(ChaosFactor factor) => factor.Factor;
    }
}
