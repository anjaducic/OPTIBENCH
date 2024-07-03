import math
from urllib.parse import urljoin
import requests
import json
from Dtos import OptimizationResultDto
from Abstractions.IMonitor import IMonitor
from Implementations.RemoteProblem import RemoteProblem

class Monitor(IMonitor):
    def __init__(self, uri: str) -> None:
        self.uri = uri
        self.client = requests.Session()
        self.client.headers.clear()
        self.client.headers.update({'Accept': 'application/json'})


    async def save(self, result: OptimizationResultDto, problem: RemoteProblem) -> None:
        if math.isnan(result.Y):
            print("Failed to save the result to the database.")
            return
        problem_info = json.loads(result.ProblemInfo)
        problem_name = problem_info["ProblemName"]
        print(problem_info)
        print(problem_name)
        result.ExactSolution = await problem.get_exact_solution(problem_name)
        print(result.ExactSolution)

        path = "result"
        full_url = urljoin(self.uri, path)
        result_json = json.dumps(result.__dict__)
        headers = {'Content-Type': 'application/json'}

        print(result_json)
        
        try:
            http_response =  self.client.post(full_url, data=result_json, headers=headers, timeout=3)
            http_response.raise_for_status()
        except requests.exceptions.RequestException as e:
            print(f"Failed to save the result to the database. Error: {e}")
