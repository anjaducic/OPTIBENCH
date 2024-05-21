import json


class OptimizerArguments:
    def __init__(self, int_specs=None, array_int_specs=None, double_specs=None, array_double_specs=None, boolean_config=None):
        self.int_specs = int_specs or {}
        self.array_int_specs = array_int_specs or {}
        self.double_specs = double_specs or {}
        self.array_double_specs = array_double_specs or {}
        self.boolean_config = boolean_config or {}
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

        return json.dumps(all_parameters, separators=(',', ':'))   #vraca json string
             
