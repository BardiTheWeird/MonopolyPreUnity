using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class PropertyDevelopmentComponent : IEntityComponent
    {
        public int HousesBuilt { get; set; }
        public int HouseBuyPrice { get; }
        public int HouseSellPrice { get; }
        public int HouseCap { get; }
        public List<int> RentList { get; }

        public PropertyDevelopmentComponent(
            int houseBuyPrice,
            List<int> rentList)
        {
            HousesBuilt = 0;
            HouseBuyPrice = houseBuyPrice;
            HouseSellPrice = HouseBuyPrice / 2;
            HouseCap = 5;
            RentList = rentList;

            if (RentList.Count != 2 + HouseCap)
                throw new ArgumentException("RentList.Count isn't equal to 2 + HouseCap");
        }

        public PropertyDevelopmentComponent(int housesBuilt, 
            int houseBuyPrice, 
            int houseSellPrice, 
            int houseCap, 
            List<int> rentList)
        {
            HousesBuilt = housesBuilt;
            HouseBuyPrice = houseBuyPrice;
            HouseSellPrice = houseSellPrice;
            HouseCap = houseCap;
            RentList = rentList;

            if (RentList.Count != 2 + HouseCap)
                throw new ArgumentException("RentList.Count isn't equal to 2 + HouseCap");
        }
    }
}
