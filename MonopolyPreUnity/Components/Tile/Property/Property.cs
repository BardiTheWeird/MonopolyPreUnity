﻿using MonopolyPreUnity.Components;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;

namespace MonopolyPreUnity.Components
{
    class Property : IEntityComponent
    {
        public int SetId { get; }
        public int BasePrice { get; }
        public int? OwnerId { get; set; } = null;
        public bool IsMortgaged { get; set; } = false;

        public Property(int setId, int basePrice)
        {
            SetId = setId;
            BasePrice = basePrice;
            OwnerId = null;
            IsMortgaged = false;
        }

        public Property(int setId, int basePrice, int? ownerId, bool isMortgaged)
        {
            SetId = setId;
            BasePrice = basePrice;
            OwnerId = ownerId;
            IsMortgaged = isMortgaged;
        }
    }
}
