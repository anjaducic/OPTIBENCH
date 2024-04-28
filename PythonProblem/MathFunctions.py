import math


class MathFunctions:
    @staticmethod
    def Easom(x):
        return -math.cos(x[0]) * math.cos(x[1]) * math.exp(-(x[0] - math.pi)**2 - (x[1] - math.pi)**2)
    
    @staticmethod
    def Beale(x):
        return math.pow(1.5 - x[0] + x[0] * x[1], 2) + math.pow(2.25 - x[0] + x[0] * math.pow(x[1], 2), 2) + math.pow(2.625 - x[0] + x[0] * math.pow(x[1], 3), 2)
    
    @staticmethod
    def Booth(x):
        return math.pow(x[0] + 2 * x[1] - 7, 2) + math.pow(2 * x[0] + x[1] - 5, 2)

    @staticmethod
    def ThreeHumpCamel(x):
        return 2 * math.pow(x[0], 2) - 1.05 * math.pow(x[0], 4) + (math.pow(x[0], 6))/6.0 + x[0] * x[1] + math.pow(x[1], 2)
    

    @staticmethod
    def McCormick(x):
        return math.sin(x[0] + x[1]) + math.pow(x[0] - x[1], 2) - 1.5 * x[0] + 2.5 * x[1] + 1 
   
    @staticmethod
    def BukinN6(x):
        return 100 * math.sqrt(abs(x[1] - 0.01 * math.pow(x[0], 2))) + 0.01 * abs(x[0] + 10)
   
    