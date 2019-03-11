using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TravellingThief.Interfaces;
using TravellingThief.TTP;

namespace TravellingThief.Statistics
{
    class FilePopulationLogger : IPopulationLogger
    {
        public void Log(int generation, IEnumerable<Individual> population)
        {
            var path = $"{Config.DumpFilePath}.csv";

            var listedPopulation = population.ToList();

            if (!File.Exists(path))
            {
                File.WriteAllText(path, "GENERATION; BEST RESULT; MEAN AVG; WORST RESULT\n");
            }

            var toWrite = string.Join(';',
                generation, listedPopulation.Max(m => m.Fitness),
                listedPopulation.Average(a => a.Fitness),
                listedPopulation.Min(m => m.Fitness)
            );

            File.AppendAllLines(path, new [] {toWrite});
        }
    }
}

