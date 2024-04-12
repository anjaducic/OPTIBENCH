import json

class ParameterJsonGenerator:
    def generate_json(self, parameters):
        return json.dumps(parameters)


#generator = ParameterJsonGenerator()
#params = {"param1": 10, "param2": "value"}
#json_string = generator.generate_json(params)
#print(json_string)
