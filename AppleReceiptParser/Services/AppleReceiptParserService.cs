using AppleReceiptParser.Atn1;
using AppleReceiptParser.Models;
using AppleReceiptParser.Services.NodesParser;
using AppleReceiptParser.Services.NodesParser.Apple;

namespace AppleReceiptParser.Services
{
    public class AppleReceiptParserService : IAppleReceiptParserService
    {
        private readonly IAppleAsn1NodesParser _appleNodesParser;
        private readonly IAsn1NodesParser _nodesParser;

        public AppleReceiptParserService(IAppleAsn1NodesParser appleNodesParser, IAsn1NodesParser nodesParser)
        {
            _appleNodesParser = appleNodesParser;
            _nodesParser = nodesParser;
        }

        public AppleAppReceipt GetAppleReceiptFromBytes(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            Asn1Node node = _nodesParser.GetNodeFromBytes(bytes);
            AppleAppReceipt receipt = _appleNodesParser.GetAppleReceiptFromNode(node);

            return receipt;
        }
    }
}