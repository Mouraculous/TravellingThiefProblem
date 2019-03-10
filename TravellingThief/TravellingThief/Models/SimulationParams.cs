using System;
using System.Collections.Generic;
using System.Text;
using TravellingThief.Interfaces;

namespace TravellingThief.Models
{
    public class SimulationParams : ISimulationParameters
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
}
