using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Utitlity
{
    public enum MonopolyCommand
    {
        MakeMove,
        ManageProperty,
        MakeDeal,
        EndTurn,
        FileBankruptcy,
        EndGame,

        ChooseProperty,
        MortgageProperty,
        UnmortgageProperty,
        BuyHouse,
        SellHouse,

        BuyProperty,
        AuctionProperty,

        ChoosePlayer,
        CreateTradeOffer,
        ValidateTradeOffer,

        JailPayMoney,
        JailUseCard,
        JailUseDice,
        StayInJail,

        CancelAction
    }
}
