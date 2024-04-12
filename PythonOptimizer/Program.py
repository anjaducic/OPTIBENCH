import asyncio
import requests
import json

from Dtos.OptimizationResultDto import OptimizationResultDto
from Implementations.Monitor import Monitor
from Implementations.RandomSearchOptimizer import RandomSearchOptimizer
from Implementations.RemoteProblem import RemoteProblem
from Utilities.OptimizerArguments import OptimizerArguments
from Utilities.ParameterJsonGenerator import ParameterJsonGenerator


class Program:
    def main(self):
        #problem_local = LocalProblem()

        spherical_remote = RemoteProblem("http://localhost:5030", "Spherical")
        rosenbrock_remote = RemoteProblem("http://localhost:5030", "Rosenbrock")
        rastrigin_remote = RemoteProblem("http://localhost:5030", "Rastrigin")
        shekel_remote = RemoteProblem("http://localhost:5030", "Shekel")
        matyas_remote = RemoteProblem("http://localhost:5030", "Matyas")
        easom_remote = RemoteProblem("http://localhost:5030", "Easom")
        gomez_levi_remote = RemoteProblem("http://localhost:5030", "GomezLevi")
        mishras_bird_remote = RemoteProblem("http://localhost:5030", "MishrasBird")

        #py_spherical_remote = RemoteProblem("http://localhost:5055", "Spherical")

        
        
        spherical_args = OptimizerArguments(
            int_specs={"Dimension": 2, "MaxIterations": 1000},
            array_double_specs={"LowerBounds": [-1, -1], "UpperBounds": [1, 1]}
        )
        rosenbrock_args = OptimizerArguments(
            int_specs={"Dimension": 2, "MaxIterations": 1000},
            array_double_specs={"LowerBounds": [0, 0], "UpperBounds": [2, 2]}
        )
        rastrigin_args = OptimizerArguments(
            int_specs={"Dimension": 2, "MaxIterations": 1000},
            array_double_specs={"LowerBounds": [-1, -1], "UpperBounds": [1, 1]}
        )
        shekel_args = OptimizerArguments(
            int_specs={"Dimension": 2, "MaxIterations": 1000},
            array_double_specs={"LowerBounds": [-1, -1], "UpperBounds": [1, 1]}
        )
        matyas_args = OptimizerArguments(
            int_specs={"Dimension": 2, "MaxIterations": 1000},
            array_double_specs={"LowerBounds": [-1, -1], "UpperBounds": [1, 1]}
        )
        easom_args = OptimizerArguments(
            int_specs={"Dimension": 2, "MaxIterations": 1000},
            array_double_specs={"LowerBounds": [2, 2], "UpperBounds": [4, 4]}
        )
        gomez_levi_args = OptimizerArguments(
            int_specs={"Dimension": 2, "MaxIterations": 1000},
            array_double_specs={"LowerBounds": [-1, -1], "UpperBounds": [0.75, 1]}
        )
        mishras_bird_args = OptimizerArguments(
            int_specs={"Dimension": 2, "MaxIterations": 1000},
            array_double_specs={"LowerBounds": [-4, -2.5], "UpperBounds": [-2, -0.5]}
        )



        # Ostali argumenti za probleme...

        monitor = Monitor("http://localhost:5201/")
        spherical_random_search_optimizer = RandomSearchOptimizer(spherical_args)
        rosenbrock_random_search_optimizer = RandomSearchOptimizer(rosenbrock_args)
        rastrigin_random_search_optimizer = RandomSearchOptimizer(rastrigin_args)
        shekel_random_search_optimizer = RandomSearchOptimizer(shekel_args)
        matyas_random_search_optimizer = RandomSearchOptimizer(matyas_args)
        easom_random_search_optimizer = RandomSearchOptimizer(easom_args)
        gomez_levi_random_search_optimizer = RandomSearchOptimizer(gomez_levi_args)
        mishras_bird_random_search_optimizer = RandomSearchOptimizer(mishras_bird_args)


        # Ostali optimizatori...

        generator = ParameterJsonGenerator()

        #spherical
        loop = asyncio.get_event_loop()
        spherical_optimum_task = asyncio.ensure_future(spherical_random_search_optimizer.optimize(spherical_remote))
        loop.run_until_complete(spherical_optimum_task)
        spherical_optimum = spherical_optimum_task.result()
        #treba li onda wait
        (x, fx, iterNum) = spherical_optimum
        print(f"spherical: x = [{', '.join(map(str, x))}], fx = {fx}")
        spherical_result = OptimizationResultDto(x, fx, spherical_args.generate_json(), generator.generate_json({"ProblemUri":spherical_remote.uri, "ProblemName":spherical_remote.problem_name}), generator.generate_json({"Count":iterNum}), "RandomSearch")
        loop = asyncio.get_event_loop()
        monitor_task = asyncio.ensure_future(monitor.save(spherical_result))
        loop.run_until_complete(monitor_task)
        #spherical_optimum = spherical_optimum_task.result()

        #rosenbrock
        loop = asyncio.get_event_loop()
        rosenbrock_optimum_task = asyncio.ensure_future(rosenbrock_random_search_optimizer.optimize(rosenbrock_remote))
        loop.run_until_complete(rosenbrock_optimum_task)
        rosenbrock_optimum = rosenbrock_optimum_task.result()
        #treba li onda wait
        (x, fx, iterNum) = rosenbrock_optimum
        print(f"rosenbrock: x = [{', '.join(map(str, x))}], fx = {fx}")
        rosenbrock_result = OptimizationResultDto(x, fx, rosenbrock_args.generate_json(), generator.generate_json({"ProblemUri":rosenbrock_remote.uri, "ProblemName":rosenbrock_remote.problem_name}), generator.generate_json({"Count":iterNum}), "RandomSearch")
        loop = asyncio.get_event_loop()
        monitor_task = asyncio.ensure_future(monitor.save(rosenbrock_result))
        loop.run_until_complete(monitor_task)

        #rastrigin
        loop = asyncio.get_event_loop()
        rastrigin_optimum_task = asyncio.ensure_future(rastrigin_random_search_optimizer.optimize(rastrigin_remote))
        loop.run_until_complete(rastrigin_optimum_task)
        rastrigin_optimum = rastrigin_optimum_task.result()
        #treba li onda wait
        (x, fx, iterNum) = rastrigin_optimum
        print(f"rastrigin: x = [{', '.join(map(str, x))}], fx = {fx}")
        rastrigin_result = OptimizationResultDto(x, fx, rastrigin_args.generate_json(), generator.generate_json({"ProblemUri":rastrigin_remote.uri, "ProblemName":rastrigin_remote.problem_name}), generator.generate_json({"Count":iterNum}), "RandomSearch")
        loop = asyncio.get_event_loop()
        monitor_task = asyncio.ensure_future(monitor.save(rastrigin_result))
        loop.run_until_complete(monitor_task)


        #shekel
        loop = asyncio.get_event_loop()
        shekel_optimum_task = asyncio.ensure_future(shekel_random_search_optimizer.optimize(shekel_remote))
        loop.run_until_complete(shekel_optimum_task)
        shekel_optimum = shekel_optimum_task.result()
        #treba li onda wait
        (x, fx, iterNum) = shekel_optimum
        print(f"shekel: x = [{', '.join(map(str, x))}], fx = {fx}")
        shekel_result = OptimizationResultDto(x, fx, shekel_args.generate_json(), generator.generate_json({"ProblemUri":shekel_remote.uri, "ProblemName":shekel_remote.problem_name}), generator.generate_json({"Count":iterNum}), "RandomSearch")
        loop = asyncio.get_event_loop()
        monitor_task = asyncio.ensure_future(monitor.save(shekel_result))
        loop.run_until_complete(monitor_task)

        #matyas
        loop = asyncio.get_event_loop()
        matyas_optimum_task = asyncio.ensure_future(matyas_random_search_optimizer.optimize(matyas_remote))
        loop.run_until_complete(matyas_optimum_task)
        matyas_optimum = matyas_optimum_task.result()
        #treba li onda wait
        (x, fx, iterNum) = matyas_optimum
        print(f"matyas: x = [{', '.join(map(str, x))}], fx = {fx}")
        matyas_result = OptimizationResultDto(x, fx, matyas_args.generate_json(), generator.generate_json({"ProblemUri":matyas_remote.uri, "ProblemName":matyas_remote.problem_name}), generator.generate_json({"Count":iterNum}), "RandomSearch")
        loop = asyncio.get_event_loop()
        monitor_task = asyncio.ensure_future(monitor.save(matyas_result))
        loop.run_until_complete(monitor_task)

        #easom
        loop = asyncio.get_event_loop()
        easom_optimum_task = asyncio.ensure_future(easom_random_search_optimizer.optimize(easom_remote))
        loop.run_until_complete(easom_optimum_task)
        easom_optimum = easom_optimum_task.result()
        #treba li onda wait
        (x, fx, iterNum) = easom_optimum
        print(f"easom: x = [{', '.join(map(str, x))}], fx = {fx}")
        easom_result = OptimizationResultDto(x, fx, easom_args.generate_json(), generator.generate_json({"ProblemUri":easom_remote.uri, "ProblemName":easom_remote.problem_name}), generator.generate_json({"Count":iterNum}), "RandomSearch")
        loop = asyncio.get_event_loop()
        monitor_task = asyncio.ensure_future(monitor.save(easom_result))
        loop.run_until_complete(monitor_task)

        #gomez_levi
        loop = asyncio.get_event_loop()
        gomez_levi_optimum_task = asyncio.ensure_future(gomez_levi_random_search_optimizer.optimize(gomez_levi_remote))
        loop.run_until_complete(gomez_levi_optimum_task)
        gomez_levi_optimum = gomez_levi_optimum_task.result()
        #treba li onda wait
        (x, fx, iterNum) = gomez_levi_optimum
        print(f"gomez_levi: x = [{', '.join(map(str, x))}], fx = {fx}")
        gomez_levi_result = OptimizationResultDto(x, fx, gomez_levi_args.generate_json(), generator.generate_json({"ProblemUri":gomez_levi_remote.uri, "ProblemName":gomez_levi_remote.problem_name}), generator.generate_json({"Count":iterNum}), "RandomSearch")
        loop = asyncio.get_event_loop()
        monitor_task = asyncio.ensure_future(monitor.save(gomez_levi_result))
        loop.run_until_complete(monitor_task)

        #mishras_bird
        loop = asyncio.get_event_loop()
        mishras_bird_optimum_task = asyncio.ensure_future(mishras_bird_random_search_optimizer.optimize(mishras_bird_remote))
        loop.run_until_complete(mishras_bird_optimum_task)
        mishras_bird_optimum = mishras_bird_optimum_task.result()
        #treba li onda wait
        (x, fx, iterNum) = mishras_bird_optimum
        print(f"mishras_bird: x = [{', '.join(map(str, x))}], fx = {fx}")
        mishras_bird_result = OptimizationResultDto(x, fx, mishras_bird_args.generate_json(), generator.generate_json({"ProblemUri":mishras_bird_remote.uri, "ProblemName":mishras_bird_remote.problem_name}), generator.generate_json({"Count":iterNum}), "RandomSearch")
        loop = asyncio.get_event_loop()
        monitor_task = asyncio.ensure_future(monitor.save(mishras_bird_result))
        loop.run_until_complete(monitor_task)
        

if __name__ == "__main__":
    program = Program()
    program.main()
