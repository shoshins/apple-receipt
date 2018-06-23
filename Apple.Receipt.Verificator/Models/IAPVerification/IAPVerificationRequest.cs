using Newtonsoft.Json;

namespace Futhark.Gyfu.Data.Models.Checkout.Apple.Verification
{
    public class IAPVerificationRequest
    {
        [JsonProperty(PropertyName = "receipt-data")]
        public string ReceiptData { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        public IAPVerificationRequest(string receiptData, string password)
        {
            ReceiptData = receiptData;
            Password = password;
        }
    }
}
