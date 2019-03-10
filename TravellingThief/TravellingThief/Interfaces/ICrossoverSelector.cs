using System;
using System.Collections.Generic;
using System.Text;
using TravellingThief.TTP;

namespace TravellingThief.Interfaces
{
    public interface ICrossoverSelector
    {
        IEnumerable<Individual> SelectIndividuals(IEnumerable<Individual> population);
    }
}
