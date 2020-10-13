using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace MonopolyPreUnity.Managers
{

    class PropertyManager
    {
        private readonly PlayerManager _playerManager;
        private readonly TileManager _tileManager;

        public PropertyManager()
        {
            throw new NotImplementedException();
        }
        public void GetAvailableActions()
        {
            throw new NotImplementedException();

        }

        public void BuildHouse(int playerId,RealEstate realEstate)
        {
            if (_playerManager.GetPlayerCash(playerId) > realEstate.UpgradePrice &&
                realEstate.BuildAllowed
           
                )
            {
                realEstate.NumberOfHouses++;
                _playerManager.PlayerCashCharge(playerId, realEstate.UpgradePrice);
            }
        }
        
        public void SellHouse()
        {


            throw new NotImplementedException();

        }
        public void Mortage()
        {

            throw new NotImplementedException();

        }
        
        public void UnMortage()
        {


            throw new NotImplementedException();
        }


        }
}
