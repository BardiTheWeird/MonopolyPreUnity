using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems.PropertySystems
{
    class PropertyTransferSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            foreach (var transfer in _context.GetComponents<PropertyTransferRequest>())
            {
                var newOwner = _context.GetPlayer(transfer.NewOwnerId);
                var prop = _context.GetTileComponent<Property>(transfer.PropertyId);

                if (prop.OwnerId.HasValue)
                    _context.GetPlayer(prop.OwnerId.Value).Properties.Remove(transfer.PropertyId);

                newOwner.Properties.Add(transfer.PropertyId);
                prop.OwnerId = newOwner.Id;

                _context.Add(new PrintFormattedLine($"|player:{newOwner.Id}| is the new owner of |tile:{transfer.PropertyId}|", 
                    OutputStream.GameLog));
            }
            _context.Remove<PropertyTransferRequest>();
        }

        #region ctor
        public PropertyTransferSystem(Context context)
            => _context = context;
        #endregion
    }
}
