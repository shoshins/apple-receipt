using System.IO;

namespace Apple.Receipt.Parser.Services.NodesParser
{
    internal interface IAsn1ParserUtilitiesService
    {
        long BytesToLong(byte[] bytes);
        int BytePrecision(ulong value);
        int DerLengthEncode(Stream xdata, ulong length);
        long DerLengthDecode(Stream bt, ref bool isIndefiniteLength);
    }
}