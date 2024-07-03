import math
from typing import Tuple, List
import random
from Abstractions.IOptimizer import IOptimizer
from Abstractions.IProblem import IProblem
from Utilities import OptimizerArguments


class PSOOptions:
    def __init__(self):
        self.cbi = 0.0
        self.cbf = 0.0
        self.cgi = 0.0
        self.cgf = 0.0
        self.wi = 0.0
        self.wf = 0.0
        self.vspan_init = 0.0
        self.init_offset = 0.0
        self.init_span = 0.0


class Particle:
    def __init__(self, initial_position: List[float], dimension: int, options: PSOOptions):
        self.dimensions = dimension
        self.options = options

        self.position = initial_position
        self.velocity = [0.0] * dimension
        self.best_position = initial_position[:]
        self.best_fitness = float('nan')
        self.fitness = float('nan')

        random_gen = random.Random()
        for i in range(dimension):
            self.velocity[i] = (random_gen.random() - 0.5) * 2 * options.vspan_init

    async def evaluate(self, problem: IProblem):
        new_fitness = await problem.get_value(self.position)
        if math.isnan(new_fitness):
            return

        PSOOptimizer.iter_num += 1
        self.fitness = new_fitness

        if math.isnan(self.best_fitness) or self.fitness < self.best_fitness:
            self.best_position = self.position[:]
            self.best_fitness = self.fitness

    def update_velocity(self, global_best_position: List[float], max_iterations: int, current_iteration: int):
        w = self.linear_interpolation(self.options.wf, self.options.wi, max_iterations, 0, current_iteration)
        cp = self.linear_interpolation(self.options.cbf, self.options.cbi, max_iterations, 0, current_iteration)
        cg = self.linear_interpolation(self.options.cgf, self.options.cgi, max_iterations, 0, current_iteration)

        random_gen = random.Random()
        for i in range(self.dimensions):
            r1 = random_gen.random()
            r2 = random_gen.random()
            vel_cognitive = cp * r1 * (self.best_position[i] - self.position[i])
            vel_social = cg * r2 * (global_best_position[i] - self.position[i])
            self.velocity[i] = w * self.velocity[i] + vel_cognitive + vel_social

    def update_position(self):
        for i in range(self.dimensions):
            self.position[i] += self.velocity[i]

    def linear_interpolation(self, xmax: float, xmin: float, tmax: int, tmin: int, t: int) -> float:
        return xmin + (xmax - xmin) / (tmax - tmin) * (tmax - t)

class PSOOptimizer(IOptimizer):
    iter_num = 0

    def __init__(self, args: OptimizerArguments):
        self.dimension = args.int_specs["Dimension"]
        self.max_iterations = args.int_specs["MaxIterations"]
        self.num_particles = args.int_specs["NumParticles"]
        self.options = PSOOptions()
        self.options.cbi = args.double_specs["Cbi"]
        self.options.cbf = args.double_specs["Cbf"]
        self.options.cgi = args.double_specs["Cgi"]
        self.options.cgf = args.double_specs["Cgf"]
        self.options.wi = args.double_specs["Wi"]
        self.options.wf = args.double_specs["Wf"]
        self.options.vspan_init = args.double_specs["VSpanInit"]
        self.options.init_offset = args.double_specs["InitOffset"]
        self.options.init_span = args.double_specs["InitSpan"]
        self.optimizer_name = "PSO-Python"

    async def optimize(self, problem: IProblem) -> Tuple[List[float], float, int]:
        self.population = []
        random_gen = random.Random()

        for _ in range(self.num_particles):
            initial_position = [(random_gen.random() - 0.5) * 2 * self.options.init_span + self.options.init_offset
                                for _ in range(self.dimension)]
            self.population.append(Particle(initial_position, self.dimension, self.options))

        global_best_position = [0.0] * self.dimension
        global_best_fitness = float('nan')

        for iter in range(self.max_iterations):
            global_best_position, global_best_fitness = await self.evaluate_fitness(problem, global_best_position, global_best_fitness)

            for particle in self.population:
                particle.update_velocity(global_best_position, self.max_iterations, iter)
                particle.update_position()

        return global_best_position, global_best_fitness, PSOOptimizer.iter_num

    async def evaluate_fitness(self, problem: IProblem, global_best_position: List[float], global_best_fitness: float) -> Tuple[List[float], float]:
        for particle in self.population:
            await particle.evaluate(problem)

            if math.isnan(global_best_fitness) or particle.best_fitness < global_best_fitness:
                global_best_position = particle.best_position[:]
                global_best_fitness = particle.best_fitness

        return global_best_position, global_best_fitness
