// Conrad Dudziak - UWB CSS 497
// July 7th 2019
// A genetic algorithm component that serves as a set of individuals (DNA) objects.
// A population can be evolved using the evolve method within the Driver class.
// Uses the darwinian principles of heredity, variation, and selection to evolve
// a set of random genes towards a target gene sequence.

namespace GeneticAlgs.ConvergingStrings {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	class Population {

		DNA[] population;
		List<DNA> matingPool;
		DNA bestScorer;
		string target;
		double mutationRate;
		int generations;
		bool finished;

		// Constructs a population object with the maxPopulation size, where each
		// individual gene sequence is the length of the target.
		public Population(string target, float mutationRate, int maxPopulation) {
			this.population = new DNA[maxPopulation];
			this.matingPool = new List<DNA>();
			this.bestScorer = null;
			this.target = target;
			this.mutationRate = mutationRate;
			this.generations = 0;
			this.finished = false;

			for (int i = 0; i < maxPopulation; i++) {
				population[i] = new DNA(target.Length);
			}

			// Calculate the original fitness of the population.
			calculateFitness();
		}

		// Populate the mating pool of the individuals elidgible for reproduction.
		public void naturalSelection() {
			matingPool.Clear();
			double bestFitness = 0.0;

			// Loop through the population and find the largest fitness score.
			for (int i = 0; i < population.Length; i++) {
				if (population[i].getFitness() > bestFitness) {
					bestFitness = population[i].getFitness();
				}
			}

			// Add individuals to the mating pool j times, where j is proportional
			// to the best fitness score.
			for (int i = 0; i < population.Length; i++) {
				double fitness = map(population[i].getFitness(), 0, bestFitness, 0, 1);
				double normalizedFitness = Math.Floor(fitness * 100);
				for (int j = 0; j < normalizedFitness; j++) {
					matingPool.Add(population[i]);
				}
			}
		}

		// Replace each individual in the population with a child whose parents
		// are randomly selected from the mating pool.
		public void generate() {
			for (int i = 0; i < population.Length; i++) {
				int firstParentIndex = (int) DNA.RandomDouble(0, matingPool.Count);
				int secondParentIndex = (int) DNA.RandomDouble(0, matingPool.Count);

				DNA firstParent = matingPool.ElementAt(firstParentIndex);
				DNA secondParent = matingPool.ElementAt(secondParentIndex);
				DNA child = firstParent.crossover(secondParent);

				child.mutate(mutationRate);
				population[i] = child;
			}
			generations++;
		}

		// Return the best scoring DNA (individual)
		public DNA getBestScorer() {
			return bestScorer;
		}

		// Calculate the fitness of each individual in the population.
		public void calculateFitness() {
			for (int i = 0; i < population.Length; i++) {
				population[i].calculateFitness(target);
			}
		}

		// Evaluate if the population has converged to the target and who
		// the current best scorer is.
		public void evaluate() {
			double bestFitness = 0.0;
			int index = 0;

			for (int i = 0; i < population.Length; i++) {
				if (population[i].getFitness() > bestFitness) {
					bestFitness = population[i].getFitness();
					index = i;
				}
			}

			bestScorer = population[index];
			if (bestFitness == 1.0) {
				finished = true;
			}
		}

		// Return whether or not the population is converged to the target.
		public bool isFinished() {
			return finished;
		}

		// Return the total generations.
		public int getGenerations() {
			return generations;
		}

		// Return the average fitness of the entire population as a calcultion of
		// all the individuals summated fitnesses.
		public double getAverageFitness() {
			double totalScore = 0.0;
			for (int i = 0; i < population.Length; i++) {
				totalScore += population[i].getFitness();
			}
			return totalScore / population.Length;
		}

		// Return a value that is rescaled.
		// Receives a value and rescales that value from A's min and max to B's min and max.
		private double map(double value, double minA, double maxA, double minB, double maxB) {
			double valuesRatio = ((value - minA) / (maxA - minA));
			double ratioMinB = valuesRatio * minB;
			double ratioMaxB = valuesRatio * maxB;
			return (1 - ratioMinB + ratioMaxB);
		}
	}
}
