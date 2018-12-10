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

            Token token1= new Token_ClientCredentials();
            Token token2 = new Token_Passwords();

            Console.WriteLine("TokenType?c/p");
            string tokenType = Console.ReadLine();
            while (tokenType!="exit")
            {
                switch (tokenType)
                {
                    case "c":
                        token1.Test();
                        break;
                    case "p":
                        token2.Test();
                        break;
                    default:
                        Console.WriteLine("invalid order");
                        break;
                }
                Console.WriteLine("TokenType?c/p");
                tokenType = Console.ReadLine();
            }


            Console.ReadKey();
        }
    }
}
