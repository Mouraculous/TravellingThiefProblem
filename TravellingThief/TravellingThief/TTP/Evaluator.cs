using System.Collections.Generic;
using System.Linq;
using TravellingThief.Interfaces;
using TravellingThief.Models;
using TravellingThief.Utilities;

namespace TravellingThief.TTP
{
    class Evaluator : IEvaluator
    {
        private readonly IItemSelector _itemSelector;

        public Evaluator(IItemSelector itemSelector)
        {
            ObjectValidator.IfNullThrowException(itemSelector, nameof(itemSelector));

            _itemSelector = itemSelector;
        }

        public Individual EvaluateIndividual(Individual individual, ISimulationParameters simulationParams)
        {
            var result = new Individual
            {
                Picks = (Item[]) individual.Picks.Clone(),
                Route = (int[]) individual.Route.Clone()
            };

            var carrying = 0.0;
            var profit = 0.0;
            var time = 0.0;

            //for (var i = 0; i < simulationParams.Cities.Length; i++)
            //{
            //    result.Picks[i] = _itemSelector.SelectItemByPwRatio(simulationParams.Cities[result.Route[i] - 1]);
            //}

            if (result.Picks.Sum(s => s?.Weight ?? 0) > simulationParams.KnapsackCapacity)
                result.Picks = _itemSelector.FreeKnapsackSpace(result.Picks, simulationParams.KnapsackCapacity);

            for (var i = 0; i < simulationParams.Cities.Length; i++)
            {
                var index = result.Route[i];
                var velocity = simulationParams.MaxSpeed - carrying *
                               (simulationParams.MaxSpeed - simulationParams.MinSpeed) /
                               simulationParams.KnapsackCapacity;
                var dist = simulationParams.Cities[index - 1]
                    .CalculateDistance(simulationParams.Cities[result.Route[i + 1] - 1]);
                time += dist / velocity;

                var item = result.Picks.SingleOrDefault(s => s.NodeNumber == index);
                carrying += item?.Weight ?? 0;
                profit += item?.Profit ?? 0;
            }

            result.Profit = profit;
            result.Time = time;
            result.Fitness = 10 * profit - 0.3 * time;
            return result;
        }

        public IEnumerable<Individual> EvaluatePopulation(IEnumerable<Individual> population, ISimulationParameters simulationParams)
        {
            return population.Select(s => EvaluateIndividual(s, simulationParams));
        }
    }
}
