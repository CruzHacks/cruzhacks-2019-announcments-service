﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using dotenv.net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace cruzhacks_2019_announcments_service
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
            // Parse env vars from local .env file.
            DotEnv.Config();

            //Console.WriteLine(System.Environment.GetEnvironmentVariable("TEST_VAR"));

            string env = System.Environment.GetEnvironmentVariable("DEV_ENVIRONMENT");

            // Use Azure DB if env is production. Use in-memory db if env is dev or not set.

            if (env != null && env.Equals("PROD"))
            {
                // Azure SQL Database
                string connectionString = @Environment.GetEnvironmentVariable("DB_DONNECTION_STRING");
                Console.WriteLine(connectionString);
                services.AddDbContext<MessageContext>(opt => opt.UseSqlServer(connectionString));

            }
            else
            {
                // In Memory DB
                services.AddDbContext<MessageContext>(opt => opt.UseInMemoryDatabase("Announcments"));
            }

            //services.AddDbContext<MessageContext>(opt => opt.UseInMemoryDatabase("Announcments"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

            // Check API Key
            app.Use((context, next) =>
            {
                string authHeader = context.Request.Headers["Authorization"];
                string apiKey = System.Environment.GetEnvironmentVariable("API_KEY");

                if (string.IsNullOrEmpty(authHeader) || !authHeader.Equals(apiKey))
                {

                    Dictionary<string, string> responseBody = new Dictionary<string, string>() {
                        { "code", "401"},
                        { "error", "true" },
                        { "message", "Invalid or missing API key." }
                    };

                    string jsonResponse = JsonConvert.SerializeObject(responseBody);

                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";

                    return context.Response.WriteAsync(jsonResponse);
                }

                return next();
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
