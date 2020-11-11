using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Initialization;
using System;
using Xunit;

namespace SystemTests
{
    public class Tests1
    {
        [Fact]
        public void TestEmptyContext()
        {
            var context = new Context();
            var c = DiContainer.CreateDiContainer(context);
            var sysArr = SystemArrays.GetAllSystems(c);

            sysArr.Execute();

            Assert.True(context.Entities.Count == 0);
        }
    }
}
