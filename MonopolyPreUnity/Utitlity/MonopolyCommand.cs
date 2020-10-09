using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Utitlity
{
    public enum MonopolyCommand
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
