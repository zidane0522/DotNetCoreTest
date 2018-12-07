using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using static IdentityModel.OidcConstants;

namespace ClientConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            var httpDisco= httpClient.GetDiscoveryDocumentAsync("http://localhost:52302/").Result;
            if (httpDisco.IsError)
            {
                Console.WriteLine(httpDisco.IsError);
                return;
            }

            var tokenEndpont = httpDisco.TokenEndpoint;
            var keys = httpDisco.KeySet.Keys;

            #region passwordGrant
            var pswTokenResponse = httpClient.RequestTokenAsync(new IdentityModel.Client.TokenRequest {
                Address=tokenEndpont,
                ClientId= "mvc",
                ClientSecret="one",
                Parameters =
                {
                    new System.Collections.Generic.KeyValuePair<string, string>("Scope","api1"),
                    new System.Collections.Generic.KeyValuePair<string, string>("UserName","micky"),
                    new System.Collections.Generic.KeyValuePair<string, string>("Password","psw"),
                },
                GrantType=GrantTypes.Implicit
            }).Result;
            if (pswTokenResponse.IsError)
            {
                Console.WriteLine(pswTokenResponse.IsError);
                return;
            }
            var pswToken = pswTokenResponse.AccessToken;
            #endregion
            var pswCustom = pswTokenResponse.Json.TryGetString("custom_parameter");
            httpClient.SetBearerToken(pswToken);
            var pswResponse = httpClient.GetAsync("http://localhost:63388/One/GetMethod1").Result;


            #region client_credentials accessToken
            var tokenResponse = httpClient.RequestTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = tokenEndpont,
                ClientId = "client1",
                ClientSecret = "one",
                Scope = "api1",
                GrantType = GrantTypes.ClientCredentials
            }).Result;
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.IsError);
                return;
            }
            var token = tokenResponse.AccessToken;
            #endregion

            var custom = tokenResponse.Json.TryGetString("custom_parameter");
            httpClient.SetBearerToken(token);
            var response = httpClient.GetAsync("http://localhost:63388/One/GetMethod1").Result;

            //var disco = DiscoveryClient.GetAsync("http://localhost:63388/").Result;
            //if (disco.IsError)
            //{
            //    Console.WriteLine(disco.IsError);
            //    return;
            //}

          
            //var tokenClient1 = new TokenClient(disco.TokenEndpoint, "client1", "one");
            //var tokenResponse1 = tokenClient1.RequestClientCredentialsAsync("api1").Result;
            //if (tokenResponse1.IsError)
            //{
            //    Console.WriteLine(tokenResponse1.Error);
            //    return;
            //}
            //var client1 = new HttpClient();
            //client1.SetBearerToken(tokenResponse1.AccessToken);
            //var response = client1.GetAsync("http://localhost:63388/One/GetMethod1").Result;
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
