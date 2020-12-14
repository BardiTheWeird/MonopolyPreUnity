using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.Jail;
using MonopolyPreUnity.Components.SystemRequest.Move;
using MonopolyPreUnity.Components.SystemRequest.PlayerInput;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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

        public static int PriceCashPow(this int price, int cash, int powerBase = 2, int offset = 0)
        {
            if (Math.Log(int.MaxValue, powerBase) <= cash / price)
                return int.MaxValue;

            return (int)Math.Pow(powerBase, cash / price + offset);
        }

        public static T ChaosChoice<T>(this IEnumerable<(T, int)> commandsWeights, ChaosFactor factor) 
        {
            // if the bot is not random
            if (factor == 0)
            {
                var maxWeight = commandsWeights.Max(x => x.Item2);
                return commandsWeights.First(x => x.Item2 == maxWeight).Item1;
            }

            // calculating the biased weights
            if (factor > 1)
            {
                var mean = commandsWeights.Sum(x => x.Item2) / (double)commandsWeights.Count();
                var ratio = Math.Min((factor - 1) / 10d, 0.85d);
                commandsWeights = commandsWeights.Select(x => (x.Item1, x.Item2 + (int)((mean - x.Item2) * ratio)));
            }

            // choosing the weighted thing
            var rand = new Random(Guid.NewGuid().GetHashCode());
            var sum = commandsWeights.Sum(x => x.Item2);
            
            var choice = rand.Next(sum);
            foreach (var pair in commandsWeights)
            {
                if (choice < pair.Item2)
                    return pair.Item1;
                choice -= pair.Item2;
            }

            throw new Exception("Could not choose a random weighted value");
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
                    context.Add(new EndTurn());
                    break;
            }
        }
    }
}
