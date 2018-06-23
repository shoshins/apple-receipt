using System.Threading.Tasks;
using Apple.Receipt.Verificator.Models;
using Futhark.Gyfu.Data.Models.Checkout.Apple.Verification;
using Refit;

namespace Apple.Receipt.Verificator.Services
{
    public interface IRestService
    {
        [Post("")]
        Task<IAPVerificationResult> ValidateAppleReceiptAsync(IAPVerificationRequest request);
    }
}