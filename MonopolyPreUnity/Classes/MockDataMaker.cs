using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class MockDataMaker
    {
        public GameData MockData { get; set; }

        #region AddStuffPublic
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cash"></param>
        /// <param name="jailCards"></param>
        /// <param name="turnsInPrison"></param>
        /// <param name="curTileId"></param>
        /// <param name="props"></param>
        /// <returns>playerId</returns>
        public int AddPlayer(string name, int cash = 600, int jailCards = 0, 
            int? turnsInPrison = null, int curTileId = -1, HashSet<int> props = null)
        {
            int id = 1;
            if (MockData.PlayerDict.Keys.Count > 0)
                id = MockData.PlayerDict.Keys.Max() + 1;

            var player = new Player(id, name, props);
            player.Cash = cash;
            player.JailCards = jailCards;
            player.TurnsInJail = turnsInPrison;
            player.CurrentTileId = curTileId;

            MockData.PlayerDict.Add(id, player);
            
            return id;
        }

        public int AddTile(string name, params ITileComponent[] components) =>
            AddTile(name, components.ToList());

        public int AddTile(string name, List<ITileComponent> components)
        {
            var identity = CreateTileIdentity(name);
            components.Add(identity);

            MockData.TileDict.Add(identity.Id, new Tile(components));
            AddToMap(identity.Id);

            if (FindTileComponent<PropertyComponent>(components, out var prop))
            {
                if (MockData.PropertySetDict.ContainsKey(prop.SetId))
                    MockData.PropertySetDict[prop.SetId].Add(identity.Id);
                else
                    MockData.PropertySetDict.Add(prop.SetId, new HashSet<int> { identity.Id });
            }

            return identity.Id;
        }
        #endregion

        #region SetStuff
        public void SetStartTile(int tileId)
        {
            foreach (var player in MockData.PlayerDict.Values)
                player.CurrentTileId = tileId;
        }

        public void SetTurnInfo()
        {
            MockData.TurnInfo.TurnOrder.AddRange(MockData.PlayerDict.Keys);
            MockData.TurnInfo.CurTurnPlayer = 0;

            foreach (var player in MockData.PlayerDict.Values)
                player.CanMove = true;
        }
        #endregion

        #region GetStuff
        public GameData GetGameData()
        {
            SetTurnInfo();
            return MockData;
        }
        #endregion

        #region Utility
        public bool FindTileComponent<T>(List<ITileComponent> components, out T comp) where T : class, ITileComponent
        {
            comp = components.Find(x => x.GetType() == typeof(T)) as T;
            if (comp == null)
                return false;
            return true;
        }

        TileComponent CreateTileIdentity(string name)
        {
            int id = 1;
            if (MockData.TileDict.Keys.Count() > 0)
                id = MockData.TileDict.Keys.Max() + 1;
            return new TileComponent(id, name);
        }

        void AddToMap(int tileId)
        {
            MockData.MapIdSequence.Add(tileId);
            MockData.MapIndex.Add(tileId, MockData.MapIdSequence.Count - 1);
        }
        #endregion

        public MockDataMaker(GameConfig gameConfig)
        {
            MockData = new GameData(gameConfig);
        }
    }
}
