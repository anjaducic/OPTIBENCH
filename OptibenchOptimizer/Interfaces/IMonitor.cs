using Dtos;

namespace interfaces
{
     interface IMonitor
    {
        Task Save(OptimizationResultDto result);
    }
}