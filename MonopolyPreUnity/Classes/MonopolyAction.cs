using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class MonopolyAction : IMonopolyAction
    {
        public MonopolyActionType ActionType { get; }

        public MonopolyAction(MonopolyActionType actionType)
        {
            ActionType = actionType;
        }
    }

    class MonopolyAction<T> : IMonopolyAction
    {
        public MonopolyActionType ActionType { get; }
        public T Argument1 { get; }

        public MonopolyAction(MonopolyActionType actionType, T argument1)
        {
            ActionType = actionType;
            Argument1 = argument1;
        }
    }

    class MonopolyAction<T1, T2> : IMonopolyAction
    {
        public MonopolyActionType ActionType { get; }
        public T1 Argument1 { get; }
        public T2 Argument2 { get; }

        public MonopolyAction(MonopolyActionType actionType, T1 argument1, T2 argument2)
        {
            ActionType = actionType;
            Argument1 = argument1;
            Argument2 = argument2;
        }
    }
}
