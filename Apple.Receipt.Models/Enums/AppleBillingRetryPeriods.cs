namespace Apple.Receipt.Models.Enums
{
    /// <summary>
    ///     For an expired subscription, whether or not Apple is still attempting to automatically renew the subscription.
    /// </summary>
    /// <remarks>
    ///     This key is only present for auto-renewable subscription receipts. If the customer’s subscription failed to renew
    ///     because the App Store was unable to complete the transaction, this value will reflect whether or not the App Store
    ///     is still trying to renew the subscription.
    /// </remarks>
    public enum AppleBillingRetryPeriods
    {
        /// <summary>
        ///     “0” - App Store has stopped attempting to renew the subscription.
        /// </summary>
        Stopped = 0,

        /// <summary>
        ///     “1” - App Store is still attempting to renew the subscription.
        /// </summary>
        Attempting = 1
    }
}