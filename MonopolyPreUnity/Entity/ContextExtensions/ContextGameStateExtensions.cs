using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.Trade;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MonopolyPreUnity.Entity.ContextExtensions
{
    static class ContextGameStateExtensions
    {
        public static TurnInfo TurnInfo(this Context context) =>
            context.GetComponent<TurnInfo>();

        public static Dice Dice(this Context context) =>
            context.GetComponent<Dice>();

        public static GameConfig GameConfig(this Context context) =>
            context.GetComponent<GameConfig>();

        public static AuctionInfo AuctionInfo(this Context context) =>
            context.GetComponent<AuctionInfo>();

        public static TradeOffer TradeOffer(this Context context) =>
            context.GetComponent<TradeOffer>();
    }
}
