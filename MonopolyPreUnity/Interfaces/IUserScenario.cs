using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Interfaces
{
    interface IUserScenario
    {
        public TExpectedInput HandleRequest<TChoices, TExpectedInput>(Request<TChoices, TExpectedInput> request);
    }
}
