using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class TrainStationComponent : ITileComponent
    {
        public int BaseRent { get; set; }

        public TrainStationComponent(int baseRent)
        {
            BaseRent = baseRent;
        }
    }
}
