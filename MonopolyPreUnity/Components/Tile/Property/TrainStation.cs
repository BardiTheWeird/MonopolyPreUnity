using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class TrainStation : IEntityComponent
    {
        public int BaseRent { get; set; }

        public TrainStation(int baseRent)
        {
            BaseRent = baseRent;
        }
    }
}
