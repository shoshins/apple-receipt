using AppleReceiptParser.Atn1;
using AppleReceiptParser.Models;

namespace AppleReceiptParser.Services.NodesParser.Apple
{
    public interface IAppleAsn1NodesParser
    {
        AppleAppReceipt GetAppleReceiptFromNode(Asn1Node tNode, AppleAppReceipt receipt = null);
    }
}