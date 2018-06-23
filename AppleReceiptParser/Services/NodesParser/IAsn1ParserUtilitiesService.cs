using System.IO;

namespace AppleReceiptParser.Services.NodesParser
{
    public interface IAsn1ParserUtilitiesService
    {
        long BytesToLong(byte[] bytes);
        int BytePrecision(ulong value);
        int DerLengthEncode(Stream xdata, ulong length);
        long DerLengthDecode(Stream bt, ref bool isIndefiniteLength);
    }
}