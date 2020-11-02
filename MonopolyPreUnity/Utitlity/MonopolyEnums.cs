﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Utitlity
{
    public enum MonopolyCommand
    {
        TurnMove,
        TurnManageProperty,
        TurnMakeDeal,
        EndTurn,

        PropertyChoose,
        PropertyMortgage,
        PropertyUnmortgage,
        PropertyBuyHouse,
        PropertySellHouse,

        TileLandedPropertyBuy,
        TileLandedPropertyAuction,

        TradeChoosePlayer,
        TradeCreateDeal,
        TradeValidate,

        JailPayMoney,
        JailUseCard,
        JailUseDice,
        StayInJail,

        CancelAction
    }
}
