using Apple.Receipt.Models;

namespace Apple.Receipt.Parser.Services
{
    public interface IAppleReceiptParserService
    {
        AppleAppReceipt GetAppleReceiptFromBytes(byte[] bytes);
    }
}