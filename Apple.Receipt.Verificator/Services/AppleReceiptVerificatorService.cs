using Apple.Receipt.Parser.Services;
using Apple.Receipt.Verificator.Models;
using Apple.Receipt.Verificator.Models.IAPVerification;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Apple.Receipt.Verificator.Services
{
    internal class AppleReceiptVerificatorService : IAppleReceiptVerificatorService
    {
        private readonly IRestService _restService;
        private readonly ILogger _logger;
        private readonly IAppleReceiptParserService _receiptParserService;
        private readonly IOptionsSnapshot<AppleReceiptVerificationSettings> _settings;
        private readonly IAppleReceiptCustomVerificatorService? _customValidation;

        public AppleReceiptVerificatorService(
            IRestService restService,
            ILogger<AppleReceiptVerificatorService> logger,
            IAppleReceiptParserService receiptParserService,
            IOptionsSnapshot<AppleReceiptVerificationSettings> settings,
            IAppleReceiptCustomVerificatorService? customValidation = null
        )
        {
            _restService = restService;
            _logger = logger;
            _receiptParserService = receiptParserService;
            _settings = settings;
            _customValidation = customValidation;
        }

        public async Task<AppleReceiptVerificationResult?> VerifyAppleReceiptAsync(string receiptData)
        {
            // 1. Validate incoming arguments
            if (string.IsNullOrEmpty(receiptData))
            {
                _logger.LogInformation("receiptData cannot be empty");

                return new AppleReceiptVerificationResult(
                    "receiptData cannot be empty",
                    IAPVerificationResponseStatus.WrongArgument
                );
            }

            // 2. Prevalidate Receipt (Optional)
            try
            {
                var data = Convert.FromBase64String(receiptData);
                var receipt = _receiptParserService.GetAppleReceiptFromBytes(data);

                // a. Validate bundle ID
                if (receipt != null && !_settings.Value.AllowedBundleIds.Contains(receipt.BundleId))
                {
                    _logger.LogInformation("Receipt has wrong bundle ID {bundle_id}", receipt.BundleId);

                    return new AppleReceiptVerificationResult(
                        $"Receipt has wrong bundle ID {receipt.BundleId}",
                        IAPVerificationResponseStatus.WrongArgument
                    );
                }

                if (_customValidation != null)
                {
                    // if custom Validator implemented
                    var validationResult = _customValidation.ValidateReceipt(receipt);

                    if (validationResult == null || validationResult.Status != IAPVerificationResponseStatus.Ok)
                    {
                        // and custom validation doesn't passed - failed
                        return validationResult;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong in receipt prevalidation. Seems to be invalid receipt data. Skip this step.");
            }

            // 3. Validate Receipt in Apple (verification in IAP)
            try
            {
                _logger.LogDebug("Start receipt verification in IAP...");
                var request = new IAPVerificationRequest(receiptData, _settings.Value.VerifyReceiptSharedSecret);
                IAPVerificationResponse iapVerificationResult = await _restService.ValidateAppleReceiptAsync(request).ConfigureAwait(false);

                if (iapVerificationResult == null)
                {
                    return new AppleReceiptVerificationResult(
                        "IAP receipt verification failed. Apple returned empty receipt.",
                        IAPVerificationResponseStatus.InternalVerificationFailed
                    );
                }

                var iapStatus = iapVerificationResult.StatusCode;
                // 1.If status <> 0 - failed
                if (iapStatus != IAPVerificationResponseStatus.Ok)
                {
                    return new AppleReceiptVerificationResult(
                        "IAP receipt verification failed",
                        iapVerificationResult
                    );
                }

                _logger.LogInformation("IAPReceipt Verification passed.");

                return new AppleReceiptVerificationResult(
                    "Everything is OK.",
                    iapVerificationResult
                );
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong in IAP receipt verification");

                return new AppleReceiptVerificationResult(
                    "Something went wrong in IAP receipt verification",
                    IAPVerificationResponseStatus.InternalVerificationBroken
                );
            }
        }
    }
}
