import math


class MathFunctions:
    @staticmethod
    def Sphere(x):
        return sum([xi**2 for xi in x])

    @staticmethod
    def Rosenbrock(x):
        return sum([100 * (x[i+1] - x[i]**2)**2 + (1 - x[i])**2 for i in range(len(x)-1)])

    @staticmethod
    def Rastrigin(x):
        return sum([xi**2 - 10 * math.cos(2 * math.pi * xi) + 10 for xi in x])

    @staticmethod
    def Matyas(x):
        return 0.26 * (x[0]**2 + x[1]**2) - 0.48 * x[0] * x[1]

    @staticmethod
    def Easom(x):
        return -math.cos(x[0]) * math.cos(x[1]) * math.exp(-(x[0] - math.pi)**2 - (x[1] - math.pi)**2)

   