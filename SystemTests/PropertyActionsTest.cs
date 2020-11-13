using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Initialization;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SystemTests
{
    public class PropertyActionsTest
    {
        private readonly Context context;
        private readonly SystemsBag sysBag;
        private readonly Player player;

        #region ctor
        public PropertyActionsTest()
        {
            context = MockContext.CreateMockDataSmallTest();
            context.RemovePlayer(2);
            var c = context.CreateDiContainer();
            sysBag = new SystemsBag(c.GetAllSystems());
            sysBag.TurnOff<TurnRequestSystem>();

            player = context.GetPlayer(1);
        }
        #endregion

        [Fact]
        public void PropertyOwnershipTest()
        {
            context.Add(new PropertyTransferRequest(3, 1));
            context.Add(new PropertyTransferRequest(4, 1));

            sysBag.Execute();

            Assert.Contains(3, player.Properties);
            Assert.Contains(4, player.Properties);
        }

        [Fact]
        public void GetFullSetTest()
        {
            var set = context.GetPropertySet(1);

            Assert.Equal(2, set.Count);
            Assert.Contains(3, set);
            Assert.Contains(4, set);
        }

        [Fact]
        public void SetOwnedTest()
        {
            PropertyOwnershipTest();
            Assert.True(context.FullSetOwned(player, 1));
        }

        [Fact]
        public void CanDoHouseOperationsTest()
        {
            PropertyOwnershipTest();
            Assert.True(context.CanDoHouseOperations(player, 3));
            Assert.True(context.CanDoHouseOperations(player, 4));
        }

        [Fact]
        public void AvailableActionsTest()
        {
            PropertyOwnershipTest();

            Assert.True(context.CanMortgage(player, 3));
            Assert.False(context.CanUnmortgage(player, 3));
            Assert.True(context.CanBuildHouse(player, 3));
            Assert.False(context.CanSellHouse(player, 3));

            Assert.True(context.CanMortgage(player, 4));
            Assert.False(context.CanUnmortgage(player, 4));
            Assert.True(context.CanBuildHouse(player, 4));
            Assert.False(context.CanSellHouse(player, 4));
        }

        [Fact]
        public void CanBuildHouseTest()
        {
            PropertyOwnershipTest();

            context.BuyHouse(player, 3);

            Assert.True(context.CanSellHouse(player, 3));
            Assert.False(context.CanBuildHouse(player, 3));

            player.Cash = 0;
            Assert.False(context.CanBuildHouse(player, 3));
        }

        [Fact]
        public void CanSellHouseTest()
        {
            context.BuyHouse(player, 3);
            context.BuyHouse(player, 4);
            context.BuyHouse(player, 4);

            Assert.False(context.CanSellHouse(player, 3));
        }
    }
}
