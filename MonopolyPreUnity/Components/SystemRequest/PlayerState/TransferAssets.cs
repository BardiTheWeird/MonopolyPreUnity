using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.PlayerState
{
    class TransferAssets : IEntityComponent
    {
        public int? ReceiverId { get; set; }
        public PlayerAssets Assets { get; set; }

        public TransferAssets(int? receiverId, PlayerAssets assets)
        {
            ReceiverId = receiverId;
            this.Assets = assets;
        }

        public TransferAssets(int? receiverId, Player player) : this(receiverId, new PlayerAssets(player)) { }
    }
}
