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
        

            ParameterJsonGenerator generator = new ParameterJsonGenerator();
            var result = new OptimizationResultDto(
            x, // X
            fx, // Y
            generator.GenerateJson(new Dictionary<string, object>{
                {"param1", new double[] { 1.2, 3.4, 5.6 }},
                {"param2", "some string" }
            }), // Params
            "spherical", // ProblemName
            "{\"number\": 30.0}" // EvaluationCount
            );

            var monitor = new Implementations.Monitor("http://localhost:5201/");//Zahtjeva namespace zbog System.Threading.Monitor-a
            var monitoring = monitor.Save(result);
            monitoring.Wait();  //jel ovo moze da se dodijeli prom. iako vraca samo Task, tj nista


            } 

            //ne saljem id, baza ga upise sama :D  
            //koliko je bitna float preciznost decimalna za rjesenje 
            //pitati za eval. count dal ga vracati u optimize, i zasto je on json i dal je to maxiter 
            //treba li random search da se izvrsava do nekog epsilon ili uvijek maxiter puta
            //namespacovi
    }
}