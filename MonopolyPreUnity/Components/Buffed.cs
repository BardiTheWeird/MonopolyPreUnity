using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    abstract class TimeElapsable : IEntityComponent
    {
        public int TicksElapsed { get; set; }
        public int MaxTicks { get; set; }

        protected TimeElapsable(int ticksElapsed, int maxTicks)
        {
            TicksElapsed = ticksElapsed;
            MaxTicks = maxTicks;
        }
    }

    class Buffed : TimeElapsable
    {
        protected Buffed(int ticksElapsed, int maxTicks) : base(ticksElapsed, maxTicks)
        {
        }
    }
}
