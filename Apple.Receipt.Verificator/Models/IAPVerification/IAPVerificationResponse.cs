using System;
using Apple.Receipt.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Apple.Receipt.Verificator.Models.IAPVerification
{
    /// <summary>
    /// The data returned in the response from the App Store.
    /// According to the https://developer.apple.com/documentation/appstorereceipts/responsebody/>
    /// </summary>
    public class IAPVerificationResponse
    {
        /// <summary>
        /// Either 0 if the receipt is valid, or a status code if there is an error.
        /// The status code reflects the status of the app receipt as a whole.
        /// See <see cref="IAPVerificationResponseStatus"/> for more details.
        /// Enum representation of the <see cref="StatusCode"/>
        /// </summary>
        public IAPVerificationResponseStatus Status {
            get
            {
                if (StatusCode > 21101 || !Enum.TryParse(Status.ToString(), out IAPVerificationResponseStatus iapStatus))
                {
                    iapStatus = IAPVerificationResponseStatus.InternalError;
                }
                return iapStatus;
            }
        }
        
        /// <summary>
        /// Numeric representation of the <see cref="Status"/>
        /// </summary>
        [JsonProperty("status")]
        public int StatusCode;

        /// <summary>
        /// The environment for which the receipt was generated.
        /// Possible values: Sandbox, Production
        /// </summary>
        [JsonProperty("environment")]
        public string Environment { get; set; }
        /// <summary>
        /// An indicator that an error occurred during the request.
        /// A value of TRUE indicates a temporary issue; retry validation for this receipt at a later time.
        /// A value of FALSE indicates an unresolvable issue; do not retry validation for this receipt.
        /// Only applicable to status codes 21100-21199.
        /// </summary>
        [JsonProperty("is-retryable")]
        public bool? IsRetryable { get; set; }
        /// <summary>
        /// A <see cref="AppleAppReceipt"/> representation of the receipt that was sent for verification.
        /// </summary>
        [JsonProperty("receipt")]
        public AppleAppReceipt? Receipt { get; set; }
        /// <summary>
        /// An array that contains all in-app purchase transactions.
        /// List of <see cref="AppleAppReceipt"/>
        /// This excludes transactions for consumable products that have been marked as finished by your app.
        /// Only returned for receipts that contain auto-renewable subscriptions.
        /// </summary>
        [JsonProperty("latest_receipt_info")]
        public List<AppleInAppPurchaseReceipt>? LatestReceiptInfo { get; set; }
        // /// <summary>
        // /// The latest app receipt (decoded from <see cref="LatestReceiptEncoded"/> into <see cref="AppleAppReceipt"/>).
        // /// Only returned for receipts that contain auto-renewable subscriptions.
        // /// </summary>
        // public AppleInAppPurchaseReceipt? LatestReceiptDecoded { get; set; }
        /// <summary>
        /// The latest app receipt represented as Base64 string.
        /// Only returned for receipts that contain auto-renewable subscriptions.
        /// </summary>
        [JsonProperty("latest_receipt")]
        public string? LatestReceiptEncoded { get; set; }
        [JsonProperty("pending_renewal_info")]
        public List<AppleInAppPurchaseReceipt>? PendingRenewalInfo { get; set; }
    }
}
