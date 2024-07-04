using Implementations;
using Dtos;
using Utilities;

namespace HttpClientSample
{   
    class Program
    {  
        static readonly Implementations.Monitor monitor = new("http://localhost:5201/"); //Potrebno namespace zbog System.Threading.Monitor-a
        static readonly ParameterJsonGenerator generator = new();

        // ****************************************** DOTNET PROBLEMS *******************************************************
        static RemoteProblem? spherical_remote_dotnet;
        static RemoteProblem? rosenbrock_remote_dotnet;
        static RemoteProblem? rastrigin_remote_dotnet;
        static RemoteProblem? shekel_remote_dotnet;
        static RemoteProblem? matyas_remote_dotnet;
        static RemoteProblem? gomez_levi_remote_dotnet;
        static RemoteProblem? mishras_bird_remote_dotnet;
        static RemoteProblem? beale_remote_dotnet;
        static RemoteProblem? booth_remote_dotnet;


        // ****************************************** ARGUMENTTS FOR RANDOM SEARCH ********************************************
        static OptimizerArguments? spherical_random_search_args;
        static OptimizerArguments? rosenbrock_random_search_args;
        static OptimizerArguments? rastrigin_random_search_args;
        static OptimizerArguments? shekel_random_search_args;
        static OptimizerArguments? matyas_random_search_args;
        static OptimizerArguments? gomez_levi_random_search_args;
        static OptimizerArguments? mishras_bird_random_search_args;
        static OptimizerArguments? beale_random_search_args;
        static OptimizerArguments? booth_random_search_args;

        // ****************************************** ARGUMENTS FOR PSO *******************************************************
        static OptimizerArguments? spherical_pso_args;
        static OptimizerArguments? rosenbrock_pso_args;
        static OptimizerArguments? rastrigin_pso_args;
        static OptimizerArguments? shekel_pso_args;
        static OptimizerArguments? matyas_pso_args;
        static OptimizerArguments? gomez_levi_pso_args;
        static OptimizerArguments? mishras_bird_pso_args;
        static OptimizerArguments? beale_pso_args;
        static OptimizerArguments? booth_pso_args;
    
        // ****************************************** OPTIMIZERS RANDOM SEARCH ***********************************************
        static RandomSearchOptimizer? spherical_random_search_optimizer;
        static RandomSearchOptimizer? rosenbrock_random_search_optimizer;
        static RandomSearchOptimizer? rastrigin_random_search_optimizer;
        static RandomSearchOptimizer? shekel_random_search_optimizer;
        static RandomSearchOptimizer? matyas_random_search_optimizer;
        static RandomSearchOptimizer? gomez_levi_random_search_optimizer;
        static RandomSearchOptimizer? mishras_bird_random_search_optimizer;
        static RandomSearchOptimizer? beale_random_search_optimizer;
        static RandomSearchOptimizer? booth_random_search_optimizer;

        // ****************************************** OPTIMIZERS PSO *********************************************************
        static PSOOptimizer? spherical_pso_optimizer;
        static PSOOptimizer? rosenbrock_pso_optimizer;
        static PSOOptimizer? rastrigin_pso_optimizer;
        static PSOOptimizer? shekel_pso_optimizer;
        static PSOOptimizer? matyas_pso_optimizer;
        static PSOOptimizer? gomez_levi_pso_optimizer;
        static PSOOptimizer? mishras_bird_pso_optimizer;
        static PSOOptimizer? beale_pso_optimizer;
        static PSOOptimizer? booth_pso_optimizer;


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
            spherical_remote_dotnet = new RemoteProblem("http://localhost:5030", "Spherical");
            rosenbrock_remote_dotnet = new RemoteProblem("http://localhost:5030", "Rosenbrock");
            rastrigin_remote_dotnet = new RemoteProblem("http://localhost:5030", "Rastrigin");
            shekel_remote_dotnet = new RemoteProblem("http://localhost:5030", "Shekel");
            matyas_remote_dotnet = new RemoteProblem("http://localhost:5030", "Matyas");
            gomez_levi_remote_dotnet = new RemoteProblem("http://localhost:5030", "GomezLevi");
            mishras_bird_remote_dotnet = new RemoteProblem("http://localhost:5030", "MishrasBird");
            beale_remote_dotnet = new RemoteProblem("http://localhost:5030", "Beale");
            booth_remote_dotnet = new RemoteProblem("http://localhost:5030", "Booth");
            
        }

