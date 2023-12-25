namespace Apple.Receipt.Models.Enums
{
    /// <summary>
    ///     The relationship of the user with the family-shared purchase to which they have access.
    /// </summary>
    /// <remarks>
    ///     When family members benefit from a shared subscription, App Store updates their receipt to include the family-shared purchase. 
    ///     Use the value of in_app_ownership_type to understand whether a transaction belongs to the purchaser or a family member who benefits.
    ///     This field appears in the App Store server notifications unified receipt (unified_receipt.Latest_receipt_info) and 
    ///     in transaction receipts (responseBody.Latest_receipt_info).
    /// </remarks>
    public enum InAppOwnershipType
    {
        /// <summary>
        ///     “FAMILY_SHARED” - The transaction belongs to a family member who benefits from service.
        /// </summary>
        FamilyShared,
        /// <summary>
        ///     “PURCHASED” - The transaction belongs to the purchaser.
        /// </summary>
        Purchased
    }
}