namespace interfaces
{
    public interface IProblem
    {
        Task<double> GetValue(double[] x);
        Task<double> GetExactSolution(string problemName);
    }
}
