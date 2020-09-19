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
                services.Configure(configureOptions);
            }

            services.AddRefitClient<IRestService>()
                .ConfigureHttpClient((serviceProvider, httpClient) 
                    => httpClient.BaseAddress = new Uri(
                        serviceProvider.GetRequiredService<IOptions<AppleReceiptVerificationSettings>>()
                            .Value.VerifyUrl));

            services.TryAddScoped<IAppleReceiptVerificatorService, AppleReceiptVerificatorService>();

            return services;
        }
    }
}
