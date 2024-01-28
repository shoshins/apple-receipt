using System.Collections.Generic;

namespace Apple.Receipt.Verificator.Models
{
    public class AppStoreReceiptVerifierSettings
    {
        public string? VerifyReceiptSharedSecret { get; set; }

        public AppleReceiptVerificationType VerificationType { get; set; }

        public ICollection<string> AllowedBundleIds { get; set; }

        public string VerifyUrl => VerificationType == AppleReceiptVerificationType.Production
            ? ProductionUrl
            : SandboxUrl;

        public string ProductionUrl { get; set; } = "https://buy.itunes.apple.com/verifyReceipt";

        public string SandboxUrl { get; set; } = "https://sandbox.itunes.apple.com/verifyReceipt";
    }
}
