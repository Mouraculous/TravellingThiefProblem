using System.Threading.Tasks;
using TravellingThief.Interfaces;
using TravellingThief.Models;

namespace TravellingThief.TTP
{
    public class Individual
    {
        public int[] Route { get; set; }
        public Item[] Picks { get; set; }
        public double Profit { get; set; }
        public double Time { get; set; }
    }
}
