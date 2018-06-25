namespace Apple.Receipt.Models.Enums
{
    /// <summary>
    ///     The current renewal status for the auto-renewable subscription.
    /// </summary>
    /// <remarks>
    ///     This key is only present for auto-renewable subscription receipts, for active or expired subscriptions.
    /// </remarks>
    /// <remarks>
    ///     The value for this key should not be interpreted as the customer’s subscription status. You can use this value to
    ///     display an alternative subscription product in your app, for example, a lower level subscription plan that the
    ///     customer can downgrade to from their current plan.
    /// </remarks>
    public enum AppleSubscriptionAutoRenewStatus
    {
        /// <summary>
        ///     “0” - Customer has turned off automatic renewal for their subscription.
        /// </summary>
        Disabled = 0,

        /// <summary>
        ///     “1” - Subscription will renew at the end of the current subscription period.
        /// </summary>
        Active = 1
    }
}