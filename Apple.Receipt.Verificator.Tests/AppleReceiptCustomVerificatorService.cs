using Apple.Receipt.Models;
using Apple.Receipt.Verificator.Models;
using Apple.Receipt.Verificator.Models.IAPVerification;
using Apple.Receipt.Verificator.Services;

namespace Apple.Receipt.Verificator.Tests
{
    public class AppleReceiptCustomVerificatorService : IAppleReceiptCustomVerificatorService
    {
        public AppleReceiptVerificationResult ValidateReceipt(AppleAppReceipt receipt)
        {
            return new AppleReceiptVerificationResult("Everything is Ok.", IapVerificationResultStatus.Ok);
        }
    }
}