class OptimizationResultDto:
    def __init__(self, x, y, parameters, problem_info, evaluation_count, optimizer_name):
        self.X = x
        self.Y = y
        self.Params = parameters
        self.ProblemInfo = problem_info
        self.EvaluationCount = evaluation_count
        self.OptimizerName = optimizer_name
        self.ExactSolution = None