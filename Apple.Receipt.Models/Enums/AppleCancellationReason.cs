namespace Apple.Receipt.Models.Enums
{
    /// <summary>
    ///     For a transaction that was canceled, the reason for cancellation.
    /// </summary>
    /// <remarks>
    ///     Use this value along with the cancellation date to identify possible issues in your app that may lead customers to
    ///     contact Apple customer support.
    /// </remarks>
    public enum AppleCancellationReason
    {
        /// <summary>
        ///     “0” - Transaction was canceled for another reason, for example, if the customer made the purchase accidentally.
        /// </summary>
        UnknownReason = 0,

        /// <summary>
        ///     “1” - Customer canceled their transaction due to an actual or perceived issue within your app.
        /// </summary>
        CancelledByUser = 1
    }
}