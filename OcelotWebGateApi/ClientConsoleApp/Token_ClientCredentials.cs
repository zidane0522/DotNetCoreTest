using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using static IdentityModel.OidcConstants;

namespace ClientConsoleApp
{
    public class Token_ClientCredentials:Token
    {
        public override void Test()
        {
            var httpClient = new HttpClient();
            var httpDisco = httpClient.GetDiscoveryDocumentAsync("http://localhost:52302/").Result;
            if (httpDisco.IsError)
            {
                Console.WriteLine(httpDisco.IsError);
                return;
            }

            var tokenEndpont = httpDisco.TokenEndpoint;
            var keys = httpDisco.KeySet.Keys;

            #region client_credentials accessToken
            var tokenResponse = httpClient.RequestTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = tokenEndpont,
                ClientId = "cClient",
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
