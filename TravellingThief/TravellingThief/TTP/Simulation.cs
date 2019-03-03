using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravellingThief.Interfaces;
using TravellingThief.Models;

namespace TravellingThief.TTP
{
    public class SimulationParams
    {
        public string EdgeWeightType { get; set; }
        public int KnapsackCapacity { get; set; }
        public int CitiesAmount { get; set; }
        public int ItemsAmount { get; set; }
        public double MinSpeed { get; set; }
        public double MaxSpeed { get; set; }
        public double RentingRatio { get; set; }
        public double ItemsPerCity { get; set; }
        public City[] Cities { get; set; }
        public Item[] Items { get; set; }
    }

    public class Simulation
    {
        public SimulationParams SimulationParams { get; }

        public Simulation(SimulationParams simulationParams)
        {
            SimulationParams = simulationParams;
        }

        public void CalculateTime(ref Individual individual, IItemSelector selector)
        {
            var velocity = SimulationParams.MaxSpeed;
            var carrying = 0.0;
            var profit = 0.0;
            var time = 0.0;

            for (var i = 0; i < SimulationParams.Cities.Length; i++)
            {
                individual.Picks[i] = selector.SelectItemByPwRatio(SimulationParams.Cities[individual.Route[i] - 1]);
            }

            if (individual.Picks.Sum(s => s?.Weight ?? 0) > SimulationParams.KnapsackCapacity)
                individual.Picks = selector.FreeKnapsackSpace(individual.Picks, SimulationParams.KnapsackCapacity);

            for (var i = 0; i < SimulationParams.Cities.Length; i++)
            {
                var index = individual.Route[i];
                velocity = SimulationParams.MaxSpeed - carrying * (SimulationParams.MaxSpeed - SimulationParams.MinSpeed) / SimulationParams.KnapsackCapacity;
                var dist = SimulationParams.Cities[index - 1].CalculateDistance(SimulationParams.Cities[individual.Route[i + 1] - 1]);
                time += dist / velocity;

                var item = individual.Picks.SingleOrDefault(s => s.NodeNumber == index);
                carrying += item?.Weight ?? 0;
                profit += item?.Profit ?? 0;
            }

            individual.Profit = profit;
            individual.Time = time;
            //Console.WriteLine($"Weight: { carrying}");
        }
    }
}
