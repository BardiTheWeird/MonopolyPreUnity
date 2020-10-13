using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace MonopolyPreUnity.Managers
{
     
    class NotAllowedExeption : Exception
    {
        public override string Message => "Ну нельзя чел че то тебе не хватает))";
        public override string ToString()
        {
            return "ЧЕ НЕ ЯСНО НЕЛЬЗЯ";
        }

    }

    class PropertyManager
    {
        private const float _mortageComission = 0.1f;
        private const float _mortageFee = 0.5f;
        private const int _housesLimit = 5;



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
            if (_playerManager.GetPlayerCash(playerId) > realEstate.HousePrice &&
                realEstate.BuildAllowed
                //тут должно быть еще одно условие для проверки сэта но я не ебу как его реализовать пока что
           
                )
            {
                realEstate.NumberOfHouses++;
                _playerManager.PlayerCashCharge(playerId, realEstate.HousePrice);
                if (realEstate.NumberOfHouses == _housesLimit) realEstate.BuildAllowed = false;

            }
            else throw new NotAllowedExeption();
        }
        
        public void SellHouse(int playerId,RealEstate realEstate)
        {
            if (realEstate.NumberOfHouses > 0
                //и тут условие для сета сука как это сделать хотя мб не надо
                )
            {
                _playerManager.PlayerCashGive(playerId, realEstate.HousePrice);
                realEstate.NumberOfHouses--;
                if (realEstate.NumberOfHouses < _housesLimit) realEstate.BuildAllowed = true;

            }
            else throw new NotAllowedExeption();


        }
        public void Mortage(int playerId, RealEstate realEstate)
        {
            if (realEstate.NumberOfHouses == 0)
            {
                _playerManager.PlayerCashGive(playerId, (int)(_mortageFee * realEstate.BasePrice));
                realEstate.IsMortgaged = true;
            }
            else throw new NotAllowedExeption();

        }
        
        public void UnMortage(int playerId,RealEstate realEstate)
        {
            if (_playerManager.GetPlayerCash(playerId) > (int)(_mortageFee+_mortageComission)*realEstate.BasePrice)
            {
                realEstate.IsMortgaged = false;
                _playerManager.PlayerCashCharge(playerId, (int)(_mortageFee + _mortageComission) * realEstate.BasePrice);
            }
            else throw new NotAllowedExeption();


        }


            }
}
