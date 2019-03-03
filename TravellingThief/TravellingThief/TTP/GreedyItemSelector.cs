using System.Collections.Generic;
using System.Linq;
using TravellingThief.Interfaces;
using TravellingThief.Models;

namespace TravellingThief.TTP
{
    public class GreedyItemSelector : IItemSelector
    {
        public Item SelectItemByPwRatio(City city)
        {
            if (city.Items.Count == 1) return city.Items.First();
            city.Items.Sort((c1, c2) => c1.PwRatio.CompareTo(c2.PwRatio));
            return city.Items.FirstOrDefault();
        }

        public Item[] FreeKnapsackSpace(Item[] items, int cap)
        {
            var weight = items.Sum(s => s?.Weight ?? 0);
            var orderedItems = items.Where(w => w != null).OrderByDescending(o => o.Weight).ToArray();

            while (weight > cap)
            {
                weight -= orderedItems[0].Weight;
                orderedItems = orderedItems.Skip(1).ToArray();
            }

            return orderedItems;
        }
    }
}
