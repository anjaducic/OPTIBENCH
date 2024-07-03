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
    
    
    async def get_exact_solution(self, problem_name):
        path = f"exact-solution/{problem_name}"
        full_url = urljoin(self.uri, path)
        exact_solution = math.inf  # Set to max by default
       

        try:
            response = self.client.get(full_url)
            if response.status_code == 200:
                ret_solution = response.text
                try:
                    exact_solution = float(ret_solution)
                except ValueError:
                    print(f"Cannot parse response '{ret_solution}' to double value.")
            else:
                print(f"Request failed with status code {response.status_code}")
        except requests.RequestException as exc:
            print(f"An error occurred while requesting {exc.request.url!r}.")

        return exact_solution
