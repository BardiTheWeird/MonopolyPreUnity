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

        GetPlayerStatus,
        GetGameStatus,

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

        PayJailFine,
        UseJailCard,
        JailRollDice,
        StayInJail,

        CancelAction
    }
}
