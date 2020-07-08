using Apple.Receipt.Parser.Services;
using Apple.Receipt.Parser.Services.NodesParser;
using Apple.Receipt.Parser.Services.NodesParser.Apple;
using Microsoft.Extensions.DependencyInjection;

namespace Apple.Receipt.Parser.Modules
{
    public static class AppleReceiptParserExtension
    {
        public static IServiceCollection RegisterAppleReceiptParser(this IServiceCollection services)
        {
            services.AddScoped<IAppleAsn1NodesParser, AppleAsn1NodesParser>();
            services.AddScoped<IAsn1NodesParser, Asn1NodesParser>();
            services.AddScoped<IAsn1ParserUtilitiesService, Asn1ParserUtilitiesService>();
            services.AddScoped<IAppleReceiptParserService, AppleReceiptParserService>();

            return services;
        }
    }
}
