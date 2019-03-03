using System;
using System.Collections.Generic;
using System.Text;

namespace TravellingThief.Models
{
    public class Item
    {
        public int Index { get; set; }
        public double Profit { get; set; }
        public double Weight { get; set; }
        public int NodeNumber { get; set; }
        public double PwRatio { get; private set; }

        public void CalculateRatio() => PwRatio = Profit / Weight;
    }
}
