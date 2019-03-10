using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravellingThief.Interfaces;
using TravellingThief.Models;
using TravellingThief.Utilities;

namespace TravellingThief.TTP
{

    public class Simulation
    {
        private readonly ISimulationParameters _simulationParameters;
        private readonly IMutator _mutator;
        private readonly IItemSelector _itemSelector;
        private readonly IPopulationLogger _logger;
        private readonly IEvaluator _evaluator;
        private readonly ICrossoverSelector _selector;
        private readonly ICrossBreeder _breeder;
        private readonly IPopulationGenerator _populationGenerator;

        public Simulation(
            ISimulationParameters simulationParams, 
            IMutator mutator,
            IItemSelector itemSelector,
            IPopulationLogger logger,
            IEvaluator evaluator,
            ICrossoverSelector selector,
            ICrossBreeder breeder,
            IPopulationGenerator populationGenerator)
        {
            ObjectValidator.IfNullThrowException(simulationParams, nameof(simulationParams));
            ObjectValidator.IfNullThrowException(mutator, nameof(mutator));
            ObjectValidator.IfNullThrowException(itemSelector, nameof(itemSelector));
            ObjectValidator.IfNullThrowException(logger, nameof(logger));
            ObjectValidator.IfNullThrowException(evaluator, nameof(evaluator));
            ObjectValidator.IfNullThrowException(selector, nameof(selector));
            ObjectValidator.IfNullThrowException(breeder, nameof(breeder));
            ObjectValidator.IfNullThrowException(populationGenerator, nameof(populationGenerator));

            _mutator = mutator;
            _itemSelector = itemSelector;
            _logger = logger;
            _evaluator = evaluator;
            _selector = selector;
            _breeder = breeder;
            _populationGenerator = populationGenerator;
            _simulationParameters = simulationParams;
        }

        public void Run(int generations, double crossoverProbability, double mutationProbability, int populationSize)
        {
            var initialPopulation = InitializePopulation(populationSize).ToList();
            initialPopulation = _evaluator.EvaluatePopulation(initialPopulation, _simulationParameters).ToList();
            _logger.Log(0, initialPopulation);

            for (var i = 0; i < generations; i++)
            {
                var currentPopulation = _selector.SelectIndividuals(initialPopulation).ToList();

                currentPopulation.AddRange(_breeder.CrossOver(currentPopulation, crossoverProbability));
                currentPopulation = FixPopulation(currentPopulation, populationSize).ToList();

                currentPopulation = currentPopulation
                    .Select(s => _mutator
                        .MutateIndividualOrNot(s, mutationProbability))
                    .ToList();

                currentPopulation = _evaluator.EvaluatePopulation(currentPopulation, _simulationParameters).ToList();
                initialPopulation = currentPopulation.Clone;

                _logger.Log(i + 1, currentPopulation);
            }
        }

        private IEnumerable<Individual> InitializePopulation(int size)
        {
            return _populationGenerator.GeneratePopulation(size, _simulationParameters);
        }

        private IEnumerable<Individual> FixPopulation(ICollection<Individual> populationToFix, int size)
        {
            populationToFix.ToList()
                .AddRange(_populationGenerator
                    .GeneratePopulation(
                        size - populationToFix.Count,
                        _simulationParameters));

            return populationToFix;
        }

        public void EvaluateIndividual(ref Individual individual, IItemSelector selector)
        {
            var carrying = 0.0;
            var profit = 0.0;
            var time = 0.0;

            for (var i = 0; i < _simulationParameters.Cities.Length; i++)
            {
                individual.Picks[i] = selector.SelectItemByPwRatio(_simulationParameters.Cities[individual.Route[i] - 1]);
            }

            if (individual.Picks.Sum(s => s?.Weight ?? 0) > _simulationParameters.KnapsackCapacity)
                individual.Picks = selector.FreeKnapsackSpace(individual.Picks, _simulationParameters.KnapsackCapacity);

            for (var i = 0; i < _simulationParameters.Cities.Length; i++)
            {
                var index = individual.Route[i];
                var velocity = _simulationParameters.MaxSpeed - carrying * (_simulationParameters.MaxSpeed - _simulationParameters.MinSpeed) / _simulationParameters.KnapsackCapacity;
                var dist = _simulationParameters.Cities[index - 1].CalculateDistance(_simulationParameters.Cities[individual.Route[i + 1] - 1]);
                time += dist / velocity;

                var item = individual.Picks.SingleOrDefault(s => s.NodeNumber == index);
                carrying += item?.Weight ?? 0;
                profit += item?.Profit ?? 0;
            }

            individual.Profit = profit;
            individual.Time = time;
        }
    }
}
