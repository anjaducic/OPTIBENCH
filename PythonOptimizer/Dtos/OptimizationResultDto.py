class OptimizationResultDto:
    def __init__(self, x, y, parameters, problem_info, evaluation_count, optimizer_name):
        self.x = x
        self.y = y
        self.params = parameters
        self.problem_info = problem_info
        self.evaluation_count = evaluation_count
        self.optimizer_name = optimizer_name