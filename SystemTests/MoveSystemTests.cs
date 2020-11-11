using Autofac;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.Move;
using MonopolyPreUnity.Components.SystemState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Initialization;
using MonopolyPreUnity.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SystemTests
{
    public class MoveSystemTests
    {
        Context context;
        IContainer c;
        MoveSystem moveSystem;

        public MoveSystemTests()
        {
            var mock = new MockContextMaker(GameConfigMaker.DefaultGameConfig());
            context = mock.GetContext();
            c = DiContainer.CreateDiContainer(context);
            moveSystem = c.Resolve<MoveSystem>();

            context.Add(new Player(1, "Joshua"));
        }

        void AddGo()
        {
            context.Add(new Tile(1, 0, "Go"), new Go());
            context.MapInfo().MapSize = 1;
            context.MapInfo().GoId = 1;

            context.GetPlayer(1).CurTileId = 1;
        }

        [Fact]
        public void OnGoPassedTest1()
        {
            AddGo();
            context.Add(new MoveSteps(1, true, 0));
            moveSystem.Execute();

            Assert.False(context.ContainsComponent<GoPassed>());

            context.Add(new MoveSteps(1, true, 1));
            moveSystem.Execute();

            Assert.True(context.ContainsComponent<GoPassed>());
        }
    }
}
