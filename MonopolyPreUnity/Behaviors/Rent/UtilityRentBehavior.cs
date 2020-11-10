using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Rent
{
    class UtilityRentBehavior : IRentBehavior
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public int GetRent(int renteeId, int ownerId, IEntityComponent component, int tileId)
        {
            var property = _context.GetTileComponent<Property>(tileId);
            var owner = _context.GetPlayer(ownerId);
            var dice = _context.Dice();

            var ownedPropertyInSet = _context.OwnedPropertiesInSet(owner, property.SetId);

            return dice.Sum * 5 * (int)Math.Round(Math.Pow(2, ownedPropertyInSet.Count - 1));
        }

        #region Constructor
        public UtilityRentBehavior(Context context) =>
            _context = context;
        #endregion
    }
}
