using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Utitlity
{
    public enum MonopolyCommand
    {
        MakeMove,
        ManageProperty,
        CreateTradeOffer,
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
        ConfirmTrade,
        ChangeReceiverAssets,
        ChangeInitiatorAssets,
        ChangeCashAmount,
        ChangeJailCardsAmount,
        ChangeProperties,
        ValidateTradeOffer,

        PayJailFine,
        UseJailCard,
        JailRollDice,
        StayInJail,

        CancelAction
    }
}
