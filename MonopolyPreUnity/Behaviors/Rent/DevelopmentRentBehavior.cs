using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Rent
{
    class DevelopmentRentBehavior : IRentBehavior
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public int GetRent(int renteeId, int ownerId, IEntityComponent component, int tileId) =>
            _context.GetCurrentRentDev(tileId);

        #region Constructor
        public DevelopmentRentBehavior(Context context) => 
            _context = context;
        #endregion
    }
}
