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

			calculateFitness();
		}

		public void naturalSelection() {
			matingPool.Clear();
			double maxFitness = 0.0;

			for (int i = 0; i < population.Length; i++) {
				if (population[i].getFitness() > maxFitness) {
					maxFitness = population[i].getFitness();
				}
			}

			// Add individuals to the mating pool j times, where j is proportional
			// to the individuals contribution to the totalFitness score.
			for (int i = 0; i < population.Length; i++) {
				double fitness = map(population[i].getFitness(), 0, maxFitness, 0, 1);
				double normalizedFitness = Math.Floor(fitness * 100);
				for (int j = 0; j < normalizedFitness; j++) {
					matingPool.Add(population[i]);
				}
			}
		}

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

		public DNA getBestScorer() {
			return bestScorer;
		}

		public void calculateFitness() {
			for (int i = 0; i < population.Length; i++) {
				population[i].calculateFitness(target);
			}
		}

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

		public bool isFinished() {
			return finished;
		}

		public int getGenerations() {
			return generations;
		}

		public double getAverageFitness() {
			double totalScore = 0.0;
			for (int i = 0; i < population.Length; i++) {
				totalScore += population[i].getFitness();
			}
			return totalScore / population.Length;
		}

		private double map(double value, double minA, double maxA, double minB, double maxB) {
			double valuesRatio = ((value - minA) / (maxA - minA));
			double ratioMinB = valuesRatio * minB;
			double ratioMaxB = valuesRatio * maxB;
			return (1 - ratioMinB + ratioMaxB);
		}
	}
}
