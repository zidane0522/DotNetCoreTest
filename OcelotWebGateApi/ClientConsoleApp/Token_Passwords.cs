using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using static IdentityModel.OidcConstants;

namespace ClientConsoleApp
{
    public class Token_Passwords:Token
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

            #region passwordGrant
            var pswTokenResponse = httpClient.RequestPasswordTokenAsync(
                new PasswordTokenRequest()
                {
                    Address=tokenEndpont,
                    UserName = "tom",
                    Password = "psw",
                    Scope="api1",
                    ClientId="pClient",
                    ClientSecret="one",
                    GrantType=GrantTypes.Password

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


            if (!pswResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(pswResponse.StatusCode);
            }
            else
            {
                var content = pswResponse.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);
            }
            Console.ReadKey();
        }
    }
}
