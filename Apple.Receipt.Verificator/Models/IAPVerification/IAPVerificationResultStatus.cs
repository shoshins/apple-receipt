namespace Apple.Receipt.Verificator.Models.IAPVerification
{
    /// <summary>
    ///     Verify Receipt Result types enumeration
    /// </summary>
    public enum IapVerificationResultStatus
    {
        /// <summary>
        ///     Internal Verification. Something went wrong due verification process.
        /// </summary>
        InternalVerificationBroken = -102,

        /// <summary>
        ///     Internal Verification. Internal Verification failed.
        /// </summary>
        InternalVerificationFailed = -101,

        /// <summary>
        ///     Internal Verification. Wrong argument
        /// </summary>
        WrongArgument = -100,

        /// <summary>
        ///     Everything is OK
        /// </summary>
        Ok = 0,

        /// <summary>
        ///     The App Store could not read the JSON object you provided.
        /// </summary>
        BadJson = 21000,

        /// <summary>
        ///     The data in the receipt-data property was malformed or missing.
        /// </summary>
        EmptyReceipt = 21002,

        /// <summary>
        ///     The receipt could not be authenticated.
        /// </summary>
        NotAuthenticatedReceipt = 21003,

        /// <summary>
        ///     The shared secret you provided does not match the shared secret on file for your account.
        /// </summary>
        SharedSecretWrong = 21004,

        /// <summary>
        ///     The receipt server is not currently available.
        /// </summary>
        Unavailable = 21005,

        /// <summary>
        ///     This receipt is valid but the subscription has expired. When this status code is returned to your server, the
        ///     receipt data is also decoded and returned as part of the response.
        ///     Only returned for iOS 6 style transaction receipts for auto-renewable subscriptions.
        /// </summary>
        SubscriptionExpired = 21006,

        /// <summary>
        ///     This receipt is from the test environment, but it was sent to the production environment for verification. Send it
        ///     to the test environment instead.
        /// </summary>
        TestReceiptOnProd = 21007,

        /// <summary>
        ///     This receipt is from the production environment, but it was sent to the test environment for verification. Send it
        ///     to the production environment instead.
        /// </summary>
        ProdReceiptOnTest = 21008,

        /// <summary>
        ///     This receipt could not be authorized. Treat this the same as if a purchase was never made.
        /// </summary>
        NotAuthorizedReceipt = 21010,

        /// <summary>
        ///     Internal data access error.
        /// </summary>
        InternalError = 21101
    }
}