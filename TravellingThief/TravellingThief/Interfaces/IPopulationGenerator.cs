using System;
using System.Collections.Generic;
using System.Text;
using TravellingThief.Models;
using TravellingThief.TTP;

namespace TravellingThief.Interfaces
{
    public interface IPopulationGenerator
    {
        IEnumerable<Individual> GeneratePopulation(int size, ISimulationParameters sim);
    }
}
