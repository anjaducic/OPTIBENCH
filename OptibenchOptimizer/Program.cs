using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


namespace HttpClientSample
{

    class Program
    {
        static HttpClient client = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(2) // ako ne dobije odgovor u roku od 2s, http zahtjev ce se prekinuti
           
        };

        private static void SetClient() 
        {
            client.BaseAddress = new Uri("http://localhost:5030/"); //za "problem",
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }

        private static string EnterProblemName()
        {
            string problem_name;
            Console.Write("Enter problem name: ");
            do
            {
                problem_name = Console.ReadLine()!.Trim();
            }
            while(string.IsNullOrWhiteSpace(problem_name));
            return problem_name;
        }

        private static (double[], bool) EnterXValues()
        {
            string xInput;
            bool validInput = true;
            Console.Write("Enter array x: (separated by a comma): ");
            do
            {
                xInput = Console.ReadLine()!.Trim();
            }
            while(string.IsNullOrWhiteSpace(xInput));
                
            string[] xStringArray = xInput.Split(',');
            double[] x = new double[xStringArray.Length];
            for (int i = 0; i < xStringArray.Length; i++)
            {
                if (double.TryParse(xStringArray[i], out double value))
                {
                    x[i] = value;
                }
                else
                {
                    validInput = false;
                }
            }

            return (x, validInput);
        }

        static async Task<double> GetProblemAsync(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            double problem = double.NaN;
            if (response.IsSuccessStatusCode)
            {
                string problemString = await response.Content.ReadAsStringAsync();

                if (double.TryParse(problemString, out double parsedProblem))
                    problem = parsedProblem;
                else
                    Console.WriteLine($"Cannot parse response '{problemString}' to double value.");

            }
            return problem;
        }

        static async Task RunAsync()
        {
            SetClient();
            
            // Get problem
            double problem;
            do 
            {
                bool validInput = true;
                double[] x;
                string problem_name = EnterProblemName();
                (x, validInput) = EnterXValues();
                 
                if(validInput)
                {
                    string path = $"problems/{problem_name}?{string.Join("&", x.Select(p => $"x={p}"))}";
                    try
                    {
                        problem = await GetProblemAsync(path);
                        Console.WriteLine("Problem f(x): " + problem);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid input for x array.");
                }
                

                Console.WriteLine("Enter 0 to finish or anything else to continue.");

            }
            while(!Console.ReadLine()!.Equals("0"));  
        }

        
        
        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        
    }
}