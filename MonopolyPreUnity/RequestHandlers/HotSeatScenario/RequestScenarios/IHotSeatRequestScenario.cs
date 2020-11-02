﻿using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HotSeatScenario
{
    interface IHotSeatRequestScenario
    {
        public void RunScenario(IRequest request, Player player);
    }
}
