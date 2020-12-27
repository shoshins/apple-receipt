namespace Apple.Receipt.Parser.Models
{
    /// <summary>
    ///     Represents ASN.1 Fields Types numbers for Apple app receipt fields.
    /// </summary>
    internal enum ReceiptAsnType
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
        ///     The date when purchase was originally made.
        /// </summary>
        /// <remarks>
        ///     ASN.1 Field Type 18
        ///     ASN.1 Field Value IA5STRING, interpreted as an RFC 3339 date
        /// </remarks>
        OriginalPurchaseDate = 18,
        
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
        ReceiptExpirationDate = 21
    }
}