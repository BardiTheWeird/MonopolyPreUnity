using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    

    
    class RealEstate : Property
    {
        public const int _upgradePrice = 50;
        public int UpgradePrice { get { return _upgradePrice; } }
        public bool BuildAllowed { get; set; }
        public int NumberOfHouses { get; set; }

        #region Constructor
        public RealEstate(string name, string set, int basePrice,
            RequestManager requestManager,
            PlayerManager playerManager,
            PropertyTransferManager propertyTransferManager,
            AuctionManager auctionManager) : base(name,set, basePrice,
            requestManager,
            playerManager,
            propertyTransferManager,
            auctionManager)
        {

        }
        #endregion



        
        public override void ChargeRent(int playerId)
        {
            throw new NotImplementedException();
        }
    }
}
