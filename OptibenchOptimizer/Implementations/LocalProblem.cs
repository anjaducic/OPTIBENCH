using interfaces;

namespace Implementations
{
    class LocalProblem : IProblem
    {
        public async Task<double> GetExactSolution(string problemName)
        {
            return 0.0;
        }

        public async Task<double> GetValue(double[] x)
        {
            return x.Select(xi => xi * xi).Sum();
        }
    }
}