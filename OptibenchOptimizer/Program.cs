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

            var args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {0,0}}, {"UpperBounds", new double[] {1,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 100 }},
            };
            //Console.WriteLine(args.GenerateJson());

            var optimizer = new RandomSearchOptimizer(args);
            var optimum = optimizer.Optimize(problem_remote);  //vraca optimum
            optimum.Wait();
            var (x, fx) = optimum.Result;
            Console.WriteLine($"x = [{string.Join(", ", x)}], fx = {fx}");

            //store result
            ParameterJsonGenerator generator = new ParameterJsonGenerator();
            var result = new OptimizationResultDto(x, fx, args.GenerateJson(), // Params
        /*odakle problemName  */                    "spherical", generator.GenerateJson(new Dictionary<string, object>{{"Count", 100}}));

            var monitor = new Implementations.Monitor("http://localhost:5201/");//Zahtjeva namespace zbog System.Threading.Monitor-a
            var monitoring = monitor.Save(result);
            monitoring.Wait();  //jel ovo moze da se dodijeli prom. iako vraca samo Task, tj nista

            
        } 
    }
}