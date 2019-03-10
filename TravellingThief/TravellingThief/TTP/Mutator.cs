using System;
using System.Linq;
using TravellingThief.Interfaces;
using TravellingThief.Models;
using TravellingThief.Utilities;

namespace TravellingThief.TTP
{
    public class Mutator : IMutator
    {
        private readonly Random _rand = new Random();

        public Individual MutateIndividualOrNot(Individual individual, double mutationProbability)
        {
            var chance = _rand.NextDouble();

            return chance <= mutationProbability 
                ? Mutate(individual) 
                : new Individual();
        }

        private Individual Mutate(Individual individual)
        {
            var result = new Individual
            {
                Picks = (Item[]) individual.Picks.Clone(),
                Profit = individual.Profit,
                Route = (int[]) individual.Route.Clone(),
                Time = individual.Time
            };
            var temp = result.Route;

            var idx1 = _rand.Next(temp.Length);
            var idx2 = _rand.Next(temp.Length);

            if (idx2 < idx1)
            {
                var swap = idx1;
                idx1 = idx2;
                idx2 = swap;
            }

            var temp2 = temp.Slice(0, idx1).ToList();
            temp2.AddRange(
                temp.Slice(idx1, idx2 - idx1)
                    .Reverse());
            temp2.AddRange(
                temp.Slice(idx2, temp.Length - idx2));

            result.Route = (int[]) temp2.ToArray().Clone();

            return result;
        }
    }
}
