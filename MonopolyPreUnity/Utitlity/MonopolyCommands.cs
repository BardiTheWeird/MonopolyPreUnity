﻿using System;
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

        PrintPlayerStatus,
        PrintGameStatus,
        PrintMap,

        ChooseProperty,
        MortgageProperty,
        UnmortgageProperty,
        BuyHouse,
        SellHouse,
        CancelManagagingProperty,

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
