using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Interfaces
{
    interface IUserScenario
    {
        public TInput HandleRequest<TInput>(Request<TInput> request);
    }
}
