using Apple.Receipt.Verificator.Models;

namespace Apple.Receipt.Verificator.Services
{
    public interface IAppleReceiptVerificatorService
    {
        Task<AppleReceiptVerificationResult> VerifyAppleReceiptAsync(string receiptData);
        Task<AppleReceiptVerificationResult> VerifyAppleProductionReceiptAsync(string receiptData);
        Task<AppleReceiptVerificationResult> VerifyAppleSandBoxReceiptAsync(string receiptData);
    }
}