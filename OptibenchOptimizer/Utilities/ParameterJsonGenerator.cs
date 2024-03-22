using Newtonsoft.Json;

namespace Utilities
{
    public class ParameterJsonGenerator
    {
        public string GenerateJson(Dictionary<string, object> parameters)
        {
            return JsonConvert.SerializeObject(parameters);
        }
    }
}

