using System;
using Apple.Receipt.Models.Enums;
using Newtonsoft.Json;

namespace Apple.Receipt.Models
{
    public class AppleInAppPurchaseReceipt
    {
        /// <summary>
        ///     The number of items purchased.
        /// </summary>
        /// <remarks>
        ///     This value corresponds to the quantity property of the SKPayment object stored in the transaction’s payment
        ///     property.
        /// </remarks>
        [JsonProperty("quantity")]
        public string Quantity { get; set; }

        /// <summary>
        ///     The product identifier of the item that was purchased.
        /// </summary>
        /// <remarks>
        ///     This value corresponds to the productIdentifier property of the SKPayment object stored in the transaction’s
        ///     payment property.
        /// </remarks>
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        /// <summary>
        ///     The transaction identifier of the item that was purchased.
        /// </summary>
        /// <remarks>
        ///     For a transaction that restores a previous transaction, this value is different from the transaction identifier of
        ///     the original purchase transaction. In an auto-renewable subscription receipt, a new value for the transaction
        ///     identifier is generated every time the subscription automatically renews or is restored on a new device.
        /// </remarks>
        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }

        /// <summary>
        ///     For a transaction that restores a previous transaction, the transaction identifier of the original transaction.
        ///     Otherwise, identical to the transaction identifier.
        /// </summary>
        /// <remarks>
        ///     This value is the same for all receipts that have been generated for a specific subscription. This value is useful
        ///     for relating together multiple iOS 6 style transaction receipts for the same individual customer’s subscription.
        /// </remarks>
        [JsonProperty("original_transaction_id")]
        public string OriginalTransactionId { get; set; }

        /// <summary>
        ///     The date and time that the item was purchased.
        /// </summary>
        /// <remarks>
        ///     For a transaction that restores a previous transaction, the purchase date is the same as the original purchase
        ///     date. Use Original Purchase Date to get the date of the original transaction.
        /// </remarks>
        /// <remarks>
        ///     In an auto-renewable subscription receipt, the purchase date is the date when the subscription was either purchased
        ///     or renewed (with or without a lapse). For an automatic renewal that occurs on the expiration date of the current
        ///     period, the purchase date is the start date of the next period, which is identical to the end date of the current
        ///     period.
        /// </remarks>
        [JsonProperty("purchase_date")]
        public string PurchaseDate { get; set; }

        /// <summary>
        ///     The same like <see cref="PurchaseDate" /> but in unix epoch milliseconds.
        /// </summary>
        [JsonProperty("purchase_date_ms")]
        public string PurchaseDateMs { get; set; }

        /// <summary>
        ///     The same like <see cref="PurchaseDate" /> but in PST timezone.
        /// </summary>
        [JsonProperty("purchase_date_pst")]
        public string PurchaseDatePst { get; set; }

        /// <summary>
        ///     For a transaction that restores a previous transaction, the date of the original transaction.
        /// </summary>
        /// <remarks>
        ///     In an auto-renewable subscription receipt, this indicates the beginning of the subscription period, even if the
        ///     subscription has been renewed.
        /// </remarks>
        [JsonProperty("original_purchase_date")]
        public string OriginalPurchaseDate { get; set; }

        /// <summary>
        ///     The same like <see cref="OriginalPurchaseDate" /> but in unix epoch milliseconds.
        /// </summary>
        [JsonProperty("original_purchase_date_ms")]
        public string OriginalPurchaseDateMs { get; set; }

        /// <summary>
        ///     The same like <see cref="OriginalPurchaseDate" /> but in PST timezone.
        /// </summary>
        [JsonProperty("original_purchase_date_pst")]
        public string OriginalPurchaseDatePst { get; set; }

