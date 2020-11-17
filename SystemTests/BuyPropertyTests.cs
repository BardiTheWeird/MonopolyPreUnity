using Autofac;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput.Property;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Initialization;
using MonopolyPreUnity.Systems.PropertySystems;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SystemTests
{
    public class BuyPropertyTests
    {
        Context context;
        BuyPropertySystem buySystem;

        public BuyPropertyTests()
        {
            context = new Context();
            var c = context.CreateDiContainer();
            buySystem = c.Resolve<BuyPropertySystem>();

            context.Add(new Player(1, "Joshua"));
            context.Add(new Tile(1, 0, "Prop"), new Property(1, 200));
        }

        [Fact]
        public void Test1()
        {
            context.Add(new BuyProperty(1, 1));
        }
    }
}
