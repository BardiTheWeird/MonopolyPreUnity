using Autofac;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Initialization;
using MonopolyPreUnity.Systems;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Xunit;

namespace SystemTests
{
    public class HSInputSystemTests
    {
        Context context;
        IContainer c;
        HSInputSystem inputSystem;

        public HSInputSystemTests()
        {
            context = new Context();
            context.Add(new Player(1, "Joshua"));
            c = context.CreateDiContainer();
            inputSystem = c.Resolve<HSInputSystem>();
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

        [Fact]
        public void ParseMultipleIndicesTest()
        {
            var inputParser = c.Resolve<InputParser>();
            var list = new List<int> { 1, 2, 3 };
            var input = "1";

            var success = inputParser.TryParseIndexMultiple(list, out var outVals, input);

            Assert.True(success);
            Assert.Contains(0, outVals);

            input = "   1  2   sfsaf";

            success = inputParser.TryParseIndexMultiple(list, out outVals, input);

            Assert.True(success);
            Assert.Contains(0, outVals);
            Assert.Contains(1, outVals);

            input = "-1 1 12 3";

            success = inputParser.TryParseIndexMultiple(list, out outVals, input);

            Assert.False(success);

            var linePrinted = context.GetComponent<PrintLine>().OutString;
            Assert.Equal("Problematic values: -1 12", linePrinted);
        }
    }
}
