namespace AppleReceiptParser.Atn1
{
    /// <summary>
    /// Define ASN.1 tag constants.
    /// </summary>
    /// 
    public class Asn1Tag
    {
        /// <summary>
        /// Tag mask constant value.
        /// </summary>
        public const byte TagMask              = 0x1F;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte Boolean 			    = 0x01;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte Integer 			    = 0x02;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte BitString			= 0x03;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte OctetString		    = 0x04;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte TagNull			    = 0x05;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte ObjectIdentifier	    = 0x06;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte ObjectDescriptor	    = 0x07;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte External			    = 0x08;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte Real				    = 0x09;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte Enumerated			= 0x0a;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte Utf8String			= 0x0c;

        /// <summary>
        /// Relative object identifier.
        /// </summary>
        public const byte RelativeOid          = 0x0d;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte Sequence			    = 0x10;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte Set 				    = 0x11;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte NumericString		= 0x12;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte PrintableString 	    = 0x13;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte T61String			= 0x14;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte VideotextString 	    = 0x15;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte Ia5String			= 0x16;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte UtcTime 			    = 0x17;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte GeneralizedTime 	    = 0x18;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte GraphicString		= 0x19;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte VisibleString		= 0x1a;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte GeneralString		= 0x1b;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte UniversalString	    = 0x1C;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte Bmpstring		        = 0x1E;
    };
}
