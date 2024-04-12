using Implementations;
using Dtos;
using Utilities;

namespace HttpClientSample
{   
    class Program
    {  
        static void Main()
        {
            var problem_local = new LocalProblem();

            var spherical_remote = new RemoteProblem("http://localhost:5030", "Spherical");
            var rosenbrock_remote = new RemoteProblem("http://localhost:5030", "Rosenbrock");
            var rastrigin_remote = new RemoteProblem("http://localhost:5030", "Rastrigin");
            var shekel_remote = new RemoteProblem("http://localhost:5030", "Shekel");
            var matyas_remote = new RemoteProblem("http://localhost:5030", "Matyas");
            var easom_remote = new RemoteProblem("http://localhost:5030", "Easom");
            var gomez_levi_remote = new RemoteProblem("http://localhost:5030", "GomezLevi");
            var mishras_bird_remote = new RemoteProblem("http://localhost:5030", "MishrasBird");


            var py_spherical_remote = new RemoteProblem("http://localhost:5055", "Spherical");




   

            var spherical_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-1,-1}}, {"UpperBounds", new double[] {1,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };
            var rosenbrock_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {0,0}}, {"UpperBounds", new double[] {2,2} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };
            var rastrigin_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-1,-1}}, {"UpperBounds", new double[] {1,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };
            var shekel_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-1,-1}}, {"UpperBounds", new double[] {1,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };
            var matyas_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-1,-1}}, {"UpperBounds", new double[] {1,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };
            var easom_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {2,2}}, {"UpperBounds", new double[] {4,4} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };
            var gomez_levi_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-1,-1}}, {"UpperBounds", new double[] {0.75,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };
            var mishras_bird_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-4,-2.5}}, {"UpperBounds", new double[] {-2,-0.5} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };

            var matyas_pso_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"NumDime", new double[] {-4,-2.5}}, {"UpperBounds", new double[] {-2,-0.5} }},
                IntSpecs = new Dictionary<string, int>{{"NumDimensions", 2}},
            };
            //Console.WriteLine(args.GenerateJson());

            var monitor = new Implementations.Monitor("http://localhost:5201/");//Zahtjeva namespace zbog System.Threading.Monitor-a
            var spherical_random_search_optimizer = new RandomSearchOptimizer(spherical_args);
            var rosenbrock_random_search_optimizer = new RandomSearchOptimizer(rosenbrock_args);
            var rastrigin_random_search_optimizer = new RandomSearchOptimizer(rastrigin_args);
            var shekel_random_search_optimizer = new RandomSearchOptimizer(shekel_args);
            var matyas_random_search_optimizer = new RandomSearchOptimizer(matyas_args);
            var easom_random_search_optimizer = new RandomSearchOptimizer(easom_args);
            var gomez_levi_random_search_optimizer = new RandomSearchOptimizer(gomez_levi_args);
            var mishras_bird_random_search_optimizer = new RandomSearchOptimizer(mishras_bird_args);

            

            ParameterJsonGenerator generator = new ParameterJsonGenerator();

            //spherical
            /*var spherical_optimum = spherical_random_search_optimizer.Optimize(spherical_remote);  //vraca optimum
            spherical_optimum.Wait();
            var (x, fx, iterNum) = spherical_optimum.Result;
            Console.WriteLine($"spherical: x = [{string.Join(", ", x)}], fx = {fx}");
            //store result
            var spherical_result = new OptimizationResultDto(x, fx, spherical_args.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", spherical_remote.Uri},{"ProblemName", spherical_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), "RandomSearch");
            var monitoring = monitor.Save(spherical_result);
            monitoring.Wait(); 
            

            //rosenbrock
            var rosenbrock_optimum = rosenbrock_random_search_optimizer.Optimize(rosenbrock_remote);  //vraca optimum
            rosenbrock_optimum.Wait();
            (x, fx, iterNum) = rosenbrock_optimum.Result;
            //store result
            var rosenbrock_result = new OptimizationResultDto(x, fx, rosenbrock_args.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", rosenbrock_remote.Uri},{"ProblemName", rosenbrock_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), "RandomSearch");
            monitoring = monitor.Save(rosenbrock_result);
            monitoring.Wait(); 
            Console.WriteLine($"rosenbrock: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //rastrigin
            var rastrigin_optimum = rastrigin_random_search_optimizer.Optimize(rastrigin_remote);  //vraca optimum
            rastrigin_optimum.Wait();
            (x, fx, iterNum) = rastrigin_optimum.Result;
            //store result
            var rastrigin_result = new OptimizationResultDto(x, fx, rastrigin_args.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", rastrigin_remote.Uri},{"ProblemName", rastrigin_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), "RandomSearch");
            monitoring = monitor.Save(rastrigin_result);
            monitoring.Wait(); 
            Console.WriteLine($"rastrigin: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //shekel
            var shekel_optimum = shekel_random_search_optimizer.Optimize(shekel_remote);  //vraca optimum
            shekel_optimum.Wait();
            (x, fx, iterNum) = shekel_optimum.Result;
            //store result
            var shekel_result = new OptimizationResultDto(x, fx, shekel_args.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", shekel_remote.Uri},{"ProblemName", shekel_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), "RandomSearch");
            monitoring = monitor.Save(shekel_result);
            monitoring.Wait(); 
            Console.WriteLine($"shekel: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //matyas
            var matyas_optimum = matyas_random_search_optimizer.Optimize(matyas_remote);  //vraca optimum
            matyas_optimum.Wait();
            (x, fx, iterNum) = matyas_optimum.Result;
            //store result
            var matyas_result = new OptimizationResultDto(x, fx, matyas_args.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", matyas_remote.Uri},{"ProblemName", matyas_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), "RandomSearch");
            monitoring = monitor.Save(matyas_result);
            monitoring.Wait(); 
            Console.WriteLine($"matyas: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //easom
            var easom_optimum = easom_random_search_optimizer.Optimize(easom_remote);  //vraca optimum
            easom_optimum.Wait();
            (x, fx, iterNum) = easom_optimum.Result;
            //store result
            var easom_result = new OptimizationResultDto(x, fx, easom_args.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", easom_remote.Uri},{"ProblemName", easom_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), "RandomSearch");
            monitoring = monitor.Save(easom_result);
            monitoring.Wait(); 
            Console.WriteLine($"easom: x = [{string.Join(", ", x)}], fx = {fx}");

            //gomez-levi
            var gomez_levi_optimum = gomez_levi_random_search_optimizer.Optimize(gomez_levi_remote);  //vraca optimum
            gomez_levi_optimum.Wait();
            (x, fx, iterNum) = gomez_levi_optimum.Result;
            //store result
            var gomez_levi_result = new OptimizationResultDto(x, fx, gomez_levi_args.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", gomez_levi_remote.Uri},{"ProblemName", gomez_levi_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), "RandomSearch");
            monitoring = monitor.Save(gomez_levi_result);
            monitoring.Wait(); 
            Console.WriteLine($"gomez-levi: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //mishras_bird
            var mishras_bird_optimum = mishras_bird_random_search_optimizer.Optimize(mishras_bird_remote);  //vraca optimum
            mishras_bird_optimum.Wait();
            (x, fx, iterNum) = mishras_bird_optimum.Result;
            //store result
            var mishras_bird_result = new OptimizationResultDto(x, fx, mishras_bird_args.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", mishras_bird_remote.Uri},{"ProblemName", mishras_bird_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), "RandomSearch");
            monitoring = monitor.Save(mishras_bird_result);
            monitoring.Wait(); 
            Console.WriteLine($" mishras: x = [{string.Join(", ", x)}], fx = {fx}"); 
*/
/*


            //py spherical
           /* var py_spherical_optimum = random_search_optimizer.Optimize(py_spherical_remote);  //vraca optimum
            py_spherical_optimum.Wait();
            var (x, fx, iterNum) = py_spherical_optimum.Result;
            Console.WriteLine($"py spherical: x = [{string.Join(", ", x)}], fx = {fx}");
            //store result
            var spherical_result = new OptimizationResultDto(x, fx, args.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", py_spherical_remote.Uri},{"ProblemName", py_spherical_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), "RandomSearch");
            var monitoring = monitor.Save(spherical_result);
            monitoring.Wait();*/

            PSOOptions options = new();
            options.NPart = 150;
            options.NIter = 300;

            
        
            
        } 
    }
}