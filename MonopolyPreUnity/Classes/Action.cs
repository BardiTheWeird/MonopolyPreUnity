using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class Action : IAction
    {
        public MonopolyAction ActionType { get; }

        public Action(MonopolyAction actionType)
        {
            ActionType = actionType;
        }
    }

    class Action<T> : IAction
    {
        public MonopolyAction ActionType { get; }
        public T Argument1 { get; }

        public Action(MonopolyAction actionType, T argument1)
        {
            ActionType = actionType;
            Argument1 = argument1;
        }
    }

    class Action<T1, T2> : IAction
    {
        public MonopolyAction ActionType { get; }
        public T1 Argument1 { get; }
        public T2 Argument2 { get; }

        public Action(MonopolyAction actionType, T1 argument1, T2 argument2)
        {
            ActionType = actionType;
            Argument1 = argument1;
            Argument2 = argument2;
        }
    }
}
