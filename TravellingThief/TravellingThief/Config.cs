using System;
using System.Collections.Generic;
using System.Text;

namespace TravellingThief
{
    public class Config
    {
        public static string SimulationDataPath = @"..\..\..\student";
        public static string DumpFilePath = @"..\..\..\student\dump";

        public static int Generations = 100;
        public static int Population = 100;
        public static double CrossoverProbability = 0.1;
        public static double MutationProbability = 0.1;
    }
}
