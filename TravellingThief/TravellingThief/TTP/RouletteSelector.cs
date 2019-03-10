using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravellingThief.Interfaces;
using TravellingThief.Models;

namespace TravellingThief.TTP
{
    public class RouletteSelector : ICrossoverSelector
    {
        private Random _random;

        public RouletteSelector()
        {
            _random = new Random();
        }

        public IEnumerable<Individual> SelectIndividuals(IEnumerable<Individual> population)
        {
            var totalFitness = population.Sum(s => s.Fitness);

            var list = population.Select(s => new Individual
                {
                    Fitness = s.Fitness / totalFitness,
                    Profit = s.Profit, Time = s.Time,
                    Picks = (Item[]) s.Picks.Clone(),
                    Route = (int[]) s.Route.Clone()
                })
                .OrderBy(o => o.Fitness)
                .ToList();

            var sum = 0.0;
            foreach (var e in list)
            {
                sum += e.Fitness;
                e.Fitness = sum;
            }

            var limit = _random.NextDouble();
            return list.Where(w => w.Fitness >= limit);
        }
    }
}
