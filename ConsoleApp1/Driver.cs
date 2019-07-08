// Conrad Dudziak - UWB CSS 497
// July 7th 2019
// A genetic algorithm component that evolves populations.
// A population can be evolved using the evolve method within the Driver class.
// Uses the darwinian principles of heredity, variation, and selection to evolve
// a set of random genes towards a target gene sequence.

namespace GeneticAlgs.ConvergingStrings {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	class Driver {

		// The main method which constructs a population object and evolves it.
		static void Main(string[] args) {
			string target = "Hello world!";
			int maxPopulation = 200;
			float mutationRate = 0.01f;

			Population population = new Population(target, mutationRate, maxPopulation);

			evolve(population);

			Console.WriteLine("Done!");
			Console.ReadKey();
		}

		// Loops until the population has resulted in an individual who matches the target.
		// Evolves the population with every iteration and outputs the results to console.
		static void evolve(Population population) {
			while (!population.isFinished()) {
				population.naturalSelection();
				population.generate();
				population.calculateFitness();
				population.evaluate();
				Console.WriteLine("Current Best: " + population.getBestScorer().toString());
				Console.WriteLine("   Total Generations: " + population.getGenerations());
				Console.WriteLine("   Average Population Fitness: " + population.getAverageFitness());
			}
		}
	}
}
