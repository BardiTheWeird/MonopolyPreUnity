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

        public PropertyDevelopmentComponent(int houseBuyPrice)
        {           
            HouseBuyPrice = houseBuyPrice;
            HouseSellPrice = HouseBuyPrice / 2;
            HousesBuilt = 0;
            HouseCap = 5;
        }

        public PropertyDevelopmentComponent(int houseBuyPrice, int houseSellPrice, int houseCap, int housesBuilt)
        {
            HousesBuilt = housesBuilt;
            HouseBuyPrice = houseBuyPrice;
            HouseSellPrice = houseSellPrice;
            HouseCap = houseCap;
        }
    }
}
