using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Interfaces
{
    interface IMonopolyAction
    {
        public MonopolyActionType ActionType { get; }
    }
}
