using interfaces;

namespace Implementations
{
    class LocalProblem : IProblem
    {
        public async Task<double> GetValue(double[] x)
        {
            return x.Select(xi => xi * xi).Sum();
        }
    }
}