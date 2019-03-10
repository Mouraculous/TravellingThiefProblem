using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravellingThief.Interfaces;
using TravellingThief.Models;

namespace TravellingThief.TTP
{
    public class CrossBreeder : ICrossBreeder
    {
        private readonly Random _random = new Random();

        public IEnumerable<Individual> CrossOver(IEnumerable<Individual> population, double crossingProbability)
        {
            var populationList = population.ToList();
            if (!populationList.Any()) return new List<Individual>();

            var children = new List<Individual>();

            for (var i = 0; i < populationList.Count - 1; i++)
            {
                if (_random.NextDouble() <= crossingProbability)
                {
                    children.AddRange(
                        PmxCrossover(populationList[i].Route, populationList[i + 1].Route)
                            .Select(s => new Individual
                            {
                                Route = s,
                                Picks = (Item[]) populationList[0].Picks.Clone()
                            }));
                }
            }

            populationList.AddRange(children);
            return populationList;
        }

        private IEnumerable<int[]> PmxCrossover(int[] parentRoute1, int[] parentRoute2)
        {
            var size = parentRoute1.Length;
            var p1 = new int[size];
            var p2 = new int[size];

            for (var i = 0; i < size; i++)
            {
                p1[parentRoute1[i]] = i;
                p2[parentRoute2[i]] = i;
            }

            var point1 = _random.Next(0, size);
            var point2 = _random.Next(0, size - 1);
            if (point2 >= point1)
                point2 += 1;
            else
            {
                var temp = point2;
                point2 = point1;
                point1 = temp;
            }

            for (var i = point1; i < point2; i++)
            {
                var temp1 = parentRoute1[i];
                var temp2 = parentRoute2[i];

                parentRoute1[i] = temp2;
                parentRoute1[p1[temp2]] = temp1;
                parentRoute2[i] = temp1;
                parentRoute2[p2[temp1]] = temp2;

                var swp = p1[temp1];
                p1[temp1] = p1[temp2];
                p1[temp2] = swp;
                swp = p2[temp1];
                p2[temp1] = p2[temp2];
                p2[temp2] = swp;
            }

            return new List<int[]> {parentRoute1, parentRoute2};
        }
    }
}
