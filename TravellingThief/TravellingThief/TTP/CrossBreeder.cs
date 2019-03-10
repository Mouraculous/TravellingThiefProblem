using System;
using System.Collections.Generic;
using System.Text;
using TravellingThief.Interfaces;

namespace TravellingThief.TTP
{
    public class CrossBreeder : ICrossBreeder
    {
        public IEnumerable<Individual> CrossOver(IEnumerable<Individual> population, double crossingProbability)
        {
            return new List<Individual>();
        }
    }
}
