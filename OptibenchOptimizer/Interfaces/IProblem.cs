namespace interfaces
{
    public interface IProblem
    {
        Task<double> GetValue(double[] x);
    }
}