        private static void DefineArguments()
        {
            // ****************************************** ARGUMENTS FOR RANDOM SEARCH ********************************************

            spherical_random_search_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-1,-0.5}}, {"UpperBounds", new double[] {0.3,0.5} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 100 }},
            };
            rosenbrock_random_search_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {0,0}}, {"UpperBounds", new double[] {1.5,1.5} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1100 }},
            };
            rastrigin_random_search_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-1,-0.5}}, {"UpperBounds", new double[] {0.5,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1100 }},
            };
            shekel_random_search_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-1,-1}}, {"UpperBounds", new double[] {0.5,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1100 }},
            };
            matyas_random_search_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-10,-10}}, {"UpperBounds", new double[] {10,10} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1100 }},
            };
            gomez_levi_random_search_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-1,-1}}, {"UpperBounds", new double[] {0.75,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1100 }},
            };
            mishras_bird_random_search_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {-9,-2.5}}, {"UpperBounds", new double[] {-2,-0.5} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1100 }},
            };
            beale_random_search_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {2,0}}, {"UpperBounds", new double[] {4,1} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1100 }},
            };
            booth_random_search_args = new OptimizerArguments 
            {
                ArrayDoubleSpecs = new Dictionary<string, double[]>{{"LowerBounds", new double[] {0,2}}, {"UpperBounds", new double[] {2,4} }},
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 1100 }},
            };


            // ****************************************** ARGUMENTS FOR PSO ********************************************************

            spherical_pso_args = new OptimizerArguments
            {
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 50 }, {"NumParticles", 200}},
                DoubleSpecs = new Dictionary<string, double>{{"Cbi", 2.5}, {"Cbf", 0.5}, {"Cgi", 0.5}, {"Cgf", 2.5}, {"Wi", 0.9}, {"Wf", 0.4}, {"VSpanInit", 1}, {"InitOffset", 0}, {"InitSpan", 10}}            
            };
            rosenbrock_pso_args = new OptimizerArguments
            {
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 50 }, {"NumParticles", 200}},
                DoubleSpecs = new Dictionary<string, double>{{"Cbi", 2.5}, {"Cbf", 0.5}, {"Cgi", 0.5}, {"Cgf", 2.5}, {"Wi", 0.9}, {"Wf", 0.4}, {"VSpanInit", 1}, {"InitOffset", 0}, {"InitSpan", 10}}
            };
            rastrigin_pso_args = new OptimizerArguments
            {
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 50 }, {"NumParticles", 200}},
                DoubleSpecs = new Dictionary<string, double>{{"Cbi", 2.5}, {"Cbf", 0.5}, {"Cgi", 0.5}, {"Cgf", 2.5}, {"Wi", 0.9}, {"Wf", 0.4}, {"VSpanInit", 1}, {"InitOffset", 0}, {"InitSpan", 10}}     
            };
            shekel_pso_args = new OptimizerArguments
            {
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 50 }, {"NumParticles", 200}},
                DoubleSpecs = new Dictionary<string, double>{{"Cbi", 2.5}, {"Cbf", 0.5}, {"Cgi", 0.5}, {"Cgf", 2.5}, {"Wi", 0.9}, {"Wf", 0.4}, {"VSpanInit", 1}, {"InitOffset", 0}, {"InitSpan", 10}}     
            };
            matyas_pso_args = new OptimizerArguments
            {
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 50 }, {"NumParticles", 200}},
                DoubleSpecs = new Dictionary<string, double>{{"Cbi", 2.5}, {"Cbf", 0.5}, {"Cgi", 0.5}, {"Cgf", 2.5}, {"Wi", 0.9}, {"Wf", 0.4}, {"VSpanInit", 1}, {"InitOffset", 0}, {"InitSpan", 10}}     
            };
            gomez_levi_pso_args = new OptimizerArguments
            {
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 50 }, {"NumParticles", 200}},
                DoubleSpecs = new Dictionary<string, double>{{"Cbi", 2.5}, {"Cbf", 0.5}, {"Cgi", 0.5}, {"Cgf", 2.5}, {"Wi", 0.9}, {"Wf", 0.4}, {"VSpanInit", 1}, {"InitOffset", 0}, {"InitSpan", 10}}     
            };
            mishras_bird_pso_args = new OptimizerArguments
            {
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 50 }, {"NumParticles", 200}},
                DoubleSpecs = new Dictionary<string, double>{{"Cbi", 2.5}, {"Cbf", 0.5}, {"Cgi", 0.5}, {"Cgf", 2.5}, {"Wi", 0.9}, {"Wf", 0.4}, {"VSpanInit", 1}, {"InitOffset", 0}, {"InitSpan", 10}}     
            };
            beale_pso_args = new OptimizerArguments
            {
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 50 }, {"NumParticles", 200}},
                DoubleSpecs = new Dictionary<string, double>{{"Cbi", 2.5}, {"Cbf", 0.5}, {"Cgi", 0.5}, {"Cgf", 2.5}, {"Wi", 0.9}, {"Wf", 0.4}, {"VSpanInit", 1}, {"InitOffset", 0}, {"InitSpan", 10}}     
            };
            booth_pso_args = new OptimizerArguments
            {
                IntSpecs = new Dictionary<string, int>{{"Dimension", 2}, {"MaxIterations", 50 }, {"NumParticles", 200}},
                DoubleSpecs = new Dictionary<string, double>{{"Cbi", 2.5}, {"Cbf", 0.5}, {"Cgi", 0.5}, {"Cgf", 2.5}, {"Wi", 0.9}, {"Wf", 0.4}, {"VSpanInit", 1}, {"InitOffset", 0}, {"InitSpan", 10}}     
            };
            
        }

        private static void InstantiateOptimizers()
        {
            // ****************************************** INSTANTIATE RADNOM SEARCH OPTIMIZERS *************************************
            spherical_random_search_optimizer = new RandomSearchOptimizer(spherical_random_search_args!);
            rosenbrock_random_search_optimizer = new RandomSearchOptimizer(rosenbrock_random_search_args!);
            rastrigin_random_search_optimizer = new RandomSearchOptimizer(rastrigin_random_search_args!);
            shekel_random_search_optimizer = new RandomSearchOptimizer(shekel_random_search_args!);
            matyas_random_search_optimizer = new RandomSearchOptimizer(matyas_random_search_args!);
            gomez_levi_random_search_optimizer = new RandomSearchOptimizer(gomez_levi_random_search_args!);
            mishras_bird_random_search_optimizer = new RandomSearchOptimizer(mishras_bird_random_search_args!);
            beale_random_search_optimizer = new RandomSearchOptimizer(beale_random_search_args!);
            booth_random_search_optimizer = new RandomSearchOptimizer(booth_random_search_args!); 

            // ****************************************** INSTANTIATE PSO OPTIMIZERS ***********************************************
            spherical_pso_optimizer = new PSOOptimizer(spherical_pso_args!);
            rosenbrock_pso_optimizer = new PSOOptimizer(rosenbrock_pso_args!);
            rastrigin_pso_optimizer = new PSOOptimizer(rastrigin_pso_args!);  
            shekel_pso_optimizer = new PSOOptimizer(shekel_pso_args!); 
            matyas_pso_optimizer = new PSOOptimizer(matyas_pso_args!);  
            gomez_levi_pso_optimizer = new PSOOptimizer(gomez_levi_pso_args!);      
            mishras_bird_pso_optimizer = new PSOOptimizer(mishras_bird_pso_args!);      
            beale_pso_optimizer = new PSOOptimizer(beale_pso_args!);      
            booth_pso_optimizer = new PSOOptimizer(booth_pso_args!);      
    
        }

        private static void ExecuteOptimizers()
        {
            // ****************************************** EXECUTE RANDOM SEARCH ********************************************
            
            //spherical
            var spherical_optimum = spherical_random_search_optimizer!.Optimize(spherical_remote_dotnet!);  
            spherical_optimum.Wait();
            var (x, fx, iterNum) = spherical_optimum.Result;
            var spherical_result = new OptimizationResultDto(x, fx, spherical_random_search_args!.GenerateJson(), 
                                        generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", spherical_remote_dotnet!.Uri},
                                        {"ProblemName", spherical_remote_dotnet!.ProblemName}}), 
                                        generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), 
                                        spherical_random_search_optimizer.OptimizerName);   
            var monitoring = monitor.Save(spherical_result, spherical_remote_dotnet);
            monitoring.Wait(); 
            Console.WriteLine($"spherical-random-search: x = [{string.Join(", ", x)}], fx = {fx}");

            //rosenbrock
            var rosenbrock_optimum = rosenbrock_random_search_optimizer!.Optimize(rosenbrock_remote_dotnet!);  
            rosenbrock_optimum.Wait();
            (x, fx, iterNum) = rosenbrock_optimum.Result;
            var rosenbrock_result = new OptimizationResultDto(x, fx, rosenbrock_random_search_args!.GenerateJson(),
                                        generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", rosenbrock_remote_dotnet!.Uri},
                                        {"ProblemName", rosenbrock_remote_dotnet.ProblemName}}), 
                                        generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}),
                                        rosenbrock_random_search_optimizer.OptimizerName);
            monitoring = monitor.Save(rosenbrock_result, rosenbrock_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"rosenbrock-random-search: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //rastrigin
            var rastrigin_optimum = rastrigin_random_search_optimizer!.Optimize(rastrigin_remote_dotnet!);  
            rastrigin_optimum.Wait();
            (x, fx, iterNum) = rastrigin_optimum.Result;
            //store result
            var rastrigin_result = new OptimizationResultDto(x, fx, rastrigin_random_search_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", rastrigin_remote_dotnet!.Uri},{"ProblemName", rastrigin_remote_dotnet!.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), rastrigin_random_search_optimizer.OptimizerName);
            monitoring = monitor.Save(rastrigin_result, rastrigin_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"rastrigin-random-search: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //shekel
            var shekel_optimum = shekel_random_search_optimizer!.Optimize(shekel_remote_dotnet!);  
            shekel_optimum.Wait();
            (x, fx, iterNum) = shekel_optimum.Result;
            //store result
            var shekel_result = new OptimizationResultDto(x, fx, shekel_random_search_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", shekel_remote_dotnet!.Uri},{"ProblemName", shekel_remote_dotnet!.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), shekel_random_search_optimizer.OptimizerName);
            monitoring = monitor.Save(shekel_result, shekel_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"shekel-random-search: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //matyas
            var matyas_optimum = matyas_random_search_optimizer!.Optimize(matyas_remote_dotnet!);  
            matyas_optimum.Wait();
            (x, fx, iterNum) = matyas_optimum.Result;
            //store result
            var matyas_result = new OptimizationResultDto(x, fx, matyas_random_search_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", matyas_remote_dotnet!.Uri},{"ProblemName", matyas_remote_dotnet!.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), matyas_random_search_optimizer.OptimizerName);
            monitoring = monitor.Save(matyas_result, matyas_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"matyas-random-search: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //gomez-levi
            var gomez_levi_optimum = gomez_levi_random_search_optimizer!.Optimize(gomez_levi_remote_dotnet!);  
            gomez_levi_optimum.Wait();
            (x, fx, iterNum) = gomez_levi_optimum.Result;
            //store result
            var gomez_levi_result = new OptimizationResultDto(x, fx, gomez_levi_random_search_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", gomez_levi_remote_dotnet!.Uri},{"ProblemName", gomez_levi_remote_dotnet!.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), gomez_levi_random_search_optimizer.OptimizerName);
            monitoring = monitor.Save(gomez_levi_result, gomez_levi_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"gomez-levi-random-search: x = [{string.Join(", ", x)}], fx = {fx}"); 

            //mishras_bird
            var mishras_bird_optimum = mishras_bird_random_search_optimizer!.Optimize(mishras_bird_remote_dotnet!);  
            mishras_bird_optimum.Wait();
            (x, fx, iterNum) = mishras_bird_optimum.Result;
            //store result
            var mishras_bird_result = new OptimizationResultDto(x, fx, mishras_bird_random_search_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", mishras_bird_remote_dotnet!.Uri},{"ProblemName", mishras_bird_remote_dotnet.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), mishras_bird_random_search_optimizer.OptimizerName);
            monitoring = monitor.Save(mishras_bird_result, mishras_bird_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"mishras-random-search: x = [{string.Join(", ", x)}], fx = {fx}");

            //beale
            var beale_optimum = beale_random_search_optimizer!.Optimize(beale_remote_dotnet!);  
            beale_optimum.Wait();
            (x, fx, iterNum) = beale_optimum.Result;
            //store result
            var beale_result = new OptimizationResultDto(x, fx, beale_random_search_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", beale_remote_dotnet!.Uri},{"ProblemName", beale_remote_dotnet!.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), beale_random_search_optimizer.OptimizerName);   
            monitoring = monitor.Save(beale_result, beale_remote_dotnet);
            monitoring.Wait();
            Console.WriteLine($"beale-random-search: x = [{string.Join(", ", x)}], fx = {fx}");
 

            //booth
            var booth_optimum = booth_random_search_optimizer!.Optimize(booth_remote_dotnet!);  
            booth_optimum.Wait();
            (x, fx, iterNum) = booth_optimum.Result;
            //store result
            var booth_result = new OptimizationResultDto(x, fx, booth_random_search_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", booth_remote_dotnet!.Uri},{"ProblemName", booth_remote_dotnet!.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), booth_random_search_optimizer.OptimizerName);   
            monitoring = monitor.Save(booth_result, booth_remote_dotnet);
            monitoring.Wait(); 
            Console.WriteLine($"booth-random-search: x = [{string.Join(", ", x)}], fx = {fx}");


            // ****************************************** EXECUTE PSO OPTIMIZERS ******************************************************

            //spherical
            var spherical_pso_optimum = spherical_pso_optimizer!.Optimize(spherical_remote_dotnet!);
            spherical_pso_optimum.Wait();
            (x, fx, iterNum) = spherical_pso_optimum.Result;
            //store result
            var spherical_pso_result = new OptimizationResultDto(x, fx, spherical_pso_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", spherical_remote_dotnet!.Uri},{"ProblemName", spherical_remote_dotnet.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), spherical_pso_optimizer.OptimizerName);   
            monitoring = monitor.Save(spherical_pso_result, spherical_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"spherical-pso: x = [{string.Join(", ", x)}], fx = {fx}");

            //rosenbrock
            var rosenbrock_pso_optimum = rosenbrock_pso_optimizer!.Optimize(rosenbrock_remote_dotnet!);  
            rosenbrock_pso_optimum.Wait();
            (x, fx, iterNum) = rosenbrock_pso_optimum.Result;
            //store result
            var rosenbrock_pso_result = new OptimizationResultDto(x, fx, rosenbrock_pso_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", rosenbrock_remote_dotnet!.Uri},{"ProblemName", rosenbrock_remote_dotnet.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), rosenbrock_pso_optimizer.OptimizerName);   
            monitoring = monitor.Save(rosenbrock_pso_result, rosenbrock_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"rosenbrock-pso: x = [{string.Join(", ", x)}], fx = {fx}");

            //rastrigin
            var rastrigin_pso_optimum = rastrigin_pso_optimizer!.Optimize(rastrigin_remote_dotnet!);  
            rastrigin_pso_optimum.Wait();
            (x, fx, iterNum) = rastrigin_pso_optimum.Result;
            //store result
            var rastrigin_pso_result = new OptimizationResultDto(x, fx, rastrigin_pso_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", rastrigin_remote_dotnet!.Uri},{"ProblemName", rastrigin_remote_dotnet.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), rastrigin_pso_optimizer.OptimizerName);   
            monitoring = monitor.Save(rastrigin_pso_result, rastrigin_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"rastrigin-pso: x = [{string.Join(", ", x)}], fx = {fx}");            

            //shekel
            var shekel_pso_optimum = shekel_pso_optimizer!.Optimize(shekel_remote_dotnet!);  
            shekel_pso_optimum.Wait();
            (x, fx, iterNum) = shekel_pso_optimum.Result;
            //store result
            var shekel_pso_result = new OptimizationResultDto(x, fx, shekel_pso_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", shekel_remote_dotnet!.Uri},{"ProblemName", shekel_remote_dotnet.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), shekel_pso_optimizer.OptimizerName);   
            monitoring = monitor.Save(shekel_pso_result, shekel_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"shekel-pso: x = [{string.Join(", ", x)}], fx = {fx}");

            //matyas
            var matyas_pso_optimum = matyas_pso_optimizer!.Optimize(matyas_remote_dotnet!);  
            matyas_pso_optimum.Wait();
            (x, fx, iterNum) = matyas_pso_optimum.Result;
            //store result
            var matyas_pso_result = new OptimizationResultDto(x, fx, matyas_pso_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", matyas_remote_dotnet!.Uri},{"ProblemName", matyas_remote_dotnet.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), matyas_pso_optimizer.OptimizerName);   
            monitoring = monitor.Save(matyas_pso_result, matyas_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"matyas-pso: x = [{string.Join(", ", x)}], fx = {fx}");

            //gomez_levi
            var gomez_levi_pso_optimum = gomez_levi_pso_optimizer!.Optimize(gomez_levi_remote_dotnet!);  //vraca optimum
            gomez_levi_pso_optimum.Wait();
            (x, fx, iterNum) = gomez_levi_pso_optimum.Result;
            //store result
            var gomez_levi_pso_result = new OptimizationResultDto(x, fx, gomez_levi_pso_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", gomez_levi_remote_dotnet!.Uri},{"ProblemName", gomez_levi_remote_dotnet!.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), gomez_levi_pso_optimizer.OptimizerName);   
            monitoring = monitor.Save(gomez_levi_pso_result, gomez_levi_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"gomez-levi-pso: x = [{string.Join(", ", x)}], fx = {fx}");

            //mishras_bird
            var mishras_bird_pso_optimum = mishras_bird_pso_optimizer!.Optimize(mishras_bird_remote_dotnet!);  
            mishras_bird_pso_optimum.Wait();
            (x, fx, iterNum) = mishras_bird_pso_optimum.Result;
            //store result
            var mishras_bird_pso_result = new OptimizationResultDto(x, fx, mishras_bird_pso_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", mishras_bird_remote_dotnet!.Uri},{"ProblemName", mishras_bird_remote_dotnet.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), mishras_bird_pso_optimizer.OptimizerName);   
            monitoring = monitor.Save(mishras_bird_pso_result, mishras_bird_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"mishras-bird-pso: x = [{string.Join(", ", x)}], fx = {fx}");

            //beale
            var beale_pso_optimum = beale_pso_optimizer!.Optimize(beale_remote_dotnet!);  
            beale_pso_optimum.Wait();
            (x, fx, iterNum) = beale_pso_optimum.Result;
            //store result
            var beale_pso_result = new OptimizationResultDto(x, fx, beale_pso_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", beale_remote_dotnet!.Uri},{"ProblemName", beale_remote_dotnet.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), beale_pso_optimizer.OptimizerName);   
            monitoring = monitor.Save(beale_pso_result, beale_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"beale-pso: x = [{string.Join(", ", x)}], fx = {fx}");

            //booth
            var booth_pso_optimum = booth_pso_optimizer!.Optimize(booth_remote_dotnet!);  
            booth_pso_optimum.Wait();
            (x, fx, iterNum) = booth_pso_optimum.Result;
            //store result
            var booth_pso_result = new OptimizationResultDto(x, fx, booth_pso_args!.GenerateJson(), generator.GenerateJson(new Dictionary<string, object>{{"ProblemUri", booth_remote_dotnet!.Uri},{"ProblemName", booth_remote_dotnet.ProblemName}}), generator.GenerateJson(new Dictionary<string, object>{{"Count", iterNum}}), booth_pso_optimizer.OptimizerName);   
            monitoring = monitor.Save(booth_pso_result, booth_remote_dotnet!);
            monitoring.Wait(); 
            Console.WriteLine($"booth-pso: x = [{string.Join(", ", x)}], fx = {fx}");



        }
    }
}