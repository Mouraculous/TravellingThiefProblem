using System;
using System.Linq;
using TravellingThief.Preparation;
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
            var setup = new Setup();
            var selector = new GreedyItemSelector();

            var problem = provider.Get();

            var model = parser.Parse(problem.ToArray());

            var population = setup.GeneratePopulation(POPULATION, model.SimulationParams).ToArray();
            
            Console.WriteLine($"KNAPSACK CAPACITY: {model.SimulationParams.KnapsackCapacity}");
            for (var i = 0; i < POPULATION; i++)
            {
                model.CalculateTime(ref population[i], selector);
            }
            Console.WriteLine("FINITO");
            Console.Read();
        }
    }
}
