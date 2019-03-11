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
            _simulationParameters.Items = PickItemsGreedy();
            var initialPopulation = InitializePopulation(populationSize).ToList();
            initialPopulation = _evaluator.EvaluatePopulation(initialPopulation, _simulationParameters).ToList();
            _logger.Log(0, initialPopulation);

            for (var i = 0; i < generations; i++)
            {
                var currentPopulation = _selector.SelectIndividuals(initialPopulation).ToList();

                currentPopulation.AddRange(_breeder.CrossOver(currentPopulation, crossoverProbability));

                if (currentPopulation.Count != populationSize)
                    currentPopulation = FixPopulation(currentPopulation, populationSize).ToList();

                currentPopulation = currentPopulation
                    .Select(s => _mutator
                        .MutateIndividualOrNot(s, mutationProbability))
                    .ToList();

                currentPopulation = _evaluator.EvaluatePopulation(currentPopulation, _simulationParameters).ToList();
                initialPopulation = currentPopulation;

                _logger.Log(i + 1, currentPopulation);
            }
        }

        private IEnumerable<Individual> InitializePopulation(int size)
        {
            return _populationGenerator.GeneratePopulation(size, _simulationParameters);
        }

        private IEnumerable<Individual> FixPopulation(List<Individual> populationToFix, int size)
        {
            if (populationToFix.Count > size) return populationToFix.Take(size);
            var patch = _populationGenerator
                .GeneratePopulation(
                    size - populationToFix.Count,
                    _simulationParameters)
                .ToList();

            populationToFix.AddRange(patch);

            return populationToFix;
        }

        private Item[] PickItemsGreedy()
        {
            var result = new Item[_simulationParameters.Cities.Length];
            for (var i = 0; i < _simulationParameters.Cities.Length; i++)
            {
                result[i] = _itemSelector.SelectItemByPwRatio(_simulationParameters.Cities[i]);
            }

            return result;
        }
    }
}
