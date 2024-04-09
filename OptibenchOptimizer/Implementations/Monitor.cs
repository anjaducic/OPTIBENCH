using System.Net.Http.Headers;
using System.Text;
using Dtos;
using interfaces;
using Newtonsoft.Json;

namespace Implementations
{
    class Monitor : IMonitor
    {
        private readonly HttpClient client = new HttpClient
        {
           Timeout = TimeSpan.FromSeconds(3) // ako ne dobije odgovor u roku od 3s, http zahtjev ce se prekinuti
        };

        public Monitor(string uri) 
        {
            client.BaseAddress = new Uri(uri); //za "monitor",
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task Save(OptimizationResultDto result)  //nema povr vrijednost za sada
        {
            if(double.IsNaN(result.Y))
            {
                Console.WriteLine($"Failed to save the result to the database."); 
                return;
            }
                  

            string path = $"result";
            var resultJson = JsonConvert.SerializeObject(result); // Serijalizujem objekat u json string
            var httpContent = new StringContent(resultJson, Encoding.UTF8, "application/json"); // StringContent moze da se koristi sa klijentom

            var httpResponse = await client.PostAsync(path, httpContent);
            if (!httpResponse.IsSuccessStatusCode)
            {            
                 Console.WriteLine($"Failed to save the result to the database.");  
            }
        }

        
    }
}