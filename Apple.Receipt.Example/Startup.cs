using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apple.Receipt.Parser.Modules;
using Apple.Receipt.Verificator.Models;
using Apple.Receipt.Verificator.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Apple.Receipt.Example
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
            services.AddControllers();
            
            // Example of getting Verification type.
            var verificationType = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production" ?
                AppleReceiptVerificationType.Production : AppleReceiptVerificationType.Sandbox;

            // Registering Example Configuration
            // TODO: Place your own configs to the appsettings.json.
            AppConfig appConfig = new AppConfig();
            Configuration.GetSection("AppConfig").Bind(appConfig);
            services.AddSingleton(appConfig);
            
            // Registering Verification Service.
            services.RegisterAppleReceiptVerificator(options =>
            {
                options.VerifyReceiptSharedSecret = appConfig.AppleSharedKey;
                options.VerificationType = verificationType;
                options.AllowedBundleIds = new[] { appConfig.AppleAppId };
            });

            services.RegisterAppleReceiptParser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
