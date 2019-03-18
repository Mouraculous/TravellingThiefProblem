## Travelling Thief Problem: Evolutionary Algorithm

This repository contains my proposition of a solution for Travelling Thief Problem (later called TTP).

The problem consists of two NP-Hard problems - Travelling Salesman Problem (TSP) and Knapsack Problem (KNP)

Solution is being developed as an evolutionary algorithm for TSP and greedy algorithm for KNP (for now)

Algorithm DOES NOT produce the optimal path and item configuration for the provided input, as genetic algorithms use randomization to provide you with a solution. The result of this algorithm is suboptimal (which is still very good, when we take execution time into the account)

My approach:
  1. Selection: Roulette (but I will implement Tournament eventually)
  2. Mutation: Partial Reversal
  3. Crossover: Partially Mapped Crossover (PMX)

TODO: 
Change selection from this stupid method to proper elitism or sth else.
Fix Roulette.
Mutation per gene, not per specimen.
Crossover fix to not change the first and last node
Create a dictionary (Specimen, Fitness) to limit the amount of fitness function calls

