using Dtos;
using interfaces;
using Utilities;

namespace Implementations
{
    class RandomSearchOptimizer : IOptimizer
    {

        public RandomSearchOptimizer(OptimizerArguments args) 
        {
            LowerBounds = args.ArrayDoubleSpecs!["LowerBounds"];
            UpperBounds = args.ArrayDoubleSpecs!["UpperBounds"];;
            Dimension = args.IntSpecs!["Dimension"];
            MaxIterations = args.IntSpecs!["MaxIterations"];;
        }
       

        public double[] LowerBounds { get; }
        public double[] UpperBounds { get; }
        public int Dimension { get; }
        public int MaxIterations { get; }
        //private Monitor monitor = new Monitor("http://localhost:5201/");

        public async Task<(double[], double)> Optimize(IProblem problem)
        {
            var random = new Random();
            double[] bestX = new double[this.Dimension];
            for (int i = 0; i < this.Dimension; i++)
            {
                bestX[i] = random.NextDouble() * (this.UpperBounds[i] - this.LowerBounds[i]) + this.LowerBounds[i];
            }
            double bestFitness = await problem.GetValue(bestX);

            for (int i = 0; i < this.MaxIterations; i++)
            {
                double[] currentX = new double[this.Dimension];
                for (int j = 0; j < this.Dimension; j++)
                {
                    currentX[j] = random.NextDouble() * (this.UpperBounds[j] - this.LowerBounds[j]) + this.LowerBounds[j];
                }
                double currentFitness = await problem.GetValue(currentX);

                // novo najbolje rjesenje
                if (currentFitness < bestFitness)
                {
                    Array.Copy(currentX, bestX, this.Dimension);
                    bestFitness = currentFitness;
                }
            }

            

            return (bestX, bestFitness);
        }
    }
}