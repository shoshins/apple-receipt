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
        public static IServiceCollection RegisterAppleReceiptVerificator(this IServiceCollection services, Action<AppleReceiptVerificationSettings>? configureOptions = null)
        {
            services.RegisterAppleReceiptParser();

            if (configureOptions != null)
            {
                services.Configure<AppleReceiptVerificationSettings>(configureOptions);
            }

            services.AddRefitClient<IRestService>()
                .ConfigureHttpClient((serviceProvider, httpClient) =>
                {
                    var options = serviceProvider.GetRequiredService<IOptionsSnapshot<AppleReceiptVerificationSettings>>();

                    httpClient.BaseAddress = new Uri(options.Value.VerifyUrl);
                });

            services.TryAddScoped<IAppleReceiptVerificatorService, AppleReceiptVerificatorService>();

            return services;
        }
    }
}
