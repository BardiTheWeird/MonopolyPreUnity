using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class PropertyDevelopmentComponent : ITileComponent
    {
        public int HousesBuilt { get; set; }
        public int HouseBuyPrice { get; }
        public int HouseSellPrice { get; }
        public int HouseCap { get; }

        public PropertyDevelopmentComponent(int housesBuilt, int houseBuyPrice, int houseSellPrice, int houseCap)
        {
            HousesBuilt = housesBuilt;
            HouseBuyPrice = houseBuyPrice;
            HouseSellPrice = houseSellPrice;
            HouseCap = houseCap;
        }
    }
}
