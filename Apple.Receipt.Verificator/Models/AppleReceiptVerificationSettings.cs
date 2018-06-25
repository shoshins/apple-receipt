using System.Collections.Generic;
using Serilog;

namespace Apple.Receipt.Verificator.Models
{
    public class AppleReceiptVerificationSettings
    {
        public AppleReceiptVerificationSettings(string verifyReceiptSharedSecret, AppleReceiptVerificationType verificationType,
            ICollection<string> allowedBundleIds, LoggerConfiguration loggerConfiguration, bool registerLogger)
        {
            VerifyReceiptSharedSecret = verifyReceiptSharedSecret;
            VerificationType = verificationType;
            AllowedBundleIds = allowedBundleIds;
            LoggerConfiguration = loggerConfiguration;
            RegisterLogger = registerLogger;
        }

        public string VerifyReceiptSharedSecret { get; set; }
        public AppleReceiptVerificationType VerificationType { get; set; }
        public ICollection<string> AllowedBundleIds { get; set; }
        public LoggerConfiguration LoggerConfiguration { get; set; }
        public bool RegisterLogger { get; set; }
    }
}