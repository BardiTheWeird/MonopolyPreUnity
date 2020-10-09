using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class RequestManager
    {
        public enum CommandID
        {
            TurnMakeMove,
            TurnManageProperty,
            TurnMakeDeal,

            PropertyChoose,
            PropertyMortgage,
            PropertyUnmortgage,
            PropertyBuyHouse,
            PropertySellHouse,

            TileLandedPropertyBuy,
            TileLandedPropertyAuction,

            TradeChoosePlayer,
            TradeCreateDeal,
            TradeValidate
        }
    }
}
