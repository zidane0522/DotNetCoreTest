using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                //new ApiResource("api2","ApiServerTwo")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                //new Client(){
                //    ClientId="client1and2",
                //    ClientName="client1and2Name",
                //    ClientSecrets={ new Secret("one".Sha256()),new Secret("two".Sha256())},
                //    AllowedScopes={ "api1","api2"}
                //},

                new Client()
                {
                    ClientId="client1",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets={ new Secret("one".Sha256())},
                    AllowedScopes={ "api1"}
                },

                new Client()
                {
                    ClientId="mvc",
                    ClientSecrets={ new Secret("one".Sha256())},
                    ClientName="MVC Client",
                    AllowedGrantTypes=GrantTypes.Implicit,
                    AllowedScopes=new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    }
                }

                //,

                //new Client()
                //{
                //    ClientId="client2",
                //    ClientName="client2Name",
                //    ClientSecrets={ new Secret("two".Sha256())},
                //    AllowedScopes={ "api2"}
                //}
            };
        }

        public static IEnumerable<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser{
                    SubjectId="1",
                    Username="tom",
                    Password="psw",
                    Claims=
                    {
                        new Claim(ClaimTypes.Role,"sdx"),
                        new Claim(ClaimTypes.Name,"元英")
                    }
                },
                new TestUser{
                    SubjectId="2",
                    Username="jerry",
                    Password="psw",
                    Claims=
                    {
                        new Claim(ClaimTypes.Role,"reviewer"),
                        new Claim(ClaimTypes.Name,"尤莎")
                    }
                },
                new TestUser{
                    SubjectId="3",
                    Username="micky",
                    Password="psw",
                    Claims=
                    {
                        new Claim("name","reviewer"),
                        new Claim("website","尤莎")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }


}
