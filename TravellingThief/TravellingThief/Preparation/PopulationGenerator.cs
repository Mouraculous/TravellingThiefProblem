using System;
using System.Collections.Generic;
using TravellingThief.Interfaces;
using TravellingThief.Models;
using TravellingThief.TTP;

namespace TravellingThief.Preparation
{
    public class PopulationGenerator : IPopulationGenerator
    {
        static Random rand = new Random();

        public IEnumerable<Individual> GeneratePopulation(int size, ISimulationParameters sim)
        {
            var population = new List<Individual>();
            var array = new int[sim.CitiesAmount + 1];
            var items = (Item[])sim.Items.Clone();

            for (var i = 0; i < sim.CitiesAmount; i++)
            {
                array[i] = i + 1;
            }

            for (var i = 0; i < size; i++)
            {
                rand.Next();
                for (var j = 0; j < sim.CitiesAmount; j++)
                {
                    var index = rand.Next(sim.CitiesAmount);
                    var swp = array[index];
                    array[index] = array[j];
                    array[j] = swp;
                }

                array[sim.CitiesAmount] = array[0];
                population.Add(new Individual{Route = (int[])array.Clone(), Picks = (Item[])items.Clone()});
            }

            return population;
        }
    }
    
}
