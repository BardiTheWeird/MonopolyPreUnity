using Autofac;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.Move;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Components.SystemState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Initialization;
using MonopolyPreUnity.RequestHandlers.HSScenario.RequestScenarios.TurnScenario;
using MonopolyPreUnity.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SystemTests
{
    public class TestsGeneral
    {
        Context context;
        IContainer c;
        ISystem[] systems;
        SystemsBag sysBag;

        public TestsGeneral()
        {
            context = MockContext.CreateMockDataSmallTest(GameConfigMaker.DefaultGameConfig());
            c = DiContainer.CreateDiContainer(context);
            systems = SystemArrays.GetAllSystems(c);
            sysBag = new SystemsBag(systems);
        }

        #region theoretical tries
        [Fact]
        public void Test1()
        {
            systems.Execute();

            Assert.True(context.ContainsComponent<PlayerInputRequest>());
            Assert.False(context.ContainsComponent<HSCommandChoiceRequest>());

            systems.Execute();

            Assert.False(context.ContainsComponent<PlayerInputRequest>());
            Assert.True(context.ContainsComponent<HSCommandChoiceRequest>());
        }

        [Fact]
        public void TestInputSystem1()
        {
            Test1();
            context.InputString = "qwrqwr";
            sysBag.Execute();

            Assert.False(context.ContainsComponent<PlayerInputRequest>());
            Assert.Equal("", context.InputString);
            Assert.True(context.ContainsComponent<HSCommandChoiceRequest>());

            context.InputString = "3";
            sysBag.Execute(); 
            sysBag.Execute();

            Assert.False(context.ContainsComponentInterface<IMoveRequest>());
            Assert.False(context.ContainsComponent<ThrowDice>());

            Assert.True(context.ContainsComponent<HSCommandChoiceRequest>());
            Assert.False(context.ContainsComponent<PlayerLanded>());
        }

        [Fact]
        public void TestMoveSystem1()
        {
            TestInputSystem1();
            var newPlayerPos = (1 + context.Dice().Sum) % context.MapInfo().MapSize;

            Assert.Equal(newPlayerPos, context.GetPlayer(1).CurTileId);
        }

        [Fact]
        public void TestInputSystem2()
        {
            Test1();
            sysBag.TurnOff<ThrowDiceSystem>();
            sysBag.TurnOff<PlayerLandedSystem>();
            context.Dice().Die1 = 1;
            context.Dice().Die2 = 1;

            context.InputString = "3";

            sysBag.Execute();
            Assert.True(!context.ContainsComponentInterface<IHSRequest>());

            sysBag.Execute();

            Assert.True(context.ContainsComponent<PlayerLanded>());
        }
        #endregion

        #region bug recreation
        [Fact]
        public void ActionFloodBug()
        {
            sysBag.TurnOff<ThrowDiceSystem>();
            var dice = context.Dice();
            dice.Die1 = 5; dice.Die2 = 6;

            // starts off the game
            sysBag.Execute();

            context.InputString = "3";

            sysBag.Execute();

            sysBag.Execute();

            sysBag.Execute();

            Assert.True(true);
        }
        #endregion
    }
}
