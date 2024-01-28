using Apple.Receipt.Parser.Modules;
using Apple.Receipt.Verificator.Models;
using Apple.Receipt.Verificator.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Refit;
using System;

namespace Apple.Receipt.Verificator.Modules
{
    public static class AppleReceiptVerificatorExtension
    {
        public static IServiceCollection RegisterAppleReceiptVerificator(this IServiceCollection services, Action<AppStoreReceiptVerifierSettings>? configureOptions = null)
        {
            services.RegisterAppleReceiptParser();

            if (configureOptions != null)
            {
                services.Configure(configureOptions);
            }

            var verificationSettings = new AppStoreReceiptVerifierSettings();
            var appleProductionUrl = verificationSettings.ProductionUrl;
            var appleSandboxUrl = verificationSettings.SandboxUrl;
            var refitSettings = new RefitSettings(new NewtonsoftJsonContentSerializer());
            
            services.TryAddScoped<IAppleReceiptVerificatorService, AppleReceiptVerificatorService>();

            return services;
        }
    }
}
