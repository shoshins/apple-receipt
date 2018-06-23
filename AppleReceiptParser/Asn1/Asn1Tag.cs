namespace AppleReceiptParser.Asn1
{
    /// <summary>
    ///     Define ASN.1 tag class constants.
    /// </summary>
    public class Asn1Tag
    {
        public const byte Universal = 0;
        public const byte Constructed = 32;
        public const byte Application = 64;
        public const byte ContextSpecific = 128;
        public const byte Private = 192;
        public const byte ClassMask = 192;
    }
}