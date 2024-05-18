using interfaces;
using Utilities;

namespace Implementations
{
    class RandomSearchOptimizer : IOptimizer
    {

        public RandomSearchOptimizer(OptimizerArguments args) 
        {
            LowerBounds = args.ArrayDoubleSpecs!["LowerBounds"];
            UpperBounds = args.ArrayDoubleSpecs!["UpperBounds"];
            Dimension = args.IntSpecs!["Dimension"];
            MaxIterations = args.IntSpecs!["MaxIterations"];;
        }
       
        public string OptimizerName { get; } = "Random-Search-Csharp-DOTNET";

        public double[] LowerBounds { get; }
        public double[] UpperBounds { get; }
        public int Dimension { get; }
        public int MaxIterations { get; }

        public async Task<(double[], double, int)> Optimize(IProblem problem)
        {
            int iterNum = 0;
            var random = new Random();
            double[] bestX = new double[this.Dimension];
            double bestFitness = double.NaN;
            
            for (int i = 0; i < this.MaxIterations; i++)
            {
                double[] currentX = new double[Dimension];
                double currentFitness;
                for (int j = 0; j < Dimension; j++)
                {
                    currentX[j] = random.NextDouble() * (this.UpperBounds[j] - this.LowerBounds[j]) + this.LowerBounds[j];
                }
                currentFitness = await problem.GetValue(currentX);

                if(double.IsNaN(currentFitness))    //da li je u redu dodati ovu provjeru ovdje
                    continue;
                
                iterNum++;

                // novo najbolje rjesenje
                if (double.IsNaN(bestFitness) || currentFitness < bestFitness)
                {
                    Array.Copy(currentX, bestX, this.Dimension);
                    bestFitness = currentFitness;
                }
            }

            if(iterNum > 0)
                return (bestX, bestFitness, iterNum);
            return (bestX, double.NaN, iterNum);    // :|
        }
    }
}