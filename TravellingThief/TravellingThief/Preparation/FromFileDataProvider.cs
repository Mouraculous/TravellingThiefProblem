using System.Collections.Generic;
using System.IO;
using System.Linq;
using TravellingThief.Interfaces;

namespace TravellingThief.Preparation
{
    public class FromFileDataProvider : ITextDataProvider
    {
        public IEnumerable<string> Get()
        {
            var path = Directory.EnumerateFiles(Config.SimulationDataPath)?.First(f => f.Contains("hard"));
            return File.ReadAllLines(path);
        }
    }
}
