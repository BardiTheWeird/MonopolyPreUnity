using MonopolyPreUnity.Components.SystemRequest.HSInput;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput
{
    interface IHSStateBehavior
    {
        public void Run(HSInputState state);
    }
}
