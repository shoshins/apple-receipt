using System.IO;
using System.Linq;

namespace AppleReceiptParser.Services.NodesParser
{
    public class Asn1ParserUtilitiesService : IAsn1ParserUtilitiesService
    {
        /// <summary>
        ///     Convert byte array to a <see cref="long" /> integer.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public long BytesToLong(byte[] bytes)
        {
            return bytes.Aggregate<byte, long>(0, (current, t) => (current << 8) | t);
        }

        /// <summary>
        ///     Calculate how many bytes is enough to hold the value.
        /// </summary>
        /// <param name="value">input value.</param>
        /// <returns>bytes number.</returns>
        public int BytePrecision(ulong value)
        {
            int i;
            for (i = 4; i > 0; --i) // 4: sizeof(ulong)
            {
                if (value >> ((i - 1) * 8) != 0)
                {
                    break;
                }
            }
            return i;
        }

        /// <summary>
        ///     ASN.1 DER length encoder.
        /// </summary>
        /// <param name="xdata">result output stream.</param>
        /// <param name="length">source length.</param>
        /// <returns>result bytes.</returns>
        public int DerLengthEncode(Stream xdata, ulong length)
        {
            int i = 0;
            if (length <= 0x7f)
            {
                xdata.WriteByte((byte) length);
                i++;
            }
            else
            {
                xdata.WriteByte((byte) (BytePrecision(length) | 0x80));
                i++;
                for (int j = BytePrecision(length); j > 0; --j)
                {
                    xdata.WriteByte((byte) (length >> ((j - 1) * 8)));
                    i++;
                }
            }
            return i;
        }

        /// <summary>
        ///     ASN.1 DER length decoder.
        /// </summary>
        /// <param name="bt">Source stream.</param>
        /// <param name="isIndefiniteLength">Output parameter.</param>
        /// <returns>Output length.</returns>
        public long DerLengthDecode(Stream bt, ref bool isIndefiniteLength)
        {
            isIndefiniteLength = false;
            long length;
            byte b;
            b = (byte) bt.ReadByte();
            if ((b & 0x80) == 0)
            {
                length = b;
            }
            else
            {
                long lengthBytes = b & 0x7f;
                if (lengthBytes == 0)
                {
                    isIndefiniteLength = true;
                    return -2; // Indefinite length.
                }
                length = 0;
                while (lengthBytes-- > 0)
                {
                    if (length >> (8 * (4 - 1)) > 0) // 4: sizeof(long)
                    {
                        return -1; // Length overflow.
                    }
                    b = (byte) bt.ReadByte();
                    length = (length << 8) | b;
                }
            }
            return length;
        }
    }
}