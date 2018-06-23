using System.Collections.Generic;
using Serilog;

namespace Apple.Receipt.Verificator.Models
{
    public class AppleReceiptVerificationSettings
    {
        public string VerifyReceiptSharedSecret { get; set; }
        public string VerifyReceiptUrl { get; set; }
        public ICollection<string> AllowedBundleIds { get; set; }
        public LoggerConfiguration LoggerConfiguration { get; set; }
        public bool RegisterLogger { get; set; }

        public AppleReceiptVerificationSettings(string verifyReceiptSharedSecret, string verifyReceiptUrl, ICollection<string> allowedBundleIds, LoggerConfiguration loggerConfiguration, bool registerLogger)
        {
            VerifyReceiptSharedSecret = verifyReceiptSharedSecret;
            VerifyReceiptUrl = verifyReceiptUrl;
            AllowedBundleIds = allowedBundleIds;
            LoggerConfiguration = loggerConfiguration;
            RegisterLogger = registerLogger;
        }
    }
}