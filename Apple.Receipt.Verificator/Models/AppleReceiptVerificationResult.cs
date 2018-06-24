using Apple.Receipt.Models;
using Apple.Receipt.Verificator.Models.IAPVerification;

namespace Apple.Receipt.Verificator.Models
{
    public class AppleReceiptVerificationResult
    {
        public AppleReceiptVerificationResult(string errorMessage, AppleReceiptVerificationStatuses status,
            IapVerificationResultStatus iapVerificationStatus, AppleAppReceipt receipt)
        {
            ErrorMessage = errorMessage;
            Status = status;
            IAPVerificationStatus = iapVerificationStatus;
            Receipt = receipt;
        }

        public AppleReceiptVerificationResult(string errorMessage, AppleReceiptVerificationStatuses status)
        {
            ErrorMessage = errorMessage;
            Status = status;
            IAPVerificationStatus = null;
            Receipt = null;
        }

        public AppleReceiptVerificationResult(IapVerificationResultStatus iapVerificationStatus,
            AppleAppReceipt receipt, AppleReceiptVerificationStatuses status = AppleReceiptVerificationStatuses.Ok,
            string errorMessage = "")
        {
            IAPVerificationStatus = iapVerificationStatus;
            Receipt = receipt;
            Status = status;
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
        public AppleReceiptVerificationStatuses Status { get; set; }
        public IapVerificationResultStatus? IAPVerificationStatus { get; set; }
        public AppleAppReceipt Receipt { get; set; }
    }
}