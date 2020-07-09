using Apple.Receipt.Models;
using Apple.Receipt.Parser.Asn1;

namespace Apple.Receipt.Parser.Services.NodesParser.Apple
{
    internal interface IAppleAsn1NodesParser
    {
        AppleAppReceipt GetAppleReceiptFromNode(Asn1Node tNode, AppleAppReceipt? receipt = null);
    }
}
