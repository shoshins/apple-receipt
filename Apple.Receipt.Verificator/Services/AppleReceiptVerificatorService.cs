using System;
using System.Threading.Tasks;
using Apple.Receipt.Models;
using Apple.Receipt.Verificator.Models;
using AppleReceiptParser.Services;
using Futhark.Gyfu.Data.Models.Checkout.Apple.Verification;
using Serilog;

namespace Apple.Receipt.Verificator.Services
{
    public class AppleReceiptVerificatorService : IAppleReceiptVerificatorService
    {
        private readonly IAppleReceiptCustomVerificatorService _customValidation;
        private readonly ILogger _log;
        private readonly IAppleReceiptParserService _receiptParserService;
        private readonly IRestService _restService;
        private readonly AppleReceiptVerificationSettings _settings;

        public AppleReceiptVerificatorService(IRestService restService, ILogger log,
            IAppleReceiptParserService receiptParserService,
            AppleReceiptVerificationSettings settings,
            IAppleReceiptCustomVerificatorService customValidation)
        {
            _restService = restService;
            _log = log;
            _receiptParserService = receiptParserService;
            _settings = settings;
            _customValidation = customValidation;
        }

        public async Task<AppleReceiptVerificationResult> VerifyAppleReceiptAsync(string receiptData)
        {
            // 1. Validate incoming arguments
            if (string.IsNullOrEmpty(receiptData))
            {
                _log.Information("receiptData cannot be empty");
                return new AppleReceiptVerificationResult("receiptData cannot be empty",
                    AppleReceiptVerificationStatuses.WrongArgument);
            }
            // 2. Prevalidate Receipt (Optional)
            try
            {
                byte[] data = Convert.FromBase64String(receiptData);
                AppleAppReceipt receipt = _receiptParserService.GetAppleReceiptFromBytes(data);

                // a. Validate bundle ID
                if (receipt != null && !_settings.AllowedBundleIds.Contains(receipt.BundleId))
                {
                    _log.Information("Receipt has wrong bundle ID {bundle_id}", receipt.BundleId);
                    return new AppleReceiptVerificationResult($"Receipt has wrong bundle ID {receipt.BundleId}",
                        AppleReceiptVerificationStatuses.WrongArgument);
                }

                if (_customValidation != null)
                {
                    // if custom Validator implemented
                    AppleReceiptVerificationResult validationResult = _customValidation.ValidateReceipt(receipt);
                    if (validationResult == null || validationResult.Status != AppleReceiptVerificationStatuses.Ok)
                    {
                        // and custom validation doesn't passed - failed
                        return validationResult;
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error(e,
                    "Something went wrong in receipt prevalidation. Seems to be invalid receipt data. Skip this step.");
            }
            // 3. Validate Receipt in Apple (verification in IAP)
            try
            {
                _log.Debug("Start receipt verification in IAP...");
                IAPVerificationRequest request =
                    new IAPVerificationRequest(receiptData, _settings.VerifyReceiptSharedSecret);
                IAPVerificationResult iapVerificationResult =
                    await _restService.ValidateAppleReceiptAsync(request).ConfigureAwait(false);
                if (iapVerificationResult == null)
                {
                    return new AppleReceiptVerificationResult("IAP receipt verification failed",
                        AppleReceiptVerificationStatuses.IAPVerificationFailed);
                }
                IapVerificationResultStatus iapStatus = iapVerificationResult.StatusCode;
                // 1.If status <> 0 - failed
                if (iapStatus != IapVerificationResultStatus.Ok)
                {
                    return new AppleReceiptVerificationResult("IAP receipt verification failed",
                        AppleReceiptVerificationStatuses.IAPVerificationFailed, iapStatus,
                        iapVerificationResult.Receipt);
                }

                // 2. If there is no information about receipt - failed.
                if (iapVerificationResult.Receipt == null)
                {
                    _log.Information("IAP Receipt Verification failed due empty receipt");
                    return new AppleReceiptVerificationResult("IAP Receipt Verification failed due empty receipt.",
                        AppleReceiptVerificationStatuses.IAPVerificationFailed);
                }

                _log.Information("IAPReceipt Verification passed.");
                return new AppleReceiptVerificationResult(iapVerificationResult.StatusCode,
                    iapVerificationResult.Receipt);
            }
            catch (Exception e)
            {
                _log.Error(e, "Something went wrong in IAP receipt verification");
                return new AppleReceiptVerificationResult("Something went wrong in IAP receipt verification",
                    AppleReceiptVerificationStatuses.IAPVerificationFailed);
            }
        }
    }
}