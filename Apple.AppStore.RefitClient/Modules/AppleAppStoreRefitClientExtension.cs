using Apple.AppStore.Client;
using Apple.Receipt.Verificator.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Refit;
using System;

namespace Apple.Receipt.Verificator.Modules
{
    public static class AppleAppStoreRefitClientExtension
    {
        public static IServiceCollection RegisterAppleAppStoreRefitClient(this IServiceCollection services, Action<AppleAppStoreRefitClientSettings>? configureOptions = null)
        {

            if (configureOptions != null)
            {
                services.Configure(configureOptions);
            }

            services.TryAddScoped<IAppStoreConnectAPI>();

            return services;
        }
    }
}
