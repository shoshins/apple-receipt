using System;
using Apple.Receipt.Parser.Asn1;

namespace Apple.Receipt.Parser.Services.NodesParser
{
    internal interface IAsn1NodesParser
    {
        string GetStringFromNode(Asn1Node nn);

        bool GetBoolFromNode(Asn1Node nn);

        DateTime GetDateTimeFromNode(Asn1Node nn);

        string GetDateTimeMsFromNode(Asn1Node nn);

        int GetIntegerFromNode(Asn1Node nn);

        Asn1Node GetNodeFromBytes(byte[] bytes);
    }
}