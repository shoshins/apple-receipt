using Apple.Receipt.Models;
using AppleReceiptParser.Asn1;

namespace AppleReceiptParser.Services.NodesParser.Apple
{
    public interface IAppleAsn1NodesParser
    {
        AppleAppReceipt GetAppleReceiptFromNode(Asn1Node tNode, AppleAppReceipt receipt = null);
    }
}