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


            // Serijalizujem obijekat u json string
            var stringPayload = JsonConvert.SerializeObject(result);

            // StringContent moze da se koristi sa klijentom
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient() ;  //treba li novi klijent u odnosu na onaj za problem?
            httpClient.BaseAddress = new Uri("http://localhost:5201/"); //za "monitor",
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
       
            var httpResponse = httpClient.PostAsync($"result", httpContent);
            httpResponse.Wait();

            
                var (x, fx) = optimum.Result;

                Console.WriteLine($"x = [{string.Join(", ", x)}], fx = {fx}");
            }     
    }
}