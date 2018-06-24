using System;
using System.Net.Http;
using Apple.Receipt.Parser.Modules;
using Apple.Receipt.Verificator.Models;
using Apple.Receipt.Verificator.Services;
using Autofac;
using Refit;
using Serilog;

namespace Apple.Receipt.Verificator.Modules
{
    public class AppleReceiptVerificatorModule : Module
    {
        private readonly AppleReceiptVerificationSettings _settings;

        public AppleReceiptVerificatorModule(AppleReceiptVerificationSettings settings)
        {
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_settings.RegisterLogger)
            {
                builder.Register<ILogger>((c, p) => _settings.LoggerConfiguration.CreateLogger()).SingleInstance();
            }
            builder.Register(c =>
                {
                    HttpClient httpClient = new HttpClient
                    {
                        BaseAddress = new Uri(_settings.VerifyReceiptUrl)
                    };
                    return RestService.For<IRestService>(httpClient);
                })
                .As<IRestService>()
                .InstancePerDependency();

            builder.RegisterModule<AppleReceiptParserModule>();
            builder.RegisterType<AppleReceiptVerificatorService>().As<IAppleReceiptVerificatorService>()
                .WithParameter("settings", _settings);
            ;
        }
    }
}