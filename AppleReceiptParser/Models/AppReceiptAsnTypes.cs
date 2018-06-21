namespace AppleReceiptParser.Models
{
    internal enum AppReceiptAsnTypes
    {
        BundleIdentifier = 2,
        AppVersion = 3,
        OpaqueValue = 4,
        Hash = 5,
        ReceiptCreationDate = 12,
        InAppPurchaseReceipt = 17,
        OriginalAppVersion = 19,
        ReceiptExpirationDate = 21,

        Quantity = 1701,
        ProductIdentifier = 1702,
        TransactionIdentifier = 1703,
        PurchaseDate = 1704,
        OriginalTransactionIdentifier = 1705,
        OriginalPurchaseDate = 1706,
        SubscriptionExpirationDate = 1708,
        WebOrderLineItemId = 1711,
        CancellationDate = 1712
    }
}
