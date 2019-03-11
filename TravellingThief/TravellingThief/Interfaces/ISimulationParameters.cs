using System;
using System.Collections.Generic;
using System.Text;
using TravellingThief.Models;

namespace TravellingThief.Interfaces
{
    public interface ISimulationParameters
    {
        int KnapsackCapacity { get; set; }
        int CitiesAmount { get; set; }
        double MinSpeed { get; set; }
        double MaxSpeed { get; set; }
        City[] Cities { get; set; }
        Item[] Items { get; set; }
    }
}
