﻿using MonopolyPreUnity.Components;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class PropertyComponent : ITileComponent
    {
        public int setId { get; }
        public int BasePrice { get; }
        public int? OwnerId { get; set; } = null;
        public bool IsMortgaged { get; set; } = false;

        public PropertyComponent(int setId, int basePrice)
        {
            this.setId = setId;
            BasePrice = basePrice;
            OwnerId = null;
            IsMortgaged = false;
        }

        public PropertyComponent(int setId, int basePrice, int? ownerId, bool isMortgaged)
        {
            this.setId = setId;
            BasePrice = basePrice;
            OwnerId = ownerId;
            IsMortgaged = isMortgaged;
        }
    }
}
