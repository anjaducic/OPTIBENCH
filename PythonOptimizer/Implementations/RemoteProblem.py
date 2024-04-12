import requests
from PythonOptimizer.AbstractClasses.IProblem import IProblem


class RemoteProblem(IProblem):
    def __init__(self, uri, problem_name):
        self.problem_name = problem_name
        self.uri = uri
        self.client = requests.Session()
        self.client.base_url = self.uri
        self.client.headers.clear()
        self.client.headers.update({'Accept': 'application/json'})

    async def get_value(self, x):
        path = f"problems/{self.problem_name}?" + "&".join([f"x={p}" for p in x])
        response = self.client.get(path)

        if response.status_code == 200:
            ret_problem = await response.text()

            try:
                parsed_problem = float(ret_problem)
                problem = parsed_problem
            except ValueError:
                print(f"Cannot parse response '{ret_problem}' to double value.")
        
        return problem
