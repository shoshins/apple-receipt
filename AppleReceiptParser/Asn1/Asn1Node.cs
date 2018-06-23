using System;
using System.Collections;
using System.IO;
using AppleReceiptParser.Services.NodesParser;

namespace AppleReceiptParser.Asn1
{
    /// <summary>
    ///     Asn1Node, implemented IAsn1Node interface.
    /// </summary>
    public class Asn1Node : IAsn1Node
    {
        /// <summary>
        ///     Default Asn1Node text line length.
        /// </summary>
        public const int DefaultLineLen = 80;

        /// <summary>
        ///     Minium line length.
        /// </summary>
        public const int MinLineLen = 60;

        /// <summary>
        ///     Constant of tag field length.
        /// </summary>
        public const int TagLength = 1;

        /// <summary>
        ///     Constant of unused bits field length.
        /// </summary>
        public const int BitStringUnusedFiledLength = 1;

        private ArrayList _childNodeList;
        private byte[] _data;
        private long _dataLength;
        private long _dataOffset;
        private bool _isIndefiniteLength;
        private long _lengthFieldBytes;
        private bool _parseEncapsulatedData = true;
        private string _path = "";
        private byte _unusedBits;
        private IAsn1ParserUtilitiesService _utilities;

        private Asn1Node(Asn1Node parentNode, long dataOffset)
        {
            Init();
            Deepness = parentNode.Deepness + 1;
            ParentNode = parentNode;
            _dataOffset = dataOffset;
        }

        /// <summary>
        ///     Constructor, initialize all the members.
        /// </summary>
        public Asn1Node()
        {
            Init();
            _dataOffset = 0;
        }

        /// <summary>
        ///     Set/Get requireRecalculatePar. RecalculateTreePar function will not do anything
        ///     if it is set to false.
        /// </summary>
        internal bool RequireRecalculatePar { get; set; } = true;

        /// <summary>
        ///     Get/Set tag value.
        /// </summary>
        public byte Tag { get; set; }

        /// <summary>
        ///     Save node data into Stream.
        /// </summary>
        /// <param name="xdata">Stream.</param>
        /// <returns>true:Succeed; false:failed.</returns>
        public bool SaveData(Stream xdata)
        {
            bool retval = true;
            long nodeCount = ChildNodeCount;
            xdata.WriteByte(Tag);
            _utilities.DerLengthEncode(xdata, (ulong) _dataLength);
            if (Tag == Asn1Type.BitString)
            {
                xdata.WriteByte(_unusedBits);
            }
            if (nodeCount == 0)
            {
                if (_data != null)
                {
                    xdata.Write(_data, 0, _data.Length);
                }
            }
            else
            {
                int i;
                for (i = 0; i < nodeCount; i++)
                {
                    Asn1Node tempNode = GetChildNode(i);
                    retval = tempNode.SaveData(xdata);
                }
            }
            return retval;
        }

        /// <summary>
        ///     Clear data and children list.
        /// </summary>
        public void ClearAll()
        {
            _data = null;
            foreach (object t in _childNodeList)
            {
                Asn1Node tempNode = (Asn1Node) t;
                tempNode.ClearAll();
            }
            _childNodeList.Clear();
            RecalculateTreePar();
        }

        /// <summary>
        ///     Add child node at the end of children list.
        /// </summary>
        /// <param name="xdata">the node that will be add in.</param>
        public void AddChild(Asn1Node xdata)
        {
            _childNodeList.Add(xdata);
            RecalculateTreePar();
        }

        /// <summary>
        ///     Get child node count.
        /// </summary>
        public long ChildNodeCount => _childNodeList.Count;

        /// <summary>
        ///     Retrieve child node by index.
        /// </summary>
        /// <param name="index">0 based index.</param>
        /// <returns>0 based index.</returns>
        public Asn1Node GetChildNode(int index)
        {
            Asn1Node retval = null;
            if (index < ChildNodeCount)
            {
                retval = (Asn1Node) _childNodeList[index];
            }
            return retval;
        }

