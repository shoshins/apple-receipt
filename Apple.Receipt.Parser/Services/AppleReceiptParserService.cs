using Apple.Receipt.Models;
using Apple.Receipt.Parser.Services.NodesParser;
using Apple.Receipt.Parser.Services.NodesParser.Apple;

namespace Apple.Receipt.Parser.Services
{
    internal class AppleReceiptParserService : IAppleReceiptParserService
    {
        private readonly IAppleAsn1NodesParser _appleNodesParser;
        private readonly IAsn1NodesParser _nodesParser;

        public AppleReceiptParserService(IAppleAsn1NodesParser appleNodesParser, IAsn1NodesParser nodesParser)
        {
            _appleNodesParser = appleNodesParser;
            _nodesParser = nodesParser;
        }

        public AppleAppReceipt? GetAppleReceiptFromBytes(byte[]? bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            var node = _nodesParser.GetNodeFromBytes(bytes);
            var receipt = _appleNodesParser.GetAppleReceiptFromNode(node);

            return receipt;
        }
    }
}