        /// <summary>
        ///     The expiration date for the subscription.
        /// </summary>
        /// <remarks>
        ///     This key is only present for auto-renewable subscription receipts. Use this value to identify the date when the
        ///     subscription will renew or expire, to determine if a customer should have access to content or service. After
        ///     validating the latest receipt, if the subscription expiration date for the latest renewal transaction is a past
        ///     date, it is safe to assume that the subscription has expired.
        /// </remarks>
        [JsonProperty("expires_date")]
        public string ExpirationDate { get; set; }

        /// <summary>
        ///     The same like <see cref="ExpirationDate" /> but in unix epoch milliseconds.
        /// </summary>
        [JsonProperty("expires_date_ms")]
        public string ExpirationDateMs { get; set; }

        /// <summary>
        ///     The same like <see cref="ExpirationDate" /> but in PST timezone.
        /// </summary>
        [JsonProperty("expires_date_pst")]
        public string ExpirationDatePst { get; set; }

        /// <summary>
        ///     For an expired subscription, the reason for the subscription expiration.
        ///     More info in <see cref="AppleExpirationIntent" />
        /// </summary>
        /// <remarks>
        ///     This key is only present for a receipt containing an expired auto-renewable subscription. You can use this value to
        ///     decide whether to display appropriate messaging in your app for customers to resubscribe.
        /// </remarks>
        [JsonProperty("expiration_intent")]
        public AppleExpirationIntent ExpirationIntent { get; set; }

        /// <summary>
        ///     For an expired subscription, whether or not Apple is still attempting to automatically renew the subscription.
        ///     More info in <see cref="AppleBillingRetryPeriod" />
        /// </summary>
        /// <remarks>
        ///     This key is only present for auto-renewable subscription receipts. If the customer’s subscription failed to renew
        ///     because the App Store was unable to complete the transaction, this value will reflect whether or not the App Store
        ///     is still trying to renew the subscription.
        /// </remarks>
        [JsonProperty("is_in_billing_retry_period")]
        public AppleBillingRetryPeriod IsInBillingRetryPeriod { get; set; }

        /// <summary>
        ///     For a subscription, whether or not it is in the free trial period.
        /// </summary>
        /// <remarks>
        ///     This key is only present for auto-renewable subscription receipts. The value for this key is "true" if the
        ///     customer’s subscription is currently in the free trial period, or "false" if not.
        /// </remarks>
        /// <remarks>
        ///     Note: If a previous subscription period in the receipt has the value “true” for either the is_trial_period or the
        ///     is_in_intro_offer_period key, the user is not eligible for a free trial or introductory price within that
        ///     subscription group.
        /// </remarks>
        [JsonProperty("is_trial_period")]
        public bool IsTrialPeriod { get; set; }

        /// <summary>
        ///     For an auto-renewable subscription, whether or not it is in the introductory price period.
        /// </summary>
        /// <remarks>
        ///     This key is only present for auto-renewable subscription receipts. The value for this key is "true" if the
        ///     customer’s subscription is currently in an introductory price period, or "false" if not.
        /// </remarks>
        /// <remarks>
        ///     Note: If a previous subscription period in the receipt has the value “true” for either the is_trial_period or the
        ///     is_in_intro_offer_period key, the user is not eligible for a free trial or introductory price within that
        ///     subscription group.
        /// </remarks>
        [JsonProperty("is_in_intro_offer_period")]
        public bool IsInIntroOfferPeriod { get; set; }

        /// <summary>
        ///     For a transaction that was canceled by Apple customer support, the time and date of the cancellation.
        ///     For an auto-renewable subscription plan that was upgraded, the time and date of the upgrade transaction.
        /// </summary>
        /// <remarks>
        ///     Treat a canceled receipt the same as if no purchase had ever been made.
        /// </remarks>
        /// <remarks>
        ///     Note: A canceled in-app purchase remains in the receipt indefinitely. Only applicable if the refund was for a
        ///     non-consumable product, an auto-renewable subscription, a non-renewing subscription, or for a free subscription.
        /// </remarks>
        [JsonProperty("cancellation_date")]
        public DateTime CancellationDate { get; set; }