        /// <summary>
        ///     Get parent node.
        /// </summary>
        public Asn1Node ParentNode { get; private set; }

        /// <summary>
        ///     Get/Set node data by byte[], the data length field content and all the
        ///     node in the parent chain will be adjusted.
        ///     <br></br>
        ///     It return all the child data for constructed node.
        /// </summary>
        public byte[] Data
        {
            get
            {
                using (MemoryStream xdata = new MemoryStream())
                {
                    long nodeCount = ChildNodeCount;
                    if (nodeCount == 0)
                    {
                        if (_data != null)
                        {
                            xdata.Write(_data, 0, _data.Length);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < nodeCount; i++)
                        {
                            Asn1Node tempNode = GetChildNode(i);
                            tempNode.SaveData(xdata);
                        }
                    }
                    byte[] tmpData = new byte[xdata.Length];
                    xdata.Position = 0;
                    xdata.Read(tmpData, 0, (int) xdata.Length);
                    return tmpData;
                }
            }
            set => SetData(value);
        }

        /// <summary>
        ///     Get the deepness of the node.
        /// </summary>
        public long Deepness { get; private set; }

        private void Init()
        {
            _childNodeList = new ArrayList();
            _data = null;
            _dataLength = 0;
            _lengthFieldBytes = 0;
            _unusedBits = 0;
            Tag = Asn1Type.Sequence | Asn1Tag.Constructed;
            _childNodeList.Clear();
            Deepness = 0;
            ParentNode = null;
            _utilities = new Asn1ParserUtilitiesService();
        }

        /// <summary>
        ///     Get node by OID.
        /// </summary>
        /// <param name="oid">OID.</param>
        /// <param name="startNode">Starting node.</param>
        /// <returns>Null or Asn1Node.</returns>
        public static Asn1Node GetDecendantNodeByOid(string oid, Asn1Node startNode)
        {
            Asn1Node retval = null;
            Oid xoid = new Oid();
            for (int i = 0; i < startNode.ChildNodeCount; i++)
            {
                Asn1Node childNode = startNode.GetChildNode(i);
                int tmpTag = childNode.Tag & Asn1Type.Date;
                if (tmpTag == Asn1Type.ObjectIdentifier)
                {
                    if (oid == xoid.Decode(childNode.Data))
                    {
                        retval = childNode;
                        break;
                    }
                }
                retval = GetDecendantNodeByOid(oid, childNode);
                if (retval != null)
                {
                    break;
                }
            }
            return retval;
        }

        /// <summary>
        ///     Find root node and recalculate entire tree length field,
        ///     path, offset and deepness.
        /// </summary>
        internal void RecalculateTreePar()
        {
            if (!RequireRecalculatePar)
            {
                return;
            }
            Asn1Node rootNode;
            for (rootNode = this; rootNode.ParentNode != null;)
            {
                rootNode = rootNode.ParentNode;
            }
            ResetBranchDataLength(rootNode);
            rootNode._dataOffset = 0;
            rootNode.Deepness = 0;
            long subOffset = rootNode._dataOffset + TagLength + rootNode._lengthFieldBytes;
            ResetChildNodePar(rootNode, subOffset);
        }

        /// <summary>
        ///     Recursively set all the node data length.
        /// </summary>
        /// <param name="node"></param>
        /// <returns>node data length.</returns>
        internal long ResetBranchDataLength(Asn1Node node)
        {
            long retval;
            long childDataLength = 0;
            if (node.ChildNodeCount < 1)
            {
                if (node._data != null)
                {
                    childDataLength += node._data.Length;
                }
            }
            else
            {
                for (int i = 0; i < node.ChildNodeCount; i++)
                {
                    childDataLength += ResetBranchDataLength(node.GetChildNode(i));
                }
            }
            node._dataLength = childDataLength;
            if (node.Tag == Asn1Type.BitString)
            {
                node._dataLength += BitStringUnusedFiledLength;
            }
            ResetDataLengthFieldWidth(node);
            retval = node._dataLength + TagLength + node._lengthFieldBytes;
            return retval;
        }

