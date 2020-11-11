using MonopolyPreUnity.Components;
using MonopolyPreUnity.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class MockContextMaker
    {
        Context Context { get; set; }
        GameConfig Config { get; set; }
        int MapSize { get; set; }

        #region add stuff
        public void AddPlayer(string name, int cash = 600, int jailCards = 0, 
            int? turnsInPrison = null, int curTileId = -1, HashSet<int> props = null)
        {
            var id = (MaxId<Player>() ?? 0) + 1;

            var player = new Player(id, name, props);
            player.Cash = cash;
            player.JailCards = jailCards;
            player.TurnsInJail = turnsInPrison;
            player.CurTileId = curTileId;

            Context.Add(player);
        }

        public void AddTile(string name, params IEntityComponent[] components)
        {
            var id = (MaxId<Tile>() ?? 0) + 1;
            var entity = new Entity.Entity(new Tile(id, MapSize, name));

            foreach (var comp in components)
                entity.AddComponent(comp);

            Context.Add(entity);
            MapSize++;
        }
        #endregion

        #region SetStuff
        public void SetStartTile(int tileId)
        {
            foreach (var player in Context.GetComponents<Player>())
                player.CurTileId = tileId;
        }
        #endregion

        #region create gameData components
        TurnInfo CreateTurnInfo()
        {
            var players = Context.GetComponents<Player>();
            var turnOrder = players.Select(p => p.Id).ToList();
            foreach (var player in players)
                player.CanMove = true;

            return new TurnInfo(turnOrder, 0);
        }

        MapInfo CreateMapInfo()
        {
            var mapInfo = new MapInfo();
            var goId = ComponentTileId<Go>();
            var jailId = ComponentTileId<Jail>();

            return new MapInfo(goId, jailId, MapSize);
        }

        Dice CreateDice() =>
            new Dice(Config.DieSides);
        #endregion

        #region get context
        public Context GetContext()
        {
            var mapInfo = CreateMapInfo();
            SetStartTile(mapInfo.GoId ?? 1);

            Context.AddEntities(CreateTurnInfo(), mapInfo, CreateDice());
            return Context;
        }
        #endregion

        #region Utility
        int? ComponentTileId<T>() where T : IEntityComponent
        {
            var go = Context.GetEntity<T>();
            return go == null ? null : (int?)go.GetComponent<Tile>().Id;
        }

        int? MaxId<T>() where T : IIdentifiable
        {
            var comps = Context.GetComponents<T>();
            if (comps.Count() == 0)
                return null;

            return comps.Max(c => c.Id);
        }
        #endregion

        #region ctor
        public MockContextMaker(GameConfig config)
        {
            Context = new Context();
            Config = config;
            Context.Add(Config);
        }
        #endregion
    }
}
