using System.Net.Http.Json;
using Implementations;
using Dtos;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;




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
            var result = new OptimizationResultDto(
            1, // Id
            [1, 2, 3], // X
            3, // Y
            "{\"prviParam\": 10.0, \"drugiParam\": 20.0}", // Params
            "spherical", // ProblemName
            "{\"number\": 30.0}" // EvaluationCount
            );

            var monitor = new Implementations.Monitor("http://localhost:5201/");//Zahtjeva namespace zbog System.Threading.Monitor-a
            var monitoring = monitor.Save(result);
            monitoring.Wait();  //jel ovo moze da se dodijeli prom. iako vraca samo Task, tj nista



            
            

            
       
           

            
            var (x, fx) = optimum.Result;

            Console.WriteLine($"x = [{string.Join(", ", x)}], fx = {fx}");
        } 

            //ne saljem id, baza ga upise sama :D    
    }
}