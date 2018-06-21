using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AppleReceiptParser.Atn1;
using Newtonsoft.Json;

namespace AppleReceiptParser.Models
{
    public class AppleAppReceipt
    {
        public AppleAppReceipt()
        {
            PurchaseReceipts = new List<AppleInAppPurchaseReceipt>();
        }

        [JsonProperty("adam_id")]
        public int? AdamId { get; set; }

        [JsonProperty("app_item")]
        public int? AppItem { get; set; }

        [JsonProperty("application_version")]
        public string ApplicationVersion { get; set; }

        [JsonProperty("bundle_id")]
        public string BundleId { get; set; }

        [JsonProperty("download_id")]
        public int? DownloadId { get; set; }

        [JsonProperty("in_app")]
        public List<AppleInAppPurchaseReceipt> PurchaseReceipts { get; set; }

        [JsonProperty("original_application_version")]
        public string OriginalApplicationVersion { get; set; }

        [JsonProperty("original_purchase_date")]
        public string OriginalPurchaseDate { get; set; }

        [JsonProperty("original_purchase_date_ms")]
        public string OriginalPurchaseDateMs { get; set; }

        [JsonProperty("original_purchase_date_pst")]
        public string OriginalPurchaseDatePst { get; set; }

        [JsonProperty("receipt_creation_date")]
        public string ReceiptCreationDate { get; set; }

        [JsonProperty("receipt_creation_date_ms")]
        public string ReceiptCreationDateMs { get; set; }

        [JsonProperty("receipt_creation_date_pst")]
        public string ReceiptCreationDatePst { get; set; }

        [JsonProperty("request_type")]
        public string RequestType { get; set; }

        [JsonProperty("request_date")]
        public string RequestDate { get; set; }

        [JsonProperty("request_date_ms")]
        public string RequestDateMs { get; set; }

        [JsonProperty("request_date_pst")]
        public string RequestDatePst { get; set; }

        [JsonProperty("version_external_identifier")]
        public int VersionExternalIdentifier { get; set; }

        #region Parsed dates objects

        public DateTime? ReceiptCreationDateDt => MillisecondsToDate(ReceiptCreationDateMs);

        public DateTime? RequestDateDt => MillisecondsToDate(RequestDateMs);

        public DateTime? OriginalPurchaseDateDt => MillisecondsToDate(OriginalPurchaseDateMs);

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
            return dt;
        }

        #endregion
    }
}
