namespace Apple.Receipt.Parser.Models
{
    /// <summary>
    ///     Represents ASN.1 Fields Types numbers for Apple app receipt fields.
    /// </summary>
    internal enum AppReceiptAsnType
    {
        /// <summary>
        ///     The app’s bundle identifier.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 2
        ///     ASN.1 Field Value UTF8STRING
        /// </remarks>
        BundleIdentifier = 2,

        /// <summary>
        ///     The app’s version number.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 3
        ///     ASN.1 Field Value UTF8STRING
        /// </remarks>
        AppVersion = 3,

        /// <summary>
        ///     An opaque value used, with other data, to compute the SHA-1 hash during validation.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 4
        ///     ASN.1 Field Value A series of bytes
        /// </remarks>
        OpaqueValue = 4,

        /// <summary>
        ///     A SHA-1 hash, used to validate the receipt.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 5
        ///     ASN.1 Field Value 20-byte SHA-1 digest
        /// </remarks>
        Hash = 5,

        /// <summary>
        ///     The date when the app receipt was created.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 12
        ///     ASN.1 Field Value IA5STRING, interpreted as an RFC 3339 date
        /// </remarks>
        ReceiptCreationDate = 12,

        /// <summary>
        ///     The receipt for an in-app purchase.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 17
        ///     ASN.1 Field Value SET of in-app purchase receipt attributes
        /// </remarks>
        InAppPurchaseReceipt = 17,

        /// <summary>
        ///     The version of the app that was originally purchased.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 19
        ///     ASN.1 Field Value UTF8STRING
        /// </remarks>
        OriginalAppVersion = 19,

        /// <summary>
        ///     The date that the app receipt expires.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 21
        ///     ASN.1 Field Value IA5STRING, interpreted as an RFC 3339 date
        /// </remarks>
        ReceiptExpirationDate = 21,

        /// <summary>
        ///     The number of items purchased.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 1701
        ///     ASN.1 Field Value INTEGER
        /// </remarks>
        Quantity = 1701,

        /// <summary>
        ///     The product identifier of the item that was purchased.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 1702
        ///     ASN.1 Field Value UTF8STRING
        /// </remarks>
        ProductIdentifier = 1702,

        /// <summary>
        ///     The transaction identifier of the item that was purchased.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 1703
        ///     ASN.1 Field Value UTF8STRING
        /// </remarks>
        TransactionIdentifier = 1703,

        /// <summary>
        ///     The date and time that the item was purchased.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 1704
        ///     ASN.1 Field Value IA5STRING, interpreted as an RFC 3339 date
        /// </remarks>
        PurchaseDate = 1704,

        /// <summary>
        ///     For a transaction that restores a previous transaction, the transaction identifier of the original transaction.
        ///     Otherwise, identical to the transaction identifier.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 1705
        ///     ASN.1 Field Value UTF8STRING
        /// </remarks>
        OriginalTransactionIdentifier = 1705,

        /// <summary>
        ///     For a transaction that restores a previous transaction, the date of the original transaction.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 1706
        ///     ASN.1 Field Value IA5STRING, interpreted as an RFC 3339 date
        /// </remarks>
        OriginalPurchaseDate = 1706,

        /// <summary>
        ///     The expiration date for the subscription, expressed as the number of milliseconds since January 1, 1970, 00:00:00
        ///     GMT.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 1708
        ///     ASN.1 Field Value IA5STRING, interpreted as an RFC 3339 date
        /// </remarks>
        SubscriptionExpirationDate = 1708,

        /// <summary>
        ///     The primary key for identifying subscription purchases.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 1711
        ///     ASN.1 Field Value INTEGER
        /// </remarks>
        WebOrderLineItemId = 1711,

        /// <summary>
        ///     For a transaction that was canceled by Apple customer support, the time and date of the cancellation.
        ///     For an auto-renewable subscription plan that was upgraded, the time and date of the upgrade transaction.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 1712
        ///     ASN.1 Field Value IA5STRING, interpreted as an RFC 3339 date
        /// </remarks>
        CancellationDate = 1712,

        /// <summary>
        ///     For an auto-renewable subscription, whether or not it is in the introductory price period.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 1719
        ///     ASN.1 Field Value INTEGER
        /// </remarks>
        SubscriptionIntroductoryPricePeriod = 1719
    }
}