using System.Collections.Generic;
using TravellingThief.TTP;

namespace TravellingThief.Interfaces
{
    public interface IPopulationLogger
    {
        void Log(int generation, IEnumerable<Individual> population);
    }
}
