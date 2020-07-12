using System.Collections.Generic;

namespace Apple.Receipt.Verificator.Models
{
    public class AppleReceiptVerificationSettings
    {
        public string VerifyReceiptSharedSecret { get; set; } = null!;

        public AppleReceiptVerificationType VerificationType { get; set; }

        public ICollection<string> AllowedBundleIds { get; set; } = null!;

        public string VerifyUrl => VerificationType == AppleReceiptVerificationType.Production
            ? ProductionUrl
            : SandboxUrl;

        public string ProductionUrl { get; set; } = "https://sandbox.itunes.apple.com/verifyReceipt";

        public string SandboxUrl { get; set; } = "https://sandbox.itunes.apple.com/verifyReceipt";
    }
}
