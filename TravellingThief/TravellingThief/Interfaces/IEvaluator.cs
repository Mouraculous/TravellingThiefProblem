using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TravellingThief.Models;

namespace TravellingThief.Interfaces
{
    interface IEvaluator
    {
        double Evaluate(City[] cities);
    }
}
