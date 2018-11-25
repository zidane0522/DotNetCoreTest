using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace ClientConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var disco = DiscoveryClient.GetAsync("http://localhost:52302").Result;
            if (disco.IsError)
            {
                Console.WriteLine(disco.IsError);
                return;
            }
            var tokenClient1 = new TokenClient(disco.TokenEndpoint, "client1", "one");
            var tokenResponse1 = tokenClient1.RequestClientCredentialsAsync("api1").Result;
            if (tokenResponse1.IsError)
            {
                Console.WriteLine(tokenResponse1.Error);
                return;
            }
            var client1 = new HttpClient();
            client1.SetBearerToken(tokenResponse1.AccessToken);
            var response = client1.GetAsync("http://localhost:63388/One/GetMethod1").Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);
            }
            Console.ReadKey();

        }
    }
}
