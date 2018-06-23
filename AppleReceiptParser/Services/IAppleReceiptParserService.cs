using Apple.Receipt.Models;

namespace AppleReceiptParser.Services
{
    public interface IAppleReceiptParserService
    {
        AppleAppReceipt GetAppleReceiptFromBytes(byte[] bytes);
    }
}