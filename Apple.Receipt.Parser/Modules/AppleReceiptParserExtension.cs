using Apple.Receipt.Parser.Services;
using Apple.Receipt.Parser.Services.NodesParser;
using Apple.Receipt.Parser.Services.NodesParser.Apple;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Apple.Receipt.Parser.Modules
{
    public static class AppleReceiptParserExtension
    {
        public static IServiceCollection RegisterAppleReceiptParser(this IServiceCollection services)
        {
            services.TryAddScoped<IAppleAsn1NodesParser, AppleAsn1NodesParser>();
            services.TryAddScoped<IAsn1NodesParser, Asn1NodesParser>();
            services.TryAddScoped<IAsn1ParserUtilitiesService, Asn1ParserUtilitiesService>();
            services.TryAddScoped<IAppleReceiptParserService, AppleReceiptParserService>();

            return services;
        }
    }
}
