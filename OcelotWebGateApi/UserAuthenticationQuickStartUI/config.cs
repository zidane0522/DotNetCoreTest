using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UserAuthenticationQuickStartUI
{

    public class config
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
                new Client(){
                    ClientId="cClient",
                    ClientName="cClient",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    ClientSecrets={ new Secret("one".Sha256())},
                    AllowedScopes={ "api1"}
                },

                new Client()
                {    ClientName="pClient",
                    ClientId="pClient",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,

                    ClientSecrets={ new Secret("one".Sha256())},
                    AllowedScopes={ "api1"}
                },

                new Client()
                {
                    ClientId="mvc",
                    ClientName="MVC Client",
                    AllowedGrantTypes=GrantTypes.Implicit,
                    AlwaysIncludeUserClaimsInIdToken=true,
                    AlwaysSendClientClaims=true,
                    RedirectUris={ "http://localhost:52086/signin-oidc" },
                    PostLogoutRedirectUris={ "http://localhost:52086/signout-callback-oidc" },
                    AllowedScopes=new List<string>
                    {   IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
   
                    }
                },

                new Client()
                {
                        ClientId = "HybridClient",
                        ClientName = "Hybrid Client",
                        AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                                    AlwaysIncludeUserClaimsInIdToken=true,
                    AlwaysSendClientClaims=true,
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },

                        RedirectUris           = { "http://localhost:52086/signin-oidc" },
                        PostLogoutRedirectUris = { "http://localhost:52086/signout-callback-oidc" },

                        AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.Phone,
                            IdentityServerConstants.StandardScopes.Address,
                            IdentityServerConstants.StandardScopes.Email,
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "api1"
                        },
                        AllowOfflineAccess = true
                }
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
                        new Claim(JwtClaimTypes.Role,"sdx"),
                        new Claim(JwtClaimTypes.Name,"元英"),
                        new Claim(JwtClaimTypes.Email,"lishen@163.com"),
                        new Claim(JwtClaimTypes.PhoneNumber,"13522225555"),
                        new Claim(JwtClaimTypes.Address,"中国上海市静安区")
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
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),

            };
        }
    }
}
