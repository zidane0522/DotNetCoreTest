using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace WebGateApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        {
            var builder= new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            builder.SetBasePath(environment.ContentRootPath)
                   .AddJsonFile("appsettings.json", false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
                   .AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true)
                   .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Action<ConfigurationBuilderCachePart> settings = (x) =>
            //{
            //    x.WithMicrosoftLogging(log =>
            //    {
            //        log.AddConsole(LogLevel.Debug);

            //    }).WithDictionaryHandle();
            //};

            var authenticationProviderKey = new string[] { "ClientServiceKeyForOneApi", "ClientServiceKeyForTwoApi" };
            Action<IdentityServerAuthenticationOptions> optionsForOneApi = options => {
                options.Authority = "http://localhost:52302";
                options.SupportedTokens = SupportedTokens.Both;
                //options.ApiSecret = "one";
            };

            Action<IdentityServerAuthenticationOptions> optionsForTwoApi = options => {
                options.Authority = "http://localhost:52302";
                options.SupportedTokens = SupportedTokens.Both;
                //options.ApiSecret = "Two";
            };

            services.AddAuthentication().AddIdentityServerAuthentication(authenticationProviderKey[0])
                .AddIdentityServerAuthentication(authenticationProviderKey[1],optionsForTwoApi);

            services.AddOcelot(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseOcelot().Wait();
        }
    }
}
