import math
from typing import Tuple
from random import random
from AbstractClasses import IOptimizer
from PythonOptimizer.AbstractClasses.IProblem import IProblem
from Utilities import OptimizerArguments


class RandomSearchOptimizer(IOptimizer):
    def __init__(self, args: OptimizerArguments):
        self.lower_bounds = args.array_double_specs["LowerBounds"]
        self.upper_bounds = args.array_double_specs["UpperBounds"]
        self.dimension = args.int_specs["Dimension"]
        self.max_iterations = args.int_specs["MaxIterations"]

    async def optimize(self, problem: IProblem) -> Tuple[list, float, int]:
        iter_num = 0
        best_x = [0.0] * self.dimension
        best_fitness = float('nan')

        for _ in range(self.max_iterations):
            current_x = [random() * (upper - lower) + lower for lower, upper in zip(self.lower_bounds, self.upper_bounds)]
            current_fitness = await problem.get_value(current_x)

            if math.isnan(current_fitness):
                continue

            iter_num += 1

            # novo najbolje rjesenje
            if math.isnan(best_fitness) or current_fitness < best_fitness:
                best_x = current_x
                best_fitness = current_fitness

        if iter_num > 0:
            return best_x, best_fitness, iter_num
        return best_x, float('nan'), iter_num
