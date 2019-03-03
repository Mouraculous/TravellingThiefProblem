using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TravellingThief.Interfaces;
using TravellingThief.Models;
using TravellingThief.TTP;

namespace TravellingThief.Preparation
{
    public class TextToSimModelParser : ITextToSimModelParser
    {
        public Simulation Parse(string[] text)
        {
            var nfi = new NumberFormatInfo {NumberDecimalSeparator = "."};

            var model = new SimulationParams
            {
                CitiesAmount = Convert.ToInt32(text[2].Split(':')[1].Trim()),
                ItemsAmount = Convert.ToInt32(text[3].Split(':')[1].Trim()),
                KnapsackCapacity = Convert.ToInt32(text[4].Split(':')[1].Trim()),
                MinSpeed = Convert.ToDouble(text[5].Split('\t')[1].Trim(), nfi),
                MaxSpeed = Convert.ToDouble(text[6].Split('\t')[1].Trim(), nfi),
                RentingRatio = Convert.ToDouble(text[7].Split('\t')[1].Trim(), nfi),
                EdgeWeightType = text[8].Split(':')[1]
            };

            var cities = GetCities(text, nfi);
            var items = GetItems(text, ref cities, nfi);

            CalculateCities(cities);

            model.Items = items;
            model.Cities = cities;
            model.ItemsPerCity = (double) items.Length / cities.Length;

            return new Simulation(model);
        }

        private static Item[] GetItems(IEnumerable<string> text, ref City[] cities, IFormatProvider nfi)
        {
            var textItems = text.SkipWhile(s => !s.Contains("ITEMS SECTION")).Skip(1).ToArray();
            var items = new Item[textItems.Length];

            for (var i = 0; i < items.Length; i++)
            {
                var s = textItems[i].Split('\t');
                items[i] = new Item
                {
                    Index = Convert.ToInt32(s[0]),
                    Profit = Convert.ToDouble(s[1], nfi),
                    Weight = Convert.ToDouble(s[2], nfi),
                    NodeNumber = Convert.ToInt32(s[3])
                };
                items[i].CalculateRatio();
                cities[items[i].NodeNumber - 1].TotalWeight += items[i].Weight;
                cities[items[i].NodeNumber - 1].TotalProfit += items[i].Profit;
                cities[items[i].NodeNumber - 1].Items.Add(items[i]);
            }

            return items;
        }

        private static City[] GetCities(IEnumerable<string> strings, IFormatProvider nfi)
        {
            return strings.Skip(10).TakeWhile(w => !w.Contains("ITEMS")).Select(s => s.Split('\t')).Select(s =>
                new City
                {
                    Index = Convert.ToInt32(s[0]),
                    XCoord = Convert.ToDouble(s[1], nfi),
                    YCoord = Convert.ToDouble(s[2], nfi),
                    Items = new List<Item>()
                }).ToArray();
        }

        private static void CalculateCities(IEnumerable<City> cities)
        {
            foreach (var city in cities)
            {
                city.CalculateRatio();
            }
        }
    }
}
