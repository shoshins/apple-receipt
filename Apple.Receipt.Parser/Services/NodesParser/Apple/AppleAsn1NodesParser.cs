using System.Linq;
using Apple.Receipt.Models;
using Apple.Receipt.Parser.Asn1;
using Apple.Receipt.Parser.Models;

namespace Apple.Receipt.Parser.Services.NodesParser.Apple
{
    internal class AppleAsn1NodesParser : IAppleAsn1NodesParser
    {
        private readonly IAsn1NodesParser _nodesParser;
        private readonly IAsn1ParserUtilitiesService _utilities;

        public AppleAsn1NodesParser(IAsn1NodesParser nodesParser, IAsn1ParserUtilitiesService utilities)
        {
            _nodesParser = nodesParser;
            _utilities = utilities;
        }

        public AppleAppReceipt GetAppleReceiptFromNode(Asn1Node tNode, AppleAppReceipt receipt = null)
        {
            if (receipt == null)
            {
                receipt = new AppleAppReceipt();
            }
            bool processedNode = false;
            if ((tNode.Tag & Asn1Type.Date) == Asn1Type.Sequence && tNode.ChildNodeCount == 3)
            {
                Asn1Node node1 = tNode.GetChildNode(0);
                Asn1Node node2 = tNode.GetChildNode(1);
                Asn1Node node3 = tNode.GetChildNode(2);

                if ((node1.Tag & Asn1Type.Date) == Asn1Type.Integer &&
                    (node2.Tag & Asn1Type.Date) == Asn1Type.Integer &&
                    (node3.Tag & Asn1Type.Date) == Asn1Type.OctetString)
                {
                    processedNode = true;
                    AppReceiptAsnType type = (AppReceiptAsnType) _utilities.BytesToLong(node1.Data);
                    switch (type)
                    {
                        case AppReceiptAsnType.BundleIdentifier:
                            receipt.BundleId = _nodesParser.GetStringFromNode(node3);
                            break;
                        case AppReceiptAsnType.AppVersion:
                            receipt.ApplicationVersion = _nodesParser.GetStringFromNode(node3);
                            break;
                        case AppReceiptAsnType.OpaqueValue:
                            break;
                        case AppReceiptAsnType.Hash:
                            break;
                        case AppReceiptAsnType.OriginalAppVersion:
                            receipt.OriginalApplicationVersion = _nodesParser.GetStringFromNode(node3);
                            break;
                        case AppReceiptAsnType.ReceiptExpirationDate:
                            break;
                        case AppReceiptAsnType.ReceiptCreationDate:
                            receipt.ReceiptCreationDateMs = _nodesParser.GetDateTimeMsFromNode(node3);
                            break;
                        case AppReceiptAsnType.InAppPurchaseReceipt:
                        {
                            if (node3.ChildNodeCount > 0)
                            {
                                Asn1Node node31 = node3.GetChildNode(0);
                                if ((node31.Tag & Asn1Type.Date) == Asn1Type.Set && node31.ChildNodeCount > 0)
                                {
                                    AppleInAppPurchaseReceipt purchaseReceipt = new AppleInAppPurchaseReceipt();

                                    for (int i = 0; i < node31.ChildNodeCount; i++)
                                    {
                                        Asn1Node node311 = node31.GetChildNode(i);
                                        if ((node311.Tag & Asn1Type.Date) == Asn1Type.Sequence &&
                                            node311.ChildNodeCount == 3)
                                        {
                                            Asn1Node node3111 = node311.GetChildNode(0);
                                            Asn1Node node3112 = node311.GetChildNode(1);
                                            Asn1Node node3113 = node311.GetChildNode(2);
                                            if ((node3111.Tag & Asn1Type.Date) == Asn1Type.Integer &&
                                                (node3112.Tag & Asn1Type.Date) == Asn1Type.Integer &&
                                                (node3113.Tag & Asn1Type.Date) == Asn1Type.OctetString)
                                            {
                                                AppReceiptAsnType type1 =
                                                    (AppReceiptAsnType) _utilities.BytesToLong(node3111.Data);
                                                switch (type1)
                                                {
                                                    case AppReceiptAsnType.Quantity:
                                                        purchaseReceipt.Quantity = _nodesParser
                                                            .GetIntegerFromNode(node3113).ToString();
                                                        break;
                                                    case AppReceiptAsnType.ProductIdentifier:
                                                        purchaseReceipt.ProductId =
                                                            _nodesParser.GetStringFromNode(node3113);
                                                        break;
                                                    case AppReceiptAsnType.TransactionIdentifier:
                                                        purchaseReceipt.TransactionId =
                                                            _nodesParser.GetStringFromNode(node3113);
                                                        break;
                                                    case AppReceiptAsnType.PurchaseDate:
                                                        purchaseReceipt.PurchaseDateMs =
                                                            _nodesParser.GetDateTimeMsFromNode(node3113);
                                                        break;
                                                    case AppReceiptAsnType.OriginalTransactionIdentifier:
                                                        purchaseReceipt.OriginalTransactionId =
                                                            _nodesParser.GetStringFromNode(node3113);
                                                        break;
                                                    case AppReceiptAsnType.OriginalPurchaseDate:
                                                        purchaseReceipt.OriginalPurchaseDateMs =
                                                            _nodesParser.GetDateTimeMsFromNode(node3113);
                                                        break;
                                                    case AppReceiptAsnType.SubscriptionExpirationDate:
                                                        purchaseReceipt.ExpirationDateMs =
                                                            _nodesParser.GetDateTimeMsFromNode(node3113);
                                                        break;
                                                    case AppReceiptAsnType.WebOrderLineItemId:
                                                        purchaseReceipt.WebOrderLineItemId =
                                                            _nodesParser.GetStringFromNode(node3113);
                                                        break;
                                                    case AppReceiptAsnType.CancellationDate:
                                                        purchaseReceipt.CancellationDate =
                                                            _nodesParser.GetDateTimeFromNode(node3113);
                                                        break;
                                                    case AppReceiptAsnType.SubscriptionIntroductoryPricePeriod:
                                                        purchaseReceipt.IsInIntroOfferPeriod =
                                                            _nodesParser.GetBoolFromNode(node3113);
                                                        break;
                                                }
                                            }
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(purchaseReceipt.ProductId))
                                    {
                                        receipt.PurchaseReceipts.Add(purchaseReceipt);
                                    }
                                }
                            }
                        }
                            break;
                        default:
                            processedNode = false;
                            break;
                    }
                }
            }

            if (!processedNode)
            {
                for (int i = 0; i < tNode.ChildNodeCount; i++)
                {
                    Asn1Node chld = tNode.GetChildNode(i);
                    if (chld != null)
                    {
                        AppleAppReceipt subReceipt = GetAppleReceiptFromNode(chld, receipt);
                        if (subReceipt.PurchaseReceipts != null)
                        {
                            foreach (AppleInAppPurchaseReceipt sr in subReceipt.PurchaseReceipts)
                            {
                                if (receipt.PurchaseReceipts.All(purchaseReceipt =>
                                    purchaseReceipt.ProductId != sr.ProductId))
                                {
                                    receipt.PurchaseReceipts.Add(sr);
                                }
                            }
                        }
                    }
                }
            }
            return receipt;
        }
    }
}