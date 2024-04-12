import json


class OptimizerArguments:
    def __init__(self):
        self.int_specs = None
        self.array_int_specs = None
        self.double_specs = None
        self.array_double_specs = None
        self.boolean_config = None
    def generate_json(self):
        all_parameters = {}
        if self.int_specs:
            all_parameters["IntSpecs"] = self.int_specs
        if self.array_int_specs:
            all_parameters["ArrayIntSpecs"] = self.array_int_specs
        if self.double_specs:
            all_parameters["DoubleSpecs"] = self.double_specs
        if self.array_double_specs:
            all_parameters["ArrayDoubleSpecs"] = self.array_double_specs
        if self.boolean_config:
            all_parameters["BooleanConfig"] = self.boolean_config

        return json.dumps(all_parameters)   #vraca json string
             
