namespace interfaces
{
    interface IProblem
    {
        Task<double> GetValue(double[] x);
    }
}
