using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.Jail;
using MonopolyPreUnity.Components.SystemRequest.Move;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Weighted_Randomizer;

namespace MonopolyPreUnity.RequestHandlers.AIScenario
{
    static class AIExtensions
    {
        public static int PropertyAcquisitionSetWeight(this Context context, Player player, int setId)
        {
            var set = context.GetPropertySet(setId);
            var intersection = player.Properties.Intersect(set);

            if (intersection.Count() == 0)
                return 0;

            return 5 + (intersection.Count() - 1) * 10;
        }

        public static int PriceCashPow(this int price, int cash, int powerBase = 2) =>
            (int)Math.Pow(powerBase, cash / price);

        public static T ChaosChoice<T>(this List<(T, int)> commandsWeight, ChaosFactor factor) 
        {
            if (factor == 0)
            {
                var maxWeight = commandsWeight.Max(x => x.Item2);
                return commandsWeight.First(x => x.Item2 == maxWeight).Item1;
            }

            var mean = commandsWeight.Sum(x => x.Item2) / (double)commandsWeight.Count;
            var ratio = Math.Max(factor / 10d, 0.95d);
            var correctedWeights = commandsWeight.Select(x => (x.Item1, x.Item2 + (int)((mean - x.Item2) * ratio)));

            var weightedRand = new StaticWeightedRandomizer<T>(Guid.NewGuid().GetHashCode());
            commandsWeight.ForEach(x => weightedRand.Add(x.Item1, x.Item2));

            return weightedRand.NextWithReplacement();
        }

        public static void AddCommand(this Context context, MonopolyCommand command, Player player)
        {
            switch (command)
            {
                case MonopolyCommand.UseJailCard:
                    context.Add(new JailUseCard(player.Id));
                    break;
                case MonopolyCommand.PayJailFine:
                    context.Add(new JailPayFine(player.Id));
                    break;
                case MonopolyCommand.JailRollDice:
                    context.Add(new ThrowDice());
                    context.Add(new JailDiceRoll(player.Id));
                    break;

                case MonopolyCommand.MakeMove:
                    context.Add(new ThrowDice());
                    context.Add(new MoveDice(player.Id));
                    break;
                case MonopolyCommand.EndTurn:
                    break;
            }
        }
    }
}
