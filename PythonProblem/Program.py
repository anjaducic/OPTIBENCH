from http.server import BaseHTTPRequestHandler, HTTPServer
from urllib.parse import urlparse, parse_qs
import json
import logging
import re

from PythonProblem.MathFunctions import MathFunctions

def calculate_problem(self, problem_name, x_values):
        if problem_name == "Spherical":
            return MathFunctions.Sphere(x_values)
        elif problem_name == "Rosenbrock":
            return MathFunctions.Rosenbrock(x_values)
        elif problem_name == "Rastrigin":
            return MathFunctions.Rastrigin(x_values)
        elif problem_name == "Matyas":
            return MathFunctions.Matyas(x_values)
        elif problem_name == "Easom":
            return MathFunctions.Easom(x_values)
        else:
            return None


class HTTPRequestHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        parsed_path = urlparse(self.path)
        query_params = parse_qs(parsed_path.query)
        if parsed_path.path.startswith('/problems/'):
            problem_name = parsed_path.path.split('/')[-1]
            x_values = [float(x) for x in query_params.get('x', [])]

            result = self.calculate_problem(problem_name, x_values)

            if result is not None:
                self.send_response(200)
                self.send_header('Content-Type', 'text/plain')
                self.end_headers()

                response = str(result)
                self.wfile.write(response.encode('utf-8'))
            else:
                self.send_response(404, 'Not Found: problem does not exist')
                self.end_headers()
        else:
            self.send_response(404, 'Not Found: endpoint does not exist')
            self.end_headers()

        
if __name__ == '__main__':
    server = HTTPServer(('localhost', 5055), HTTPRequestHandler)
    logging.info('Starting httpd...\n')
    try:
        server.serve_forever()  #pokrece sam server
    except KeyboardInterrupt:
        pass
    server.server_close()
    logging.info('Stopping httpd...\n')