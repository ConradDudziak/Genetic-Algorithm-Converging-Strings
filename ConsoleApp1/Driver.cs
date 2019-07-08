namespace GeneticAlgs.ConvergingStrings {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	class Driver {
		static void Main(string[] args) {
			string target = "Hello world!";
			int maxPopulation = 200;
			float mutationRate = 0.01f;

			Population population = new Population(target, mutationRate, maxPopulation);

			evolve(population);

			Console.WriteLine("Done!");
			Console.ReadKey();
		}

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