        /// <summary>
        ///     Encode the node data length field and set lengthFieldBytes and dataLength.
        /// </summary>
        /// <param name="node">The node needs to be reset.</param>
        internal void ResetDataLengthFieldWidth(Asn1Node node)
        {
            using (MemoryStream tempStream = new MemoryStream())
            {
                _utilities.DerLengthEncode(tempStream, (ulong) node._dataLength);
                node._lengthFieldBytes = tempStream.Length;
            }
        }

        /// <summary>
        ///     Recursively set all the child parameters, except dataLength.
        ///     dataLength is set by ResetBranchDataLength.
        /// </summary>
        /// <param name="xNode">Starting node.</param>
        /// <param name="subOffset">Starting node offset.</param>
        internal void ResetChildNodePar(Asn1Node xNode, long subOffset)
        {
            int i;
            if (xNode.Tag == Asn1Type.BitString)
            {
                subOffset++;
            }
            for (i = 0; i < xNode.ChildNodeCount; i++)
            {
                Asn1Node tempNode = xNode.GetChildNode(i);
                tempNode.ParentNode = xNode;
                tempNode._dataOffset = subOffset;
                tempNode.Deepness = xNode.Deepness + 1;
                tempNode._path = xNode._path + '/' + i;
                subOffset += TagLength + tempNode._lengthFieldBytes;
                ResetChildNodePar(tempNode, subOffset);
                subOffset += tempNode._dataLength;
            }
        }

        /// <summary>
        ///     Decode ASN.1 encoded node Stream data.
        /// </summary>
        /// <param name="xdata">Stream data.</param>
        /// <returns>true:Succeed, false:Failed.</returns>
        internal bool GeneralDecode(Stream xdata)
        {
            long nodeMaxLen;
            nodeMaxLen = xdata.Length - xdata.Position;
            Tag = (byte) xdata.ReadByte();
            long start, end;
            start = xdata.Position;
            _dataLength = _utilities.DerLengthDecode(xdata, ref _isIndefiniteLength);
            if (_dataLength < 0)
            {
                return false; // Node data length can not be negative.
            }
            end = xdata.Position;
            _lengthFieldBytes = end - start;
            if (nodeMaxLen < _dataLength + TagLength + _lengthFieldBytes)
            {
                return false;
            }
            if (ParentNode == null || (ParentNode.Tag & Asn1Tag.Constructed) == 0)
            {
                if ((Tag & Asn1Type.Date) <= 0 || (Tag & Asn1Type.Date) > 0x1E)
                {
                    return false;
                }
            }
            if (Tag == Asn1Type.BitString)
            {
                // First byte of BIT_STRING is unused bits.
                // BIT_STRING data does not include this byte.

                // Fixed by Gustaf Björklund.
                if (_dataLength < 1)
                {
                    return false; // We cannot read less than 1 - 1 bytes.
                }

                _unusedBits = (byte) xdata.ReadByte();
                _data = new byte[_dataLength - 1];
                xdata.Read(_data, 0, (int) (_dataLength - 1));
            }
            else
            {
                _data = new byte[_dataLength];
                xdata.Read(_data, 0, (int) _dataLength);
            }
            return true;
        }

