namespace Apple.Receipt.Verificator.Models
{
    public enum AppleReceiptVerificationStatuses
    {
        /// <summary>
        ///     Everything is fine
        /// </summary>
        Ok = 0,

        /// <summary>
        ///     Something went wrong
        /// </summary>
        InternalError = 1,

        /// <summary>
        ///     Wrong arguments provided
        /// </summary>
        WrongArgument = 2,

        /// <summary>
        ///     Apple IAP verification failed
        /// </summary>
        IAPVerificationFailed = 3
    }
}