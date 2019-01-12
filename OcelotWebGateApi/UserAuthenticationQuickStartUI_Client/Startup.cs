﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserAuthenticationQuickStartUI_Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options=> {
               
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            }).AddCookie("Cookies")
            //.AddOpenIdConnect("oidc",options=> {
            //    options.ClientId = "mvc";
            //    options.GetClaimsFromUserInfoEndpoint = false;
            //    options.SignInScheme = "Cookies";
            //    options.Authority = "http://localhost:56400";
            //    options.RequireHttpsMetadata = false;
            //    options.SaveTokens = true;
            //    options.Scope.Add("openid");
            //    options.Scope.Add("profile");
            //    options.Scope.Add("address");
            //    options.Scope.Add("phone");
            //    options.Scope.Add("email");
            //});

            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookies";

                options.Authority = "http://localhost:56400";
                options.RequireHttpsMetadata = false;

                options.ClientId = "HybridClient";
                options.ClientSecret = "secret";
                options.ResponseType = "code id_token";

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("address");
                options.Scope.Add("phone");
                options.Scope.Add("email");
                options.Scope.Add("api1");
                options.Scope.Add("offline_access");
                options.ClaimActions.MapJsonKey("website", "website");
            });
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
