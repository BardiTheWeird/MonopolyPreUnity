using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Managers;
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
            foreach (var transfer in _context.GetComponents<TransferAssets>())
            {
                if (transfer.ReceiverId != null)
                    TransferToPlayer(_context.GetPlayer((int)transfer.ReceiverId), transfer.Assets);
                else
                    TransferToBank(transfer.Assets);
            }
            _context.RemoveEntities<TransferAssets>();
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
        void TransferToPlayer(Player player, PlayerAssets assets)
        {
            player.Cash += assets.Cash;
            player.JailCards += assets.JailCards;
            
            foreach (var propId in assets.Properties)
            {
                var prop = _context.GetTileComponent<Property>(propId);
                prop.OwnerId = player.Id;
                player.Properties.Add(propId);
            }
        }
        #endregion

        #region ctor
        public AssetTransferSystem(Context context) =>
            _context = context;
        #endregion
    }
}
