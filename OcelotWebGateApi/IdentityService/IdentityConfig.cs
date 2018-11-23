using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService
{
    public class IdentityConfig
    {

        public static IConfiguration Configuration { get; set; }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1","ApiServerOne"),
                new ApiResource("api2","ApiServerTwo")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client(){
                    ClientId="client1",
                    ClientName="client1Name",
                    ClientSecrets={ new Secret("one".Sha256()),new Secret("two".Sha256())},
                    AllowedScopes={ "api1","api2"}
                },

                new Client()
                {
                    ClientId="client2",
                    ClientName="client2Name",
                    ClientSecrets={ new Secret("one".Sha256())},
                    AllowedScopes={ "api1"}
                }
            };
        }
    }


}
