using System;
using Newtonsoft.Json;

namespace AppleReceiptParser.Models
{
    public class AppleInAppPurchaseReceipt
    {
        [JsonProperty("quantity")]
        public string Quantity { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }

        [JsonProperty("original_transaction_id")]
        public string OriginalTransactionId { get; set; }

        [JsonProperty("purchase_date")]
        public string PurchaseDate { get; set; }

        [JsonProperty("purchase_date_ms")]
        public string PurchaseDateMs { get; set; }

        [JsonProperty("purchase_date_pst")]
        public string PurchaseDatePst { get; set; }

        [JsonProperty("original_purchase_date")]
        public string OriginalPurchaseDate { get; set; }

        [JsonProperty("original_purchase_date_ms")]
        public string OriginalPurchaseDateMs { get; set; }

        [JsonProperty("original_purchase_date_pst")]
        public string OriginalPurchaseDatePst { get; set; }

        [JsonProperty("is_trial_period")]
        public string IsTrialPeriod { get; set; }

        public DateTime CancellationDate { get; set; }

        #region Parsed dates objects

        public DateTime? PurchaseDateDt => MillisecondsToDate(PurchaseDateMs);

        public DateTime? OriginalPurchaseDateDt => MillisecondsToDate(OriginalPurchaseDateMs);
        public int WebOrderLineItemId { get; set; }
        public DateTime SubscriptionExpirationDate { get; set; }

        #endregion

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
    }
}
