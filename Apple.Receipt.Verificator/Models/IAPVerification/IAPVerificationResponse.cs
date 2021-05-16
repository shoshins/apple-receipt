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
        [JsonProperty("status")]
        public int Status { get; set; }

        public IAPVerificationResponseStatus StatusCode
        {
            get
            {
                if (Status > 21101 || !Enum.TryParse(Status.ToString(), out IAPVerificationResponseStatus iapStatus))
                {
                    iapStatus = IAPVerificationResponseStatus.InternalError;
                }
                return iapStatus;
            }
        }

        [JsonProperty("environment")]
        public string Environment { get; set; }

        [JsonProperty("receipt")]
        public AppleAppReceipt? Receipt { get; set; }

        /// <summary>
        /// An indicator that an error occurred during the request.
        /// A value of TRUE indicates a temporary issue; retry validation for this receipt at a later time.
        /// A value of FALSE indicates an unresolvable issue; do not retry validation for this receipt.
        /// Only applicable to status codes 21100-21199.
        /// </summary>
        [JsonProperty("is-retryable")]
        public bool? IsRetryable { get; set; }

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

        /// <summary>
        /// An array that contains all in-app purchase transactions.
        /// List of <see cref="AppleInAppPurchaseReceipt"/>
        /// This excludes transactions for consumable products that have been marked as finished by your app.
        /// Only returned for receipts that contain auto-renewable subscriptions.
        /// </summary>
        [JsonProperty("latest_receipt_info")]
        [JsonConverter(typeof(ObjectOrArrayToArrayConverter<AppleInAppPurchaseReceipt>))]
        public ICollection<AppleInAppPurchaseReceipt>? LatestReceiptInfo { get; set; }

        /// <summary>
        /// An array where each element contains the pending renewal information for each auto-renewable subscription identified by the product_id.
        /// List of <see cref="AppleInAppPurchaseReceipt"/>
        /// Only returned for app receipts that contain auto-renewable subscriptions.
        /// </summary>
        [JsonProperty("pending_renewal_info")]
        public ICollection<AppleInAppPurchaseReceipt>? PendingRenewalInfo { get; set; }
    }
}
