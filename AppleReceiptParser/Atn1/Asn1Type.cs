namespace AppleReceiptParser.Atn1
{
    /// <summary>
    ///     Represents codes that encodes ASN1 types.
    /// </summary>
    public class Asn1Type
    {
        public const byte Boolean = 1;
        public const byte Integer = 2;
        public const byte BitString = 3;
        public const byte OctetString = 4;
        public const byte Null = 5;
        public const byte ObjectIdentifier = 6;
        public const byte ObjectDescriptor = 7;
        public const byte External = 8;
        public const byte Real = 9;
        public const byte Enumerated = 10;
        public const byte EmbeddedPdv = 11;
        public const byte Utf8String = 12;
        public const byte RelativeOid = 13;
        public const byte Time = 14;
        public const byte Sequence = 16;
        public const byte Set = 17;
        public const byte NumericString = 18;
        public const byte PrintableString = 19;
        public const byte TeletexString = 20;
        public const byte VideotextString = 21;
        public const byte Ia5String = 22;
        public const byte UtcTime = 23;
        public const byte GeneralizedTime = 24;
        public const byte GraphicString = 25;
        public const byte VisibleString = 26;
        public const byte GeneralString = 27;
        public const byte UniversalString = 28;
        public const byte CharacterString = 29;
        public const byte Bmpstring = 30;
        public const byte Date = 31;
    }
}