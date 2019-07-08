namespace GeneticAlgs.ConvergingStrings {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	class DNA {
		private static readonly Random random = new Random();
		private char[] genes;
		private double fitness;

		public DNA(int length) {
			genes = getRandomGeneSequence(length);
			fitness = 0.0;
		}

		public void calculateFitness(string target) {
			int score = 0;
			for (int i = 0; i < genes.Length; i++) {
				if (genes[i] == target[i]) {
					score++;
				}
			}
			fitness = (double) score / (double) target.Length;
		}

		public DNA crossover(DNA other) {
			DNA result = new DNA(genes.Length);

			int midpoint = (int) RandomDouble(0, genes.Length);

			for (int i = 0; i < genes.Length; i++) {
				if (i > midpoint) {
					result.genes[i] = genes[i];
				} else {
					result.genes[i] = other.genes[i];
				}
			}

			return result;
		}

		public void mutate(double mutationRate) {
			for (int i = 0; i < genes.Length; i++) {
				if (RandomDouble(0, 1) < mutationRate) {
					genes[i] = getRandomChar();
				}
			}
		}

		public double getFitness() {
			return fitness;
		}

		public char getRandomChar() {
			// Create a random integer from 32 to 126
			int randomUnicode = (int) RandomDouble(32, 127);
			// Cast the random integer to a String through unicode
			return Convert.ToChar(randomUnicode);
		}

		public char[] getRandomGeneSequence(int length) {
			char[] genes = new char[length];
			for (int i = 0; i < length; i++) {
				genes[i] = getRandomChar();
			}
			return genes;
		}

		public string toString() {
			return new string(genes);
		}

		// Returns a random double less than or equal to min and less than max
		public static double RandomDouble(int min, int max) {
			return random.NextDouble() * (max - min) + min;
		}
	}
}
