using System;
using System.IO;
using System.Text;
using AppleReceiptParser.Atn1;

namespace AppleReceiptParser.Services.NodesParser
{
    public class Asn1NodesParser: IAsn1NodesParser
    {
        private readonly IAsn1ParserUtilitiesService _utilities;
        public Asn1NodesParser(IAsn1ParserUtilitiesService utilities)
        {
            _utilities = utilities;
        }

        public string GetStringFromNode(Asn1Node nn)
        {
            string dataStr = null;

            if ((nn.Tag & Asn1Tag.TagMask) == Asn1Tag.OctetString && nn.ChildNodeCount > 0)
            {
                Asn1Node n = nn.GetChildNode(0);

                switch (n.Tag & Asn1Tag.TagMask)
                {
                    case Asn1Tag.PrintableString:
                    case Asn1Tag.Ia5String:
                    case Asn1Tag.UniversalString:
                    case Asn1Tag.VisibleString:
                    case Asn1Tag.NumericString:
                    case Asn1Tag.UtcTime:
                    case Asn1Tag.Utf8String:
                    case Asn1Tag.Bmpstring:
                    case Asn1Tag.GeneralString:
                    case Asn1Tag.GeneralizedTime:
                        {
                            if ((n.Tag & Asn1Tag.TagMask) == Asn1Tag.Utf8String)
                            {
                                UTF8Encoding unicode = new UTF8Encoding();
                                dataStr = unicode.GetString(n.Data);
                            }
                            else
                            {
                                dataStr = Encoding.ASCII.GetString(n.Data);
                            }
                        }
                        break;
                }
            }
            return dataStr;
        }

        public DateTime GetDateTimeFromNode(Asn1Node nn)
        {
            string dataStr = GetStringFromNode(nn);
            if (string.IsNullOrEmpty(dataStr))
            {
                return DateTime.MinValue;
            }
            DateTime retval = DateTime.MaxValue;
            DateTime.TryParse(dataStr, out retval);
            return retval;
        }

        public string GetDateTimeMsFromNode(Asn1Node nn)
        {
            var date = GetDateTimeFromNode(nn);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalMilliseconds).ToString();
        }

        public int GetIntegerFromNode(Asn1Node nn)
        {
            int retval = -1;

            if ((nn.Tag & Asn1Tag.TagMask) == Asn1Tag.OctetString && nn.ChildNodeCount > 0)
            {
                Asn1Node n = nn.GetChildNode(0);
                if ((n.Tag & Asn1Tag.TagMask) == Asn1Tag.Integer)
                    retval = (int)_utilities.BytesToLong(n.Data);
            }
            return retval;
        }

        public Asn1Node GetNodeFromBytes(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                Asn1Node resultNode = new Asn1Node();
                try
                {
                    resultNode.RequireRecalculatePar = false;
                    resultNode.InternalLoadData(stream);
                    return resultNode;
                }
                finally
                {
                    resultNode.RequireRecalculatePar = true;
                    resultNode.RecalculateTreePar();
                }
            }
        }
    }
}
