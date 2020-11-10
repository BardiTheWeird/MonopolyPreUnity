﻿using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class TradeOffer : IEntityComponent
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