using Implementations;
using Dtos;
using Utilities;

namespace HttpClientSample
{   
    class Program
    {  
        static readonly Implementations.Monitor monitor = new("http://localhost:5201/");//Zahtjeva namespace zbog System.Threading.Monitor-a
        static readonly ParameterJsonGenerator generator = new();

        static RemoteProblem? spherical_remote;
        static RemoteProblem? rosenbrock_remote;
        static RemoteProblem? rastrigin_remote;
        static RemoteProblem? shekel_remote;
        static RemoteProblem? matyas_remote;
        static RemoteProblem? easom_remote;
        static RemoteProblem? gomez_levi_remote;
        static RemoteProblem? mishras_bird_remote;
        static RemoteProblem? py_spherical_remote;
        static OptimizerArguments? spherical_args;
        static OptimizerArguments? rosenbrock_args;
        static OptimizerArguments? rastrigin_args;
        static OptimizerArguments? shekel_args;
        static OptimizerArguments? matyas_args;
        static OptimizerArguments? easom_args;
        static OptimizerArguments? gomez_levi_args;
        static OptimizerArguments? mishras_bird_args;
        static OptimizerArguments? matyas_pso_args;
        
        static OptimizerArguments? spherical_pso_args;
        static RandomSearchOptimizer? spherical_random_search_optimizer;
        static RandomSearchOptimizer? rosenbrock_random_search_optimizer;
        static RandomSearchOptimizer? rastrigin_random_search_optimizer;
        static RandomSearchOptimizer? shekel_random_search_optimizer;
        static RandomSearchOptimizer? matyas_random_search_optimizer;
        static RandomSearchOptimizer? easom_random_search_optimizer;
        static RandomSearchOptimizer? gomez_levi_random_search_optimizer;
        static RandomSearchOptimizer? mishras_bird_random_search_optimizer;


        static PSOOptimizer? spherical_pso_optimizer;

        static void Main()
        {
            
            InstantiateProblems();
            DefineArguments();
            InstantiateOptimizers();
            ExecuteOptimizers();
            

            

            
        
            
        } 

        private static void InstantiateProblems()
        {
            var problem_local = new LocalProblem();
            spherical_remote = new RemoteProblem("http://localhost:5030", "Spherical");
            rosenbrock_remote = new RemoteProblem("http://localhost:5030", "Rosenbrock");
            rastrigin_remote = new RemoteProblem("http://localhost:5030", "Rastrigin");
            shekel_remote = new RemoteProblem("http://localhost:5030", "Shekel");
            matyas_remote = new RemoteProblem("http://localhost:5030", "Matyas");
            easom_remote = new RemoteProblem("http://localhost:5030", "Easom");
            gomez_levi_remote = new RemoteProblem("http://localhost:5030", "GomezLevi");
            mishras_bird_remote = new RemoteProblem("http://localhost:5030", "MishrasBird");
            py_spherical_remote = new RemoteProblem("http://localhost:5055", "Spherical");
            
        }

