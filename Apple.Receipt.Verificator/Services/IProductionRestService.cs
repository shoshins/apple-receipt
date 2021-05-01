using System.Threading.Tasks;
using Apple.Receipt.Verificator.Models.IAPVerification;
using Refit;

namespace Apple.Receipt.Verificator.Services
{
    internal interface IProductionRestService: IRestService
    {
        [Post("")]
        new Task<IAPVerificationResponse?> ValidateAppleReceiptAsync(IAPVerificationRequest request);
    }
}
