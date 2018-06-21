using System;
using System.Collections.Generic;
using System.Text;
using AppleReceiptParser.Models;

namespace AppleReceiptParser.Services
{
    public interface IAppleReceiptParserService
    {
        AppleAppReceipt GetAppleReceiptFromBytes(byte[] bytes);
    }
}
