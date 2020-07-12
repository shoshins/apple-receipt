using System.Threading.Tasks;
using Apple.Receipt.Verificator.Models.IAPVerification;
using Refit;

namespace Apple.Receipt.Verificator.Services
{
    internal interface IRestService
    {
        [Post("")]
        Task<IAPVerificationResult?> ValidateAppleReceiptAsync(IAPVerificationRequest request);
    }
}
