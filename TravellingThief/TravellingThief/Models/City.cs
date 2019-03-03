using System;
using System.Collections.Generic;
using System.Text;

namespace TravellingThief.Models
{
    public class City
    {
        public int Index { get; set; }
        public double XCoord { get; set; }
        public double YCoord { get; set; }
        public double TotalWeight { get; set; }
        public double TotalProfit { get; set; }
        public double PwRatio { get; private set; }
        public List<Item> Items { get; set; }

        public double CalculateDistance(City other)
        {
            var sum = Math.Pow(XCoord - other.XCoord, 2) + Math.Pow(YCoord - other.YCoord, 2);
            return Math.Sqrt(sum);
        }

        public void CalculateRatio() => PwRatio = TotalProfit / TotalWeight;
    }
}
