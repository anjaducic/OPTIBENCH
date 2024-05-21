import math
from urllib.parse import urljoin
import requests
import json
from Dtos import OptimizationResultDto
from Abstractions.IMonitor import IMonitor

class Monitor(IMonitor):
    def __init__(self, uri: str) -> None:
        self.uri = uri
        self.client = requests.Session()
        self.client.headers.clear()
        self.client.headers.update({'Accept': 'application/json'})


    async def save(self, result: OptimizationResultDto) -> None:
        if math.isnan(result.Y):
            print("Failed to save the result to the database.")
            return

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
