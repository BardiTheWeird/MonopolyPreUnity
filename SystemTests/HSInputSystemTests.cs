using Autofac;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Initialization;
using MonopolyPreUnity.Systems;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SystemTests
{
    public class HSInputSystemTests
    {
        Context context;
        HSInputSystem inputSystem;

        public HSInputSystemTests()
        {
            context = new Context();
            context.Add(new Player(1, "Joshua"));
            inputSystem = DiContainer.CreateDiContainer(context).Resolve<HSInputSystem>();
        }

        [Fact]
        public void Test1()
        {
            var state = context.HSInputState();
            state.Set(HSState.TurnChoice, 1);

            var commandList = new List<MonopolyCommand>()
            {
                MonopolyCommand.ManageProperty,
                MonopolyCommand.CreateTradeOffer,
                MonopolyCommand.MakeMove
            };
            context.Add(new HSCommandChoiceRequest(commandList, 1));
            context.InputString = "3";

            inputSystem.Execute();

            Assert.Equal("", context.InputString);
            Assert.False(context.ContainsComponent<HSCommandChoiceRequest>());
            Assert.False(context.ContainsComponent<HSCommandChoice>());
        }
    }
}
