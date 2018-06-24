using System;
using System.Linq;
using System.Reflection;
using Apple.Receipt.Parser.Services;
using Apple.Receipt.Parser.Services.NodesParser;
using Apple.Receipt.Parser.Services.NodesParser.Apple;
using Autofac;
using Autofac.Core.Activators.Reflection;
using Module = Autofac.Module;

namespace Apple.Receipt.Parser.Modules
{
    public class AppleReceiptParserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppleAsn1NodesParser>()
                .As<IAppleAsn1NodesParser>();
            builder.RegisterType<Asn1NodesParser>()
                .As<IAsn1NodesParser>();
            builder.RegisterType<Asn1ParserUtilitiesService>()
                .As<IAsn1ParserUtilitiesService>();
            builder.RegisterType<AppleReceiptParserService>().As<IAppleReceiptParserService>()
                .FindConstructorsWith(new InternalConstructorFinder()).AsSelf();
        }
    }

    public class InternalConstructorFinder : IConstructorFinder
    {
        public ConstructorInfo[] FindConstructors(Type t)
        {
            return t.GetTypeInfo().DeclaredConstructors.Where(c => !c.IsPrivate && !c.IsPublic).ToArray();
        }
    }
}