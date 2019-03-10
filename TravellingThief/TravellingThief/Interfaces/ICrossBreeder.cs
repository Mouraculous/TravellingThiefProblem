using System;
using System.Collections.Generic;
using System.Text;
using TravellingThief.TTP;

namespace TravellingThief.Interfaces
{
    public interface ICrossBreeder
    {
        IEnumerable<Individual> CrossOver(IEnumerable<Individual> population, double crossingProbability);
    }
}
