using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MonopolyPreUnity.Entity
{
    static class ContextTileExtensions
    {
        #region GetEntity

        #endregion

        #region GetTile
        /// <summary>
        /// Returns a tile with a given id
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tileId"></param>
        /// <returns>Tile</returns>
        public static Tile GetTileId(this Context context, int tileId) =>
            context.GetComponent<Tile>(t => t.Id == tileId);

        /// <summary>
        /// Returns a tile with a given map position
        /// </summary>
        /// <param name="context"></param>
        /// <param name="position"></param>
        /// <returns>Tile</returns>
        public static Tile GetTilePosition(this Context context, int position) =>
            context.GetComponent<Tile>(t => t.MapPosition == position);

        public static List<Tile> GetAllTiles(this Context context) =>
            context.GetComponents<Tile>();
        #endregion

        #region Map
        public static MapInfo MapInfo(this Context context) =>
            context.GetComponent<MapInfo>();

        public static int GetPosition(this Context context, int id) =>
            context.GetTileId(id).MapPosition;

        public static List<Tile> GetMap(this Context context) =>
            context.GetAllTiles().OrderBy(t => t.MapPosition).ToList();
        #endregion

        #region TileComponents
        public static T GetTileComponent<T>(this Context context, int tileId) where T : IEntityComponent =>
            (T)context
            .GetTileComponents(tileId)
            .FirstOrDefault(comp => comp.GetType() == typeof(T));

        public static List<IEntityComponent> GetTileComponents(this Context context, int tileId)
        {
            var entity = context.GetEntity<Tile>(entity => entity.GetComponent<Tile>().Id == tileId);
            if (entity == null)
                return new List<IEntityComponent>();
            return entity.Components;
        }

        public static bool ContainsComponent(this Tile tile, Type type, Context context)
        {
            var entity = context.GetEntity<Tile>(e => e.GetComponent<Tile>() == tile);
            foreach (var comp in entity.Components)
            {
                if (comp.GetType() == type)
                    return true;
            }
            return false;
        }
        #endregion

        #region Property
        public static HashSet<int> GetPropertySet(this Context context, int setId) =>
            context
            .GetEntities<Tile>()
            .GetEntities<Property>()
            .Select(e => e.GetComponent<Property>().SetId)
            .Where(id => id == setId)
            .ToHashSet();

        public static bool IsPropertySetOwned(this Context context, Player player, int setId)
        {
            HashSet<int> set = context.GetPropertySet(setId);
            var intersection = player.Properties.Intersect(set);
            if (set.Count == intersection.Count())
                return true;
            return false;
        }

        public static HashSet<int> OwnedPropertiesInSet(this Context context, Player player, int setId) =>
            player.Properties
            .Intersect(context.GetPropertySet(setId))
            .ToHashSet();
        #endregion
    }
}