        /// <summary>
        ///     Decode ASN.1 encoded complex data type Stream data.
        /// </summary>
        /// <param name="xdata">Stream data.</param>
        /// <returns>true:Succeed, false:Failed.</returns>
        internal bool ListDecode(Stream xdata)
        {
            bool retval = false;
            long originalPosition = xdata.Position;
            try
            {
                long childNodeMaxLen = xdata.Length - xdata.Position;
                Tag = (byte) xdata.ReadByte();
                long start, end, offset;
                start = xdata.Position;
                _dataLength = _utilities.DerLengthDecode(xdata, ref _isIndefiniteLength);
                if (_dataLength < 0 || childNodeMaxLen < _dataLength)
                {
                    return false;
                }
                end = xdata.Position;
                _lengthFieldBytes = end - start;
                offset = _dataOffset + TagLength + _lengthFieldBytes;
                if (Tag == Asn1Type.BitString)
                {
                    // First byte of BIT_STRING is unused bits.
                    // BIT_STRING data does not include this byte.
                    _unusedBits = (byte) xdata.ReadByte();
                    _dataLength--;
                    offset++;
                }
                if (_dataLength <= 0)
                {
                    return false; // List data length can't be zero.
                }
                using (Stream secData = new MemoryStream((int) _dataLength))
                {
                    byte[] secByte = new byte[_dataLength];
                    xdata.Read(secByte, 0, (int) _dataLength);
                    if (Tag == Asn1Type.BitString)
                    {
                        _dataLength++;
                    }
                    secData.Write(secByte, 0, secByte.Length);
                    secData.Position = 0;
                    while (secData.Position < secData.Length)
                    {
                        Asn1Node node = new Asn1Node(this, offset) {_parseEncapsulatedData = _parseEncapsulatedData};
                        start = secData.Position;
                        if (!node.InternalLoadData(secData))
                        {
                            return false;
                        }
                        AddChild(node);
                        end = secData.Position;
                        offset += end - start;
                    }
                }
                retval = true;
            }
            finally
            {
                if (!retval)
                {
                    xdata.Position = originalPosition;
                    ClearAll();
                }
            }
            return true;
        }

        /// <summary>
        ///     Set the node data and recalculate the entire tree parameters.
        /// </summary>
        /// <param name="xdata">byte[] data.</param>
        internal void SetData(byte[] xdata)
        {
            if (_childNodeList.Count > 0)
            {
                throw new Exception("Constructed node can't hold simple data.");
            }
            _data = xdata;
            _dataLength = _data?.Length ?? 0;
            RecalculateTreePar();
        }

        /// <summary>
        ///     Load data from Stream. Start from current position.
        /// </summary>
        /// <param name="xdata">Stream</param>
        /// <returns>true:Succeed; false:failed.</returns>
        internal bool InternalLoadData(Stream xdata)
        {
            bool retval = true;
            ClearAll();
            byte xtag;
            long curPosition = xdata.Position;
            xtag = (byte) xdata.ReadByte();
            xdata.Position = curPosition;
            int maskedTag = xtag & Asn1Type.Date;
            if ((xtag & Asn1Tag.Constructed) != 0
                || _parseEncapsulatedData
                && (maskedTag == Asn1Type.BitString
                    || maskedTag == Asn1Type.External
                    || maskedTag == Asn1Type.GeneralString
                    || maskedTag == Asn1Type.GeneralizedTime
                    || maskedTag == Asn1Type.GraphicString
                    || maskedTag == Asn1Type.Ia5String
                    || maskedTag == Asn1Type.OctetString
                    || maskedTag == Asn1Type.PrintableString
                    || maskedTag == Asn1Type.Sequence
                    || maskedTag == Asn1Type.Set
                    || maskedTag == Asn1Type.TeletexString
                    || maskedTag == Asn1Type.UniversalString
                    || maskedTag == Asn1Type.Utf8String
                    || maskedTag == Asn1Type.VideotextString
                    || maskedTag == Asn1Type.VisibleString)
            )
            {
                if (ListDecode(xdata))
                {
                    return true;
                }
                if (!GeneralDecode(xdata))
                {
                    retval = false;
                }
            }
            else
            {
                if (!GeneralDecode(xdata))
                {
                    retval = false;
                }
            }
            return retval;
        }
    }
}