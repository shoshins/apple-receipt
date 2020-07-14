using Apple.Receipt.Verificator.Models;
using System.Threading.Tasks;

namespace Apple.Receipt.Verificator.Services
{
    public interface IAppleReceiptVerificatorService
    {
        Task<AppleReceiptVerificationResult> VerifyAppleReceiptAsync(string receiptData);
    }
}