        /// <summary>
        ///     For a transaction that was canceled, the reason for cancellation.
        ///     More info in <see cref="AppleCancellationReason" />.
        /// </summary>
        /// <remarks>
        ///     Use this value along with the cancellation date to identify possible issues in your app that may lead customers to
        ///     contact Apple customer support.
        /// </remarks>
        [JsonProperty("cancellation_reason")]
        public AppleCancellationReason CancellationReason { get; set; }

        /// <summary>
        ///     A string that the App Store uses to uniquely identify the application that created the transaction.
        /// </summary>
        /// <remarks>
        ///     If your server supports multiple applications, you can use this value to differentiate between them.
        /// </remarks>
        /// <remarks>
        ///     Apps are assigned an identifier only in the production environment, so this key is not present for receipts created
        ///     in the test environment.
        /// </remarks>
        /// <remarks>
        ///     This field is not present for Mac apps.
        /// </remarks>
        /// <remarks>
        ///     See also <seealso cref="AppleAppReceipt.BundleId" />
        /// </remarks>
        [JsonProperty("app_item_id")]
        public string AppItemId { get; set; }

        /// <summary>
        ///     The primary key for identifying subscription purchases.
        /// </summary>
        /// <remarks>
        ///     This value is a unique ID that identifies purchase events across devices, including subscription renewal purchase
        ///     events.
        /// </remarks>
        [JsonProperty("web_order_line_item_id")]
        public string WebOrderLineItemId { get; set; }

        /// <summary>
        ///     The current renewal status for the auto-renewable subscription.
        ///     More info in <see cref="AppleSubscriptionAutoRenewStatus" />.
        /// </summary>
        /// <remarks>
        ///     This key is only present for auto-renewable subscription receipts, for active or expired subscriptions.
        /// </remarks>
        /// <remarks>
        ///     The value for this key should not be interpreted as the customer’s subscription status. You can use this value to
        ///     display an alternative subscription product in your app, for example, a lower level subscription plan that the
        ///     customer can downgrade to from their current plan.
        /// </remarks>
        [JsonProperty("auto_renew_status")]
        public AppleSubscriptionAutoRenewStatus SubscriptionAutoRenewStatus { get; set; }

        /// <summary>
        ///     The current renewal preference for the auto-renewable subscription.
        /// </summary>
        /// <remarks>
        ///     This key is only present for auto-renewable subscription receipts.
        /// </remarks>
        /// <remarks>
        ///     The value for this key corresponds to the productIdentifier property of the product that the customer’s
        ///     subscription renews. You can use this value to present an alternative service level to the customer before the
        ///     current subscription period ends.
        /// </remarks>
        [JsonProperty("auto_renew_product_id")]
        public string SubscriptionRenewProductId { get; set; }


        /// <summary>
        ///     The current price consent status for a subscription price increase.
        /// </summary>
        /// <remarks>
        ///     This key is only present for auto-renewable subscription receipts if the subscription price was increased without
        ///     keeping the existing price for active subscribers.
        /// </remarks>
        /// <remarks>
        ///     You can use this value to track customer adoption of the new price and take appropriate action.
        /// </remarks>
        [JsonProperty("price_consent_status")]
        public AppleSubscriptionPriceConsentStatus SubscriptionPriceConsentStatus { get; set; }

        #region Internal things

        private DateTime? MillisecondsToDate(string millisecondsString)
        {
            if (string.IsNullOrEmpty(millisecondsString))
            {
                return null;
            }

            long milliseconds;

            if (!long.TryParse(millisecondsString, out milliseconds))
            {
                return null;
            }
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(milliseconds);
            return dt.ToLocalTime();
        }

        #endregion

        #region Parsed dates objects

        public DateTime? PurchaseDateDt => MillisecondsToDate(PurchaseDateMs);

        public DateTime? OriginalPurchaseDateDt => MillisecondsToDate(OriginalPurchaseDateMs);

        #endregion
    }
}