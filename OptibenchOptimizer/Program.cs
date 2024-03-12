using System.Net.Http.Headers;


namespace HttpClientSample
{

    class Program
    {
        static Random random = new Random();
        static HttpClient client = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(3) // ako ne dobije odgovor u roku od 3s, http zahtjev ce se prekinuti
           
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

    //algotirmi

    // Random search algoritam, N dimenz prostor
        static async Task<(double[], double)> RandomSearch(string problem_name, double[] lowerBounds, double[] upperBounds, int dimension, int maxIterations)
        {
            //inicijalizujem
            double[] bestX = new double[dimension];
            double bestFitness = double.NaN;
            for (int i = 0; i < dimension; i++)
            {
                bestX[i] = random.NextDouble() * (upperBounds[i] - lowerBounds[i]) + lowerBounds[i];
            }
            //vratim f(x) pozivom servera
            string path = $"problems/{problem_name}?{string.Join("&", bestX.Select(p => $"x={p}"))}";
            try
            { 
                bestFitness = await GetProblemAsync(path);
                Console.WriteLine("Problem f(x): " + bestFitness);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            for (int i = 0; i < maxIterations; i++)
            {
                double[] currentX = new double[dimension];
                double currentFitness = double.NaN;
                for (int j = 0; j < dimension; j++)
                {
                    currentX[j] = random.NextDouble() * (upperBounds[j] - lowerBounds[j]) + lowerBounds[j];
                }
                //vratim f(x) pozivom servera
                path = $"problems/{problem_name}?{string.Join("&", currentX.Select(p => $"x={p}"))}";
                try
                { 
                    currentFitness = await GetProblemAsync(path);
                    Console.WriteLine("Problem f(x): " + currentFitness);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                // novo najbolje rjesenje
                if (currentFitness < bestFitness)
                {
                    Array.Copy(currentX, bestX, dimension);
                    bestFitness = currentFitness;
                }
            }

            return (bestX, bestFitness);
        }

        static async Task RunAsync()
        {
            SetClient();
            
            // Find optimum
            do 
            {
                double[] bestX;
                double bestFitness;
                string problem_name = EnterProblemName();
                (bestX, bestFitness) = await RandomSearch(problem_name,[-5],[5],1,1000);  //vidjeti za ND  
                Console.WriteLine("\nBEST X: " + $"[{string.Join(", ", bestX)}]" + ", BEST F(X): " + bestFitness);          
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