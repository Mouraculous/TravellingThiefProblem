using System;
using System.Collections.Generic;
using System.Text;
using TravellingThief.Models;

namespace TravellingThief.Interfaces
{
    public interface ISimulationParameters
    {
        string EdgeWeightType { get; set; }
        int KnapsackCapacity { get; set; }
        int CitiesAmount { get; set; }
        int ItemsAmount { get; set; }
        double MinSpeed { get; set; }
        double MaxSpeed { get; set; }
        double RentingRatio { get; set; }
        double ItemsPerCity { get; set; }
        City[] Cities { get; set; }
        Item[] Items { get; set; }
    }
}
