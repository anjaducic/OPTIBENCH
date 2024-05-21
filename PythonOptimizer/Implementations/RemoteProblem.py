import math
from urllib.parse import urljoin
import requests
from Abstractions.IProblem import IProblem


class RemoteProblem(IProblem):
    def __init__(self, uri, problem_name):
        self.problem_name = problem_name
        self.uri = uri
        self.client = requests.Session()
        self.client.headers.clear()
        self.client.headers.update({'Accept': 'application/json'})

    async def get_value(self, x):
        path = f"problems/{self.problem_name}?" + "&".join([f"x={p}" for p in x])
        full_url = urljoin(self.uri, path)
        response = self.client.get(full_url)
        problem = math.nan

        if response.status_code == 200:
            ret_problem = response.json()

            try:
                parsed_problem = float(ret_problem)
                problem = parsed_problem
            except ValueError:
                print(f"Cannot parse response '{ret_problem}' to double value.")
        
        return problem
