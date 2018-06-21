using AppleReceiptParser.Atn1;
using AppleReceiptParser.Models;

namespace AppleReceiptParser.Services
{
    public interface IAppleAsn1NodesParser
    {
        AppleAppReceipt GetAppleReceiptFromNode(Asn1Node tNode, AppleAppReceipt receipt = null);
    }
}
