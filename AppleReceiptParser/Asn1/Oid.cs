using System;
using System.IO;

namespace AppleReceiptParser.Asn1
{
    /// <summary>
    ///     Summary description for OID.
    ///     This class is used to encode and decode OID strings.
    /// </summary>
    public class Oid
    {
        /// <summary>
        ///     Decode OID byte array to OID string.
        /// </summary>
        /// <param name="data">source byte array.</param>
        /// <returns>result OID string.</returns>
        public string Decode(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                ms.Position = 0;
                string retval = Decode(ms);
                return retval;
            }
        }

        /// <summary>
        ///     Decode OID <see cref="Stream" /> and return OID string.
        /// </summary>
        /// <param name="bt">source stream.</param>
        /// <returns>result OID string.</returns>
        public virtual string Decode(Stream bt)
        {
            string retval = "";
            ulong v = 0;
            byte b = (byte) bt.ReadByte();
            retval += Convert.ToString(b / 40);
            retval += "." + Convert.ToString(b % 40);
            while (bt.Position < bt.Length)
            {
                try
                {
                    DecodeValue(bt, ref v);
                    retval += "." + v;
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to decode OID value: " + e.Message);
                }
            }
            return retval;
        }

        /// <summary>
        ///     Decode single OID value.
        /// </summary>
        /// <param name="bt">source stream.</param>
        /// <param name="v">output value</param>
        /// <returns>OID value bytes.</returns>
        protected int DecodeValue(Stream bt, ref ulong v)
        {
            int i = 0;
            v = 0;
            while (true)
            {
                byte b = (byte) bt.ReadByte();
                i++;
                v <<= 7;
                v += (ulong) (b & 0x7f);
                if ((b & 0x80) == 0)
                {
                    return i;
                }
            }
        }
    }
}