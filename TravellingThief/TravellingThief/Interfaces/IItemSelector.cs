using System;
using System.Collections.Generic;
using System.Text;
using TravellingThief.Models;

namespace TravellingThief.Interfaces
{
    public interface IItemSelector
    {
        Item SelectItemByPwRatio(City city);
        Item[] FreeKnapsackSpace(Item[] items, int cap);
    }
}
