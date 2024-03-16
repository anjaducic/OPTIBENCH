using System.Net.Http.Json;
using Implementations;
using Dtos;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Utilities;




namespace HttpClientSample
{   
    class Program
    {  
        static void Main()
        {

            var problem_remote = new RemoteProblem("http://localhost:5030", "spherical");
            var problem_local = new LocalProblem();

            var optimizer = new RandomSearchOptimizer([0, 0], [1, 1], 2, 100);
            var optimum = optimizer.Optimize(problem_remote);  //vraca optimum
            optimum.Wait();

            var (x, fx) = optimum.Result;
            Console.WriteLine($"x = [{string.Join(", ", x)}], fx = {fx}");
        } 
    }
}