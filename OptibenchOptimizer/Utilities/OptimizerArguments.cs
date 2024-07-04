using Newtonsoft.Json;

namespace Utilities
{
    public class OptimizerArguments
    {
        public Dictionary<string, int>? IntSpecs { get; set; }
        public Dictionary<string, int[]>? ArrayIntSpecs { get; set; } 
        public Dictionary<string, double>? DoubleSpecs { get; set; }
        public Dictionary<string, double[]>? ArrayDoubleSpecs { get; set; }
        public Dictionary<string, bool>? BooleanConfig { get; set; }
        public string GenerateJson()
        {
            Dictionary<string, object> allParameters = new Dictionary<string, object>();

            if (IntSpecs != null && IntSpecs.Any())
                allParameters.Add("IntSpecs", IntSpecs); 
            if (ArrayIntSpecs != null && ArrayIntSpecs.Any())
                allParameters.Add("ArrayIntSpecs", ArrayIntSpecs);            
            if (DoubleSpecs != null && DoubleSpecs.Any())
                allParameters.Add("DoubleSpecs", DoubleSpecs);
            if (ArrayDoubleSpecs != null && ArrayDoubleSpecs.Any())
                allParameters.Add("ArrayDoubleSpecs", ArrayDoubleSpecs);
            if (BooleanConfig != null && BooleanConfig.Any())
                allParameters.Add("BooleanConfig", BooleanConfig);
            return JsonConvert.SerializeObject(allParameters);
        }
    }
}

