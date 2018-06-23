using System.Threading.Tasks;
using Apple.Receipt.Verificator.Models;

namespace Apple.Receipt.Verificator.Services
{
    public interface IAppleReceiptVerificatorService
    {
        Task<AppleReceiptVerificationResult> VerifyAppleReceiptAsync(string receiptData);
    }
}