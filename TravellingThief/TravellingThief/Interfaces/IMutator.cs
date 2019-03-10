using System;
using System.Collections.Generic;
using System.Text;
using TravellingThief.TTP;

namespace TravellingThief.Interfaces
{
    public interface IMutator
    {
        Individual MutateIndividualOrNot(Individual individual, double mutationProbability);
    }
}
