using Apple.Receipt.Models;
using Apple.Receipt.Verificator.Models.IAPVerification;

namespace Apple.Receipt.Verificator.Models
{
    public class AppleReceiptVerificationResult
    {
        public AppleReceiptVerificationResult(string errorMessage, IapVerificationResultStatus status,
            AppleAppReceipt receipt)
        {
            Message = errorMessage;
            Status = status;
            Receipt = receipt;
        }

        public AppleReceiptVerificationResult(string message, IapVerificationResultStatus status)
        {
            Message = message;
            Status = status;
            Receipt = null;
        }

        public AppleReceiptVerificationResult(IapVerificationResultStatus status,
            AppleAppReceipt receipt,
            string message)
        {
            Receipt = receipt;
            Status = status;
            Message = message;
        }

        public string Message { get; set; }
        public IapVerificationResultStatus? Status { get; set; }
        public AppleAppReceipt Receipt { get; set; }
    }
}