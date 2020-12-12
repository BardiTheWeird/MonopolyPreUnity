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
        public static MonopolyCommand ChaosChoice(this List<(MonopolyCommand, int)> commandsWeight, ChaosFactor factor) 
        {
            if (factor == 0)
            {
                var maxWeight = commandsWeight.Max(x => x.Item2);
                return commandsWeight.First(x => x.Item2 == maxWeight).Item1;
            }

            var mean = commandsWeight.Sum(x => x.Item2) / (double)commandsWeight.Count;
            var ratio = Math.Max(factor / (double)10, 0.95f);
            commandsWeight.ForEach(x => x.Item2 += (int)((mean - x.Item2) * ratio));

            var weightedRand = new StaticWeightedRandomizer<MonopolyCommand>(Guid.NewGuid().GetHashCode());
            commandsWeight.ForEach(x => weightedRand.Add(x.Item1, x.Item2));

            return weightedRand.NextWithReplacement();
        }
    }
}
