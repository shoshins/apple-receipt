using AppleReceiptParser.Services;
using AppleReceiptParser.Services.NodesParser;
using AppleReceiptParser.Services.NodesParser.Apple;
using Autofac;

namespace AppleReceiptParser.Modules
{
    public class AppleReceiptParserModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppleAsn1NodesParser>()
                .As<IAppleAsn1NodesParser>();
            builder.RegisterType<Asn1NodesParser>()
                .As<IAsn1NodesParser>();
            builder.RegisterType<Asn1ParserUtilitiesService>()
                .As<IAsn1ParserUtilitiesService>();
            builder.RegisterType<AppleReceiptParserService>()
                .As<IAppleReceiptParserService>();
        }
    }
}
