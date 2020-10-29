﻿using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class ActionTileComponent : ITileComponent
    {
        public IMonopolyAction Action { get; }

        public ActionTileComponent(IMonopolyAction action)
        {
            Action = action;
        }
    }
}
