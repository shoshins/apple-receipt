using Newtonsoft.Json;

namespace Apple.Receipt.Verificator.Models.IAPVerification
{
    internal class IAPVerificationRequest
    {
        public IAPVerificationRequest(string receiptData, string password)
        {
            ReceiptData = receiptData;
            Password = password;
        }

        [JsonProperty(PropertyName = "receipt-data")]
        public string ReceiptData { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
