using System;
using System.Linq;
using TravellingThief.Preparation;
using TravellingThief.Statistics;
using TravellingThief.TTP;

namespace TravellingThief
{
    class Program
    {
        private const int POPULATION = 200;

        static void Main(string[] args)
        {
            var parser = new TextToSimModelParser();
            var provider = new FromFileDataProvider();
            var populationGenerator = new PopulationGenerator();
            var selector = new GreedyItemSelector();
            var mutator = new Mutator();
            var popLogger = new FilePopulationLogger();
            var evaluator = new Evaluator(selector);
            var crossoverSelector = new RouletteSelector();
            var crossBreeder = new CrossBreeder();

            var problem = provider.Get();

            var model = parser.Parse(problem.ToArray());

            var simulation = new Simulation(model, mutator, selector, popLogger, evaluator, crossoverSelector,
                crossBreeder, populationGenerator);

            simulation.Run(Config.Generations, Config.CrossoverProbability, Config.MutationProbability,
                Config.Population);

            Console.WriteLine("FINITO");
            Console.Read();
        }
    }
}
