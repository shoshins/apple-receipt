using System;
using System.Collections.Generic;
using System.Text;

namespace AppleReceiptParser.Atn1
{
    /// <summary>
    /// Define ASN.1 tag class constants.
    /// </summary>
    /// 
    public class Asn1TagClasses
    {
        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte ClassMask = 0xc0;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte Universal = 0x00;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte Constructed = 0x20;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte Application = 0x40;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte ContextSpecific = 0x80;

        /// <summary>
        /// Constant value.
        /// </summary>
        public const byte Private = 0xc0;
    };
}
