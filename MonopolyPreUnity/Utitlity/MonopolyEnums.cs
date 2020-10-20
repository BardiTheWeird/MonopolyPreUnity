using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Utitlity
{
    public enum MonopolyActionType
    {
        ChangeBalance,
        GiftFromPlayers,
        TaxPerHouse,
        GoToJail,
        GoToTile,
        GetAnItem
    }

    public enum MonopolyCommand
    {
        TurnMakeMove,
        TurnManageProperty,
        TurnMakeDeal,
        TurnEndTurn,

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
    
    public enum MonopolyRequest
    {
        TurnCommandChoice,

        PropertyManagePropertyChoice,
        PropertyManageActionChoice,

        MoveJailChoice,

        TileLandedPropertyChoice,
        AuctionBidChoice,

        TradePlayerChoice,
        TradeDealChoice,
        TradeValidationChoice
    }
}
