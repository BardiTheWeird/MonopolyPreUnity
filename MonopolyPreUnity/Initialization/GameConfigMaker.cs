﻿using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Initialization
{
    static class GameConfigMaker
    {
        public static GameConfig DefaultGameConfig() =>
            new GameConfig(@"D:\ImmaCodder\Kursach\MonopolyPreUnity\MonopolyPreUnity\Resources\defaultGameConfig.xml");
    }
}
