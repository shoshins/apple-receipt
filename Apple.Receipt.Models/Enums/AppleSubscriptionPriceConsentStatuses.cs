namespace Apple.Receipt.Models.Enums
{
    /// <summary>
    ///     The current price consent status for a subscription price increase.
    /// </summary>
    /// <remarks>
    ///     This key is only present for auto-renewable subscription receipts if the subscription price was increased without
    ///     keeping the existing price for active subscribers.
    /// </remarks>
    /// <remarks>
    ///     You can use this value to track customer adoption of the new price and take appropriate action.
    /// </remarks>
    public enum AppleSubscriptionPriceConsentStatuses
    {
        /// <summary>
        ///     “0” - Customer has not taken action regarding the increased price. Subscription expires if the customer takes no
        ///     action before the renewal date.
        /// </summary>
        NoActionFromCustomer = 0,

        /// <summary>
        ///     “1” - Customer has agreed to the price increase. Subscription will renew at the higher price.
        /// </summary>
        PriceIncreaseAgreed = 1
    }
}