using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class TradeOffer
    {
        public PlayerAssets InitiatorAssets { get; set; }
        public PlayerAssets ReceiverAssets { get; set; }

        public TradeOffer(PlayerAssets initiatorAssets, PlayerAssets receiverAssets)
        {
            InitiatorAssets = initiatorAssets;
            ReceiverAssets = receiverAssets;
        }

        public TradeOffer()
        {
        }
    }
}
