import asyncio
import requests
import json
from Dtos.OptimizationResultDto import OptimizationResultDto
from Implementations.Monitor import Monitor
from Implementations.RandomSearchOptimizer import RandomSearchOptimizer
from Implementations.RemoteProblem import RemoteProblem
from Implementations.PsoOptimizer import PSOOptimizer
from Utilities.OptimizerArguments import OptimizerArguments
from Utilities.ParameterJsonGenerator import ParameterJsonGenerator


class Program:
    def main(self):
        monitor = Monitor("http://localhost:5201/")
        generator = ParameterJsonGenerator()

        # ****************************************** PYTHON PROBLEMS *******************************************************
        spherical_remote_py = RemoteProblem("http://localhost:5055", "Spherical")
        easom_remote_py = RemoteProblem("http://localhost:5055", "Easom")
        beale_remote_py = RemoteProblem("http://localhost:5055", "Beale")
        
       
        # ****************************************** ARGUMENTS FOR RANDOM SEARCH *******************************************************
        spherical_random_search_args = OptimizerArguments(
            int_specs={"Dimension": 2, "MaxIterations": 10},
            array_double_specs={"LowerBounds": [-1, -1], "UpperBounds": [1, 1]}
        )
        easom_random_search_args = OptimizerArguments(
            int_specs={"Dimension": 2, "MaxIterations": 5},
            array_double_specs={"LowerBounds": [3, 3], "UpperBounds": [4, 4]}
        )
        beale_random_search_args = OptimizerArguments(
            int_specs={"Dimension": 2, "MaxIterations": 5},
            array_double_specs={"LowerBounds": [2, 0], "UpperBounds": [4, 1]}
        )

        # ****************************************** ARGUMENTS FOR PSO *******************************************************
        spherical_pso_args = OptimizerArguments(
            int_specs={"Dimension": 2, "MaxIterations": 10, "NumParticles": 10},
            double_specs={"Cbi": 2.5, "Cbf": 0.5, "Cgi": 0.5, "Cgf": 2.5, "Wi": 0.9, "Wf": 0.4, "VSpanInit": 1, "InitOffset": 0, "InitSpan": 10 }
        )
        easom_pso_args = OptimizerArguments(
            int_specs={"Dimension": 2, "MaxIterations": 5, "NumParticles": 3},
            double_specs={"Cbi": 2.5, "Cbf": 0.5, "Cgi": 0.5, "Cgf": 2.5, "Wi": 0.9, "Wf": 0.4, "VSpanInit": 1, "InitOffset": 0, "InitSpan": 10 }
        )
        beale_pso_args = OptimizerArguments(
            int_specs={"Dimension": 2, "MaxIterations": 5, "NumParticles": 3},
            double_specs={"Cbi": 2.5, "Cbf": 0.5, "Cgi": 0.5, "Cgf": 2.5, "Wi": 0.9, "Wf": 0.4, "VSpanInit": 1, "InitOffset": 0, "InitSpan": 10 }
        )
        


        # ****************************************** OPTIMIZERS RANDOM SEARCH ***********************************************
        spherical_random_search_optimizer = RandomSearchOptimizer(spherical_random_search_args)
        easom_random_search_optimizer = RandomSearchOptimizer(easom_random_search_args)
        beale_random_search_optimizer = RandomSearchOptimizer(beale_random_search_args)
        

        # ****************************************** OPTIMIZERS PSO ***********************************************
        spherical_pso_optimizer = PSOOptimizer(spherical_pso_args)
        easom_pso_optimizer = PSOOptimizer(easom_pso_args)
        beale_pso_optimizer = PSOOptimizer(beale_pso_args)

        
        # ****************************************** EXECUTE RANDOM SEARCH ********************************************

        #spherical
        loop = asyncio.get_event_loop()
        spherical_optimum_task = asyncio.ensure_future(spherical_random_search_optimizer.optimize(spherical_remote_py))
        loop.run_until_complete(spherical_optimum_task)
        spherical_optimum = spherical_optimum_task.result()
        (x, fx, iterNum) = spherical_optimum
        print(f"spherical-random-search: x = [{', '.join(map(str, x))}], fx = {fx}")
        spherical_result = OptimizationResultDto(x, fx, spherical_random_search_args.generate_json(), generator.generate_json({"ProblemUri":spherical_remote_py.uri, "ProblemName":spherical_remote_py.problem_name}), generator.generate_json({"Count":iterNum}), spherical_random_search_optimizer.optimizer_name)
        loop = asyncio.get_event_loop()
        monitor_task = asyncio.ensure_future(monitor.save(spherical_result, spherical_remote_py))
        loop.run_until_complete(monitor_task)
        #spherical_optimum = spherical_optimum_task.result()

        #easom
        loop = asyncio.get_event_loop()
        easom_optimum_task = asyncio.ensure_future(easom_random_search_optimizer.optimize(easom_remote_py))
        loop.run_until_complete(easom_optimum_task)
        easom_optimum = easom_optimum_task.result()
        (x, fx, iterNum) = easom_optimum
        print(f"easom-random-search: x = [{', '.join(map(str, x))}], fx = {fx}")
        easom_result = OptimizationResultDto(x, fx, easom_random_search_args.generate_json(), generator.generate_json({"ProblemUri":easom_remote_py.uri, "ProblemName":easom_remote_py.problem_name}), generator.generate_json({"Count":iterNum}), easom_random_search_optimizer.optimizer_name)
        loop = asyncio.get_event_loop()
        monitor_task = asyncio.ensure_future(monitor.save(easom_result, easom_remote_py))
        loop.run_until_complete(monitor_task)

        #beale
        loop = asyncio.get_event_loop()
        beale_optimum_task = asyncio.ensure_future(beale_random_search_optimizer.optimize(beale_remote_py))
        loop.run_until_complete(beale_optimum_task)
        beale_optimum = beale_optimum_task.result()
        #treba li onda wait
        (x, fx, iterNum) = beale_optimum
        print(f"beale-random-search: x = [{', '.join(map(str, x))}], fx = {fx}")
        beale_result = OptimizationResultDto(x, fx, beale_random_search_args.generate_json(), generator.generate_json({"ProblemUri":beale_remote_py.uri, "ProblemName":beale_remote_py.problem_name}), generator.generate_json({"Count":iterNum}), beale_random_search_optimizer.optimizer_name)
        loop = asyncio.get_event_loop()
        monitor_task = asyncio.ensure_future(monitor.save(beale_result, beale_remote_py))
        loop.run_until_complete(monitor_task)


        # ****************************************** EXECUTE PSO ********************************************
        
        #spherical
        loop = asyncio.get_event_loop()
        spherical_optimum_task = asyncio.ensure_future(spherical_pso_optimizer.optimize(spherical_remote_py))
        loop.run_until_complete(spherical_optimum_task)
        spherical_optimum = spherical_optimum_task.result()
        (x, fx, iterNum) = spherical_optimum
        print(f"spherical-pso: x = [{', '.join(map(str, x))}], fx = {fx}")
        spherical_result = OptimizationResultDto(x, fx, spherical_pso_args.generate_json(), 
                                                generator.generate_json({"ProblemUri":spherical_remote_py.uri, "ProblemName":spherical_remote_py.problem_name}), 
                                                generator.generate_json({"Count":iterNum}), spherical_pso_optimizer.optimizer_name)
        loop = asyncio.get_event_loop()
        monitor_task = asyncio.ensure_future(monitor.save(spherical_result, spherical_remote_py))
        loop.run_until_complete(monitor_task)

        #easom
        loop = asyncio.get_event_loop()
        easom_optimum_task = asyncio.ensure_future(easom_pso_optimizer.optimize(easom_remote_py))
        loop.run_until_complete(easom_optimum_task)
        easom_optimum = easom_optimum_task.result()
        (x, fx, iterNum) = easom_optimum
        print(f"easom-pso: x = [{', '.join(map(str, x))}], fx = {fx}")
        easom_result = OptimizationResultDto(x, fx, easom_pso_args.generate_json(), 
                                            generator.generate_json({"ProblemUri":easom_remote_py.uri, "ProblemName":easom_remote_py.problem_name}), 
                                            generator.generate_json({"Count":iterNum}), easom_pso_optimizer.optimizer_name)
        loop = asyncio.get_event_loop()
        monitor_task = asyncio.ensure_future(monitor.save(easom_result, easom_remote_py))
        loop.run_until_complete(monitor_task)

        #beale
        loop = asyncio.get_event_loop()
        beale_optimum_task = asyncio.ensure_future(beale_pso_optimizer.optimize(beale_remote_py))
        loop.run_until_complete(beale_optimum_task)
        beale_optimum = beale_optimum_task.result()
        #treba li onda wait
        (x, fx, iterNum) = beale_optimum
        print(f"beale-pso: x = [{', '.join(map(str, x))}], fx = {fx}")
        beale_result = OptimizationResultDto(x, fx, beale_pso_args.generate_json(), generator.generate_json({"ProblemUri":beale_remote_py.uri, "ProblemName":beale_remote_py.problem_name}), generator.generate_json({"Count":iterNum}), beale_pso_optimizer.optimizer_name)
        loop = asyncio.get_event_loop()
        monitor_task = asyncio.ensure_future(monitor.save(beale_result, beale_remote_py))
        loop.run_until_complete(monitor_task)

        

if __name__ == "__main__":
    program = Program()
    program.main()
