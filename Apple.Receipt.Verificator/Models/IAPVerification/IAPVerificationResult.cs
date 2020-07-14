using Apple.Receipt.Models;
using Newtonsoft.Json;
using System;

namespace Apple.Receipt.Verificator.Models.IAPVerification
{
    internal class IAPVerificationResult
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        public IapVerificationResultStatus StatusCode
        {
            get
            {
                IapVerificationResultStatus iapStatus;
                if (Status > 21101 || !Enum.TryParse(Status.ToString(), out iapStatus))
                {
                    iapStatus = IapVerificationResultStatus.InternalError;
                }
                return iapStatus;
            }
        }

        [JsonProperty("environment")]
        public string Environment { get; set; }

        [JsonProperty("receipt")]
        public AppleAppReceipt? Receipt { get; set; }
    }
}
