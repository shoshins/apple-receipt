using System.Threading.Tasks;
using Apple.Receipt.Verificator.Models.IAPVerification;
using Refit;

namespace Apple.Receipt.Verificator.Services
{
    internal interface ISandboxRestService: IRestService
    {
        [Post("")]
        Task<IAPVerificationResponse?> ValidateAppleReceiptAsync(IAPVerificationRequest request);
    }
}
