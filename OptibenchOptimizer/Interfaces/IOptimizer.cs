namespace interfaces
{
     interface IOptimizer
    {
        Task<(double[], double)> Optimize(IProblem problem);
    }
}