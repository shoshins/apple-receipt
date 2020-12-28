using System;
using System.Collections;
using System.Collections.Generic;
using Apple.Receipt.Models;
using Apple.Receipt.Verificator.Models.IAPVerification;

namespace Apple.Receipt.Verificator.Models
{
    public class AppleReceiptVerificationResult
    {
        public AppleReceiptVerificationResult(
            string errorMessage,
            IAPVerificationResponse verificationResponse
        )
        {
            Message = errorMessage;
            AppleVerificationResponse = verificationResponse;
            if (verificationResponse != null)
            {
                Status = verificationResponse.StatusCode;
                Receipt = verificationResponse.Receipt;
            }
        }
        public AppleReceiptVerificationResult(
            string errorMessage,
            IAPVerificationResponseStatus status
        )
        {
            Message = errorMessage;
            Status = status;
        }
        public string Message { get; set; }
        public IAPVerificationResponse? AppleVerificationResponse { get; set; }

        #region Obsolete fields

        [Obsolete("This field is obsolete. Please use the AppleVerificationResult instead. AppleVerificationResult.Status has the same data + full response data")]
        public IAPVerificationResponseStatus? Status { get; set; }
        [Obsolete("This field is obsolete. Please use the AppleVerificationResult instead. AppleVerificationResult.Receipt has the same data + full response data")]
        public AppleAppReceipt? Receipt { get; set; }

        #endregion
    }
}
