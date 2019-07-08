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

	class DNA {

		private static readonly Random random = new Random();
		private char[] genes;
		private double fitness;

		// Constructs a DNA object with a random gene sequence of parameter length.
		public DNA(int length) {
			genes = getRandomGeneSequence(length);
			fitness = 0.0;
		}

		// Calculates the DNA objects fitness to the target string parameter.
		// Fitness is calculated by comparing every value and incremeting the score
		// based on each correct value.
		public void calculateFitness(string target) {
			int score = 0;
			for (int i = 0; i < genes.Length; i++) {
				if (genes[i] == target[i]) {
					score++;
				}
			}
			fitness = (double) score / (double) target.Length;
		}

		// Recieves a DNA object other, and returns a new DNA object with a gene sequence
		// made from this DNA object and other. The two gene sequences are spliced
		// together at a random midpoint.
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

		// Recieves a mutationRate. Loops through every gene
		// in the gene sequence of the DNA, and randomly decides to
		// mutate genes to a new value.
		public void mutate(double mutationRate) {
			for (int i = 0; i < genes.Length; i++) {
				if (RandomDouble(0, 1) < mutationRate) {
					genes[i] = getRandomChar();
				}
			}
		}

		// Returns the DNA's fitness score.
		public double getFitness() {
			return fitness;
		}

		// Returns a random char between the unicode 32 and 126.
		public char getRandomChar() {
			// Create a random integer from 32 to 126
			int randomUnicode = (int) RandomDouble(32, 127);
			// Cast the random integer to a String through unicode
			return Convert.ToChar(randomUnicode);
		}

		// Returns a char array of parameter length that is randomly populated
		// with chars.
		public char[] getRandomGeneSequence(int length) {
			char[] genes = new char[length];
			for (int i = 0; i < length; i++) {
				genes[i] = getRandomChar();
			}
			return genes;
		}

		// Returns the gene sequence as a string.
		public string toString() {
			return new string(genes);
		}

		// Returns a random double less than or equal to min and less than max.
		public static double RandomDouble(int min, int max) {
			return random.NextDouble() * (max - min) + min;
		}
	}
}
