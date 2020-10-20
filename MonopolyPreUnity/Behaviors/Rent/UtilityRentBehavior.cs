using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Behaviors.Rent
{
    class UtilityRentBehavior : IRentBehavior
    {
        #region Dependencies
        private readonly TileManager _tileManager;
        private readonly PropertyManager _propertyManager;
        #endregion

        #region fields
        private readonly Dice _dice;
        #endregion

        public int GetRent(int renteeId, int ownerId, ITileComponent component, int tileId)
        {
            var property = _tileManager.GetTileComponent<PropertyComponent>(tileId);
            var ownedPropertyInSet = _propertyManager.OwnedPropertiesInSet(ownerId, property.setId);

            return _dice.Sum * 5 * (int)Math.Round(Math.Pow(2, ownedPropertyInSet.Count - 1));
        }

        #region Constructor
        public UtilityRentBehavior(TileManager tileManager, PropertyManager propertyManager, GameData gameData)
        {
            _tileManager = tileManager;
            _propertyManager = propertyManager;
            _dice = gameData.DiceValues;
        }
        #endregion
    }
}
