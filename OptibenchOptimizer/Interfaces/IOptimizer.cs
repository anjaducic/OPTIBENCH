namespace interfaces
{
     interface IOptimizer
    {
        Task<(double[], double, int)> Optimize(IProblem problem);
    }
}