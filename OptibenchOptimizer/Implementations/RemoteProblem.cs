using System.Net.Http.Headers;
using interfaces;

namespace Implementations 
{
    class RemoteProblem : IProblem
    {
        public readonly string ProblemName;
        public readonly string Uri;
        private readonly HttpClient client = new HttpClient
        {
           Timeout = TimeSpan.FromSeconds(3) // ako ne dobije odgovor u roku od 3s, http zahtjev ce se prekinuti
        };

        public RemoteProblem(string uri, string problemName) 
        {
            this.ProblemName = problemName;
            this.Uri = uri;
            client.BaseAddress = new Uri(this.Uri); //za "problem",
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<double> GetValue(double[] x)
        {
            string path = $"problems/{this.ProblemName}?{string.Join("&", x.Select(p => $"x={p}"))}";
            HttpResponseMessage response = await client.GetAsync(path);
            double problem = double.NaN;
            if (response.IsSuccessStatusCode)
            {
                string retProblem = await response.Content.ReadAsStringAsync();

                if (double.TryParse(retProblem, out double parsedProblem))
                    problem = parsedProblem;
                else
                    Console.WriteLine($"Cannot parse response '{retProblem}' to double value.");

            }
            return problem;
        }

        
    }
}