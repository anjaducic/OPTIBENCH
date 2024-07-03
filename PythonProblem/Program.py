from http.server import BaseHTTPRequestHandler, HTTPServer
from urllib.parse import urlparse, parse_qs
import logging
import math
from MathFunctions import MathFunctions




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
                self.send_response(404, 'Not Found: result does not exist')
                self.end_headers()
        else:
            self.send_response(404, 'Not Found: problem does not exist')
            self.end_headers()

    def do_GET_exact_solution(self):
        parsed_path = urlparse(self.path)
        if parsed_path.path.startswith('/exact-solution/'):
            problem_name = parsed_path.path.split('/')[-1]
            result = self.get_exact_solution(problem_name)

            if result is not None:
                self.send_response(200)
                self.send_header('Content-Type', 'text/plain')
                self.end_headers()

                response = str(result)
                self.wfile.write(response.encode('utf-8'))
            else:
                self.send_response(200)
                self.send_header('Content-Type', 'text/plain')
                self.end_headers()

                response = str(math.nan)
                self.wfile.write(response.encode('utf-8'))
        else:
            self.send_response(404, 'Problem not found.')
            self.end_headers()


    def calculate_problem(self, problem_name, x_values):
        if problem_name == "Spherical":
            return MathFunctions.Sphere(x_values)
        elif problem_name == "Easom":
            return MathFunctions.Easom(x_values)
        elif problem_name == "Beale":
            return MathFunctions.Beale(x_values)
        elif problem_name == "Booth":
            return MathFunctions.Booth(x_values)
        elif problem_name == "ThreeHumpCamel":
            return MathFunctions.ThreeHumpCamel(x_values)
        elif problem_name == "McCormick":
            return MathFunctions.McCormick(x_values)
        elif problem_name == "BukinN6":
            return MathFunctions.BukinN6(x_values)
        else:
            return None
        
    def get_exact_solution(self, problem_name):
            if problem_name == "Spherical":
                return 0.0
            elif problem_name == "Easom":
                return -1.0
            elif problem_name == "Beale":
                return 0.0
            elif problem_name == "Booth":
                return 0.0
            elif problem_name == "ThreeHumpCamel":
                return 0.0
            elif problem_name == "McCormick":
                return -1.9133
            elif problem_name == "BukinN6":
                return -0.0
            else:
                return None

        
if __name__ == '__main__':
    server = HTTPServer(('localhost', 5055), HTTPRequestHandler)
    logging.info('Starting httpd...\n')
    try:
        server.serve_forever()  #pokrece sam server
    except KeyboardInterrupt:
        pass
    server.server_close()
    logging.info('Stopping httpd...\n')