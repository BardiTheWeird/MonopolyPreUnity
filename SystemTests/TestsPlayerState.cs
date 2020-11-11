using Autofac;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.Cash;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Initialization;
using MonopolyPreUnity.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SystemTests
{
    public class TestsPlayerState
    {
        Context context;
        IContainer c;
        PlayerCashSystem cashSystem;

        #region ctor
        public TestsPlayerState()
        {
            context = new Context();
            c = DiContainer.CreateDiContainer(context);

            cashSystem = c.Resolve<PlayerCashSystem>();

            context.Add(new Player(1, ""));

            var p2 = new Player(2, "");
            p2.Cash = 200;
            context.Add(p2);
        }
        #endregion

        #region cash
        [Fact]
        public void TestGive()
        {
            context.Add(new GiveCash(50, 1));
            cashSystem.Execute();

            Assert.Equal(50, context.GetPlayer(1).Cash);
        }

        [Fact]
        public void TestCharge()
        {
            context.Add(new ChargeCash(200, 2));
            cashSystem.Execute();

            Assert.Equal(0, context.GetPlayer(2).Cash);
        }

        [Fact]
        public void TestDebt()
        {
            context.Add(new ChargeCash(50, 1));
            context.Add(new ChargeCash(250, 2));
            cashSystem.Execute();

            Assert.Equal(2, context.GetComponents<PlayerDebt>().Count);
        }

        #endregion
    }
}