        private static void DefineArguments()
        {
            spherical_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-1,-1}}, {"UpperBounds", new double[] {1,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };
            rosenbrock_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {0,0}}, {"UpperBounds", new double[] {2,2} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };
            rastrigin_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-1,-1}}, {"UpperBounds", new double[] {1,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };
            shekel_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-1,-1}}, {"UpperBounds", new double[] {1,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };
            matyas_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-1,-1}}, {"UpperBounds", new double[] {1,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };
            easom_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {2,2}}, {"UpperBounds", new double[] {4,4} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };
            gomez_levi_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-1,-1}}, {"UpperBounds", new double[] {0.75,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };
            mishras_bird_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-4,-2.5}}, {"UpperBounds", new double[] {-2,-0.5} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }},
            };

            matyas_pso_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"NumDim", new double[] {-4,-2.5}}, {"UpperBounds", new double[] {-2,-0.5} }},
                IntSpecs = new Dictionary<string, int>{{"NumDimensions", 2}},
            };


            //za pso
            spherical_pso_args = new OptimizerArguments 
            {
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1000 }, {"NumParticles", 200}},
                DoubleSpecs = new Dictionary<string, double>{{"Cbi", 2.5}, {"Cbf", 0.5}, {"Cgi", 0.5}, {"Cgf", 2.5}, {"Wi", 0.9}, {"Wf", 0.4}, {"VSpanInit", 1}, {"InitOffset", 0}, {"InitSpan", 1}}
            };

        }

        private static void InstantiateOptimizers()
        {
            spherical_random_search_optimizer = new RandomSearchOptimizer(spherical_args!);
            rosenbrock_random_search_optimizer = new RandomSearchOptimizer(rosenbrock_args!);
            rastrigin_random_search_optimizer = new RandomSearchOptimizer(rastrigin_args!);
            shekel_random_search_optimizer = new RandomSearchOptimizer(shekel_args!);
            matyas_random_search_optimizer = new RandomSearchOptimizer(matyas_args!);
            easom_random_search_optimizer = new RandomSearchOptimizer(easom_args!);
            gomez_levi_random_search_optimizer = new RandomSearchOptimizer(gomez_levi_args!);
            mishras_bird_random_search_optimizer = new RandomSearchOptimizer(mishras_bird_args!); 

            spherical_pso_optimizer = new PSOOptimizer(spherical_pso_args!);      
        }

        private static void ExecuteOptimizers()
        {
             //spherical
            var spherical_optimum = spherical_random_search_optimizer!.Optimize(spherical_remote!);  //vraca optimum
            spherical_optimum.Wait();
            var (x, fx, iterNum) = spherical_optimum.Result;
            Console.WriteLine($"spherical: x = [{string.Join(", ", x)}], fx = {fx}");
            //store result
            var spherical_result = new OptimizationResultDto(x, fx, spherical_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", spherical_remote!.Uri},{"ProblemName", spherical_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), spherical_random_search_optimizer.OptimizerName);   
            var monitoring = monitor.Save(spherical_result);
            monitoring.Wait(); 

            //spherical
            var spherical_pso_optimum = spherical_pso_optimizer!.Optimize(spherical_remote!);  //vraca optimum
            spherical_pso_optimum.Wait();
            (x, fx, iterNum) = spherical_pso_optimum.Result;
            Console.WriteLine($"spherical pso: x = [{string.Join(", ", x)}], fx = {fx}");
            //store result
            var spherical_pso_result = new OptimizationResultDto(x, fx, spherical_pso_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", spherical_remote!.Uri},{"ProblemName", spherical_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), spherical_pso_optimizer.OptimizerName);   
            monitoring = monitor.Save(spherical_pso_result);
            monitoring.Wait(); 
            

            //rosenbrock
          /*  var rosenbrock_optimum = rosenbrock_random_search_optimizer!.Optimize(rosenbrock_remote!);  //vraca optimum
            rosenbrock_optimum.Wait();
            (x, fx, iterNum) = rosenbrock_optimum.Result;
            //store result
            var rosenbrock_result = new OptimizationResultDto(x, fx, rosenbrock_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", rosenbrock_remote!.Uri},{"ProblemName", rosenbrock_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), spherical_random_search_optimizer.OptimizerName);
            monitoring = monitor.Save(rosenbrock_result);
            monitoring.Wait(); 
            Console.WriteLine($"rosenbrock: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //rastrigin
            var rastrigin_optimum = rastrigin_random_search_optimizer!.Optimize(rastrigin_remote!);  //vraca optimum
            rastrigin_optimum.Wait();
            (x, fx, iterNum) = rastrigin_optimum.Result;
            //store result
            var rastrigin_result = new OptimizationResultDto(x, fx, rastrigin_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", rastrigin_remote!.Uri},{"ProblemName", rastrigin_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), spherical_random_search_optimizer.OptimizerName);
            monitoring = monitor.Save(rastrigin_result);
            monitoring.Wait(); 
            Console.WriteLine($"rastrigin: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //shekel
            var shekel_optimum = shekel_random_search_optimizer!.Optimize(shekel_remote!);  //vraca optimum
            shekel_optimum.Wait();
            (x, fx, iterNum) = shekel_optimum.Result;
            //store result
            var shekel_result = new OptimizationResultDto(x, fx, shekel_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", shekel_remote!.Uri},{"ProblemName", shekel_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), spherical_random_search_optimizer.OptimizerName);
            monitoring = monitor.Save(shekel_result);
            monitoring.Wait(); 
            Console.WriteLine($"shekel: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //matyas
            var matyas_optimum = matyas_random_search_optimizer!.Optimize(matyas_remote!);  //vraca optimum
            matyas_optimum.Wait();
            (x, fx, iterNum) = matyas_optimum.Result;
            //store result
            var matyas_result = new OptimizationResultDto(x, fx, matyas_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", matyas_remote!.Uri},{"ProblemName", matyas_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), spherical_random_search_optimizer.OptimizerName);
            monitoring = monitor.Save(matyas_result);
            monitoring.Wait(); 
            Console.WriteLine($"matyas: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //easom
            var easom_optimum = easom_random_search_optimizer!.Optimize(easom_remote!);  //vraca optimum
            easom_optimum.Wait();
            (x, fx, iterNum) = easom_optimum.Result;
            //store result
            var easom_result = new OptimizationResultDto(x, fx, easom_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", easom_remote!.Uri},{"ProblemName", easom_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), spherical_random_search_optimizer.OptimizerName);
            monitoring = monitor.Save(easom_result);
            monitoring.Wait(); 
            Console.WriteLine($"easom: x = [{string.Join(", ", x)}], fx = {fx}");

            //gomez-levi
            var gomez_levi_optimum = gomez_levi_random_search_optimizer!.Optimize(gomez_levi_remote!);  //vraca optimum
            gomez_levi_optimum.Wait();
            (x, fx, iterNum) = gomez_levi_optimum.Result;
            //store result
            var gomez_levi_result = new OptimizationResultDto(x, fx, gomez_levi_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", gomez_levi_remote!.Uri},{"ProblemName", gomez_levi_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), spherical_random_search_optimizer.OptimizerName);
            monitoring = monitor.Save(gomez_levi_result);
            monitoring.Wait(); 
            Console.WriteLine($"gomez-levi: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //mishras_bird
            var mishras_bird_optimum = mishras_bird_random_search_optimizer!.Optimize(mishras_bird_remote!);  //vraca optimum
            mishras_bird_optimum.Wait();
            (x, fx, iterNum) = mishras_bird_optimum.Result;
            //store result
            var mishras_bird_result = new OptimizationResultDto(x, fx, mishras_bird_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", mishras_bird_remote!.Uri},{"ProblemName", mishras_bird_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), spherical_random_search_optimizer.OptimizerName);
            monitoring = monitor.Save(mishras_bird_result);
            monitoring.Wait(); 
            Console.WriteLine($"mishras: x = [{string.Join(", ", x)}], fx = {fx}");

            */

            /* ovdje optimizer preimenovati i optimizername
            //py spherical
           /* var py_spherical_optimum = random_search_optimizer.Optimize(py_spherical_remote);  //vraca optimum
            py_spherical_optimum.Wait();
            var (x, fx, iterNum) = py_spherical_optimum.Result;
            Console.WriteLine($"py spherical: x = [{string.Join(", ", x)}], fx = {fx}");
            //store result
            var spherical_result = new OptimizationResultDto(x, fx, args.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", py_spherical_remote.Uri},{"ProblemName", py_spherical_remote.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), spherical_random_search_optimizer.OptimizerName);
            var monitoring = monitor.Save(spherical_result);
            monitoring.Wait();*/
        }
    }
}