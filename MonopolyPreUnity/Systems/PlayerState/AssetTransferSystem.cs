using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class AssetTransferSystem : ISystem
    {
        #region dependencies
        private readonly Context _context;
        #endregion

        public void Execute()
        {
            foreach (var transfer in _context.GetComponents<AssetTransferRequest>())
            {
                if (transfer.ReceiverId.HasValue)
                    TransferToPlayer(_context.GetPlayer(transfer.ReceiverId.Value), transfer.Assets);
                else
                    TransferToBank(transfer.Assets);
            }
            _context.Remove<AssetTransferRequest>();
        }

        #region to bank
        void TransferToBank(PlayerAssets assets)
        {
            foreach (var propId in assets.Properties)
            {
                var prop = _context.GetTileComponent<Property>(propId);
                var dev = _context.GetTileComponent<PropertyDevelopment>(propId);

                prop.IsMortgaged = false;
                prop.OwnerId = null;

                if (dev != null)
                    dev.HousesBuilt = 0;
            }
        }
        #endregion

        #region to player
        void TransferToPlayer(Player receiver, PlayerAssets assets)
        {
            var sender = _context.GetPlayer(assets.PlayerId);

            receiver.Cash += assets.Cash;
            sender.Cash -= assets.Cash;

            receiver.JailCards += assets.JailCards;
            sender.JailCards -= assets.JailCards;

            if (assets.Properties != null)
            {
                foreach (var propId in assets.Properties)
                {
                    var prop = _context.GetTileComponent<Property>(propId);
                    prop.OwnerId = receiver.Id;
                    receiver.Properties.Add(propId);
                    sender.Properties.Remove(propId);
                }
            }
        }
        #endregion

        #region ctor
        public AssetTransferSystem(Context context) =>
            _context = context;
        #endregion
    }
}
