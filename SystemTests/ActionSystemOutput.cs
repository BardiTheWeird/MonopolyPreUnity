using Autofac;
using MonopolyPreUnity.Actions;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Initialization;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SystemTests
{
    public class ActionSystemOutput
    {
        Context context;
        IContainer c;
        SystemsBag sysBag;

        public ActionSystemOutput()
        {
            context = new Context();
            c = context.CreateDiContainer();
            sysBag = new SystemsBag(c.GetAllSystems());
        }

        [Fact]
        public void Test1()
        {
            var player = new Player(1, "Joshua");
            player.CurTileId = 1;
            context.Add(player);
            context.Add(new Tile(1, 0, "Tax 100$"), new ActionTile(new ChangeBalanceAction(-100)));
            context.Add(new PlayerLanded(1));

            sysBag.Execute();
            sysBag.Execute();

            //Assert.Equal("", context.OutputString);
        }
    }
}
