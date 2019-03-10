using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TravellingThief.Models;
using TravellingThief.TTP;

namespace TravellingThief.Interfaces
{
    public interface IEvaluator
    {
        Individual EvaluateIndividual(Individual individual, ISimulationParameters simulationParams);
        IEnumerable<Individual> EvaluatePopulation(IEnumerable<Individual> population, ISimulationParameters simulationParams);
    }
}
