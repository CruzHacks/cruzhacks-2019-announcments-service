using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using dotenv.net;
using cruzhacks_2019_announcments_service.Models;

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

            /*
            if (System.Environment.GetEnvironmentVariable("DEPLOYMENT_ENV").Equals("PROD"))
            {
                // Azure SQL Database
                string connectionString = System.Environment.GetEnvironmentVariable("DB_DONNECTION_STRING");
                services.AddDbContext<MessageContext>(opt => opt.UseSqlServer(connectionString));
            }
            else
            {
                // In Memory DB
                services.AddDbContext<MessageContext>(opt => opt.UseInMemoryDatabase("Announcments"));
            }
            */

            services.AddDbContext<MessageContext>(opt => opt.UseInMemoryDatabase("Announcments"));
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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
