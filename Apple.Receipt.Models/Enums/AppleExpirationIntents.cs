namespace Apple.Receipt.Models.Enums
{
    /// <summary>
    ///     For an expired subscription, the reason for the subscription expiration.
    /// </summary>
    /// <remarks>
    ///     This key is only present for a receipt containing an expired auto-renewable subscription. You can use this value to
    ///     decide whether to display appropriate messaging in your app for customers to resubscribe.
    /// </remarks>
    public enum AppleExpirationIntents
    {
        /// <summary>
        ///     “1” - Customer canceled their subscription.
        /// </summary>
        Cancelled = 1,

        /// <summary>
        ///     “2” - Billing error; for example customer’s payment information was no longer valid.
        /// </summary>
        BillingError = 2,

        /// <summary>
        ///     “3” - Customer did not agree to a recent price increase.
        /// </summary>
        DisagreePrice = 3,

        /// <summary>
        ///     “4” - Product was not available for purchase at the time of renewal.
        /// </summary>
        ProductNotAvailable = 4,

        /// <summary>
        ///     “5” - Unknown error.
        /// </summary>
        Unknown = 5
    }
}