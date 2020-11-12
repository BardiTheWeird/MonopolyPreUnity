using Autofac;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Entity;
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
            inputSystem = DiContainer.CreateDiContainer(context).Resolve<HSInputSystem>();
        }

        [Fact]
        public void Test1()
        {
            var commandList = new List<MonopolyCommand>()
            {
                MonopolyCommand.ManageProperty,
                MonopolyCommand.MakeDeal,
                MonopolyCommand.MakeMove
            };
            context.Add(new HSCommandChoiceRequest(commandList, 1));
            context.InputString = "3";

            inputSystem.Execute();

            Assert.Equal("", context.InputString);
            Assert.True(context.ContainsComponent<HSCommandChoice>());
            Assert.False(context.ContainsComponent<HSCommandChoiceRequest>());
            Assert.Equal(MonopolyCommand.MakeMove, context.GetComponent<HSCommandChoice>().Command);
        }
    }
}
