using System;
using System.Collections;
using System.Collections.Generic;
using Apple.Receipt.Models;
using Apple.Receipt.Verificator.Models.IAPVerification;
using Newtonsoft.Json;

namespace Apple.Receipt.Verificator.Models
{
    public class AppleReceiptVerificationResult
    {
        #region .ctors

        /// <summary>
        /// Creates an empty instance of the <see cref="AppleReceiptVerificationResult"/>
        /// </summary>
        [JsonConstructor]
        public AppleReceiptVerificationResult() { }

        /// <summary>
        /// Creates an instance of the <see cref="AppleReceiptVerificationResult"/>
        /// with Detailed Apple Verification Response (<param name="verificationResponse"></param>)
        /// and verbal result description (<param name="errorMessage"></param>)
        /// </summary>
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
                // Backward compatibility.
#pragma warning disable 618
                Receipt = verificationResponse.Receipt;
#pragma warning restore 618
            } 
            else
            {
                Status = IAPVerificationResponseStatus.InternalVerificationFailed;
            }
        }
        
        /// <summary>
        /// Creates an instance of the <see cref="AppleReceiptVerificationResult"/>
        /// without Detailed Apple Verification Response.
        /// With Internal Verification Process Status (<param name="status"></param>)
        /// and verbal result description (<param name="errorMessage"></param>)
        /// </summary>
        public AppleReceiptVerificationResult(
            string errorMessage,
            IAPVerificationResponseStatus status
        )
        {
            Message = errorMessage;
            Status = status;
        }

        #endregion
        
        /// <summary>
        /// Represents Verification Process Results Verbal Description.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Represents Verification Process Status.
        /// Basically, represents the same status code as <see cref="AppleVerificationResponse"/> (StatusCode) does.
        /// But, it also extends basic Apple Response Status with Internal Verification Process Results.
        /// </summary>
        public IAPVerificationResponseStatus? Status { get; set; }
        
        /// <summary>
        /// Represents Apple Server Verification Response.
        /// If empty -> There is no Apple Response for some reasons.
        /// The reason may be checked in the <see cref="Status"/> property.
        /// </summary>
        public IAPVerificationResponse? AppleVerificationResponse { get; set; }

        #region Obsolete fields

        /// <summary>
        /// This field is obsolete. Please use the AppleVerificationResponse instead.
        /// <see cref="AppleVerificationResponse"/> has the same data (Receipt property) + full response data
        /// </summary>
        [Obsolete("This field is obsolete. Please use the AppleVerificationResponse instead. AppleVerificationResponse.Receipt has the same data + full response data")]
        public AppleAppReceipt? Receipt { get; set; }

        #endregion
    }
}
