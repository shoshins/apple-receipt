using Apple.Receipt.Models;
using Apple.Receipt.Verificator.Models;

namespace Apple.Receipt.Verificator.Services
{
    public interface IAppleReceiptCustomVerificatorService
    {
        AppleReceiptVerificationResult? ValidateReceipt(AppleAppReceipt? receipt);
    }
}
