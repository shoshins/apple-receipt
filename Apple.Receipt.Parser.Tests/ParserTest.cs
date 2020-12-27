using Apple.Receipt.Parser.Modules;
using Apple.Receipt.Parser.Services;
using Apple.Receipt.Parser.Services.NodesParser;
using Apple.Receipt.Parser.Services.NodesParser.Apple;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Apple.Receipt.Models;
using KellermanSoftware.CompareNetObjects;
using Xunit;

namespace Apple.Receipt.Parser.Tests
{
    public class ParserTest : IDisposable
    {
        private readonly IServiceProvider _container;

        public ParserTest()
        {
            var services = new ServiceCollection();
            services.RegisterAppleReceiptParser();
            _container = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            if (_container is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        [Theory]
        [ClassData(typeof(TypesExpectedToBeRegisteredTestCaseSource))]
        public void CheckAutofacRegistration(Type type)
        {
            Assert.NotNull(_container.GetService(type));
        }

        private class TypesExpectedToBeRegisteredTestCaseSource : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                return Types()
                    .Select(type => new object[] {type})
                    .GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private IEnumerable<Type> Types()
            {
                yield return typeof(IAppleAsn1NodesParser);
                yield return typeof(IAppleReceiptParserService);
                yield return typeof(IAsn1NodesParser);
                yield return typeof(IAsn1ParserUtilitiesService);
            }
        }
        
        [Fact]
        public void ParsingTestOne()
        {
            #region Receipt JSON

            // {
            //   "original_purchase_date_pst": "2015-11-17 08:34:41 America/Los_Angeles",
            //   "purchase_date_ms": "1447821281000",
            //   "unique_identifier": "dd0e33ebf56bb1f3ad4c2dbaa7a0b33ac68b0e85",
            //   "original_transaction_id": "1000000180636226",
            //   "expires_date": "1448346881000",
            //   "transaction_id": "1000000181298734",
            //   "original_purchase_date_ms": "1447778081000",
            //   "web_order_line_item_id": "1000000030938713",
            //   "bvrs": "1",
            //   "unique_vendor_identifier": "4E5E1A3B-F01A-4855-820F-F349F1242148",
            //   "expires_date_formatted_pst": "2015-11-23 22:34:41 America/Los_Angeles",
            //   "item_id": "1028950797",
            //   "expires_date_formatted": "2015-11-24 06:34:41 Etc/GMT",
            //   "product_id": "yearly",
            // {
            //   :application_version=>"1",
            //   :original_application_version=>"1.0",
            //   :environment=>"ProductionSandbox",
            //   :bundle_id=>"com.mbaasy.ios.demo",
            //   :creation_date=>"2015-08-13T07:50:46Z",
            //   :in_app=> [{
            //     :expires_date=>"",
            //     :cancellation_date=>"",
            //     :quantity=>1,
            //     :web_order_line_item_id=>0,
            //     :product_id=>"consumable",
            //     :transaction_id=>"1000000166865231",
            //     :original_transaction_id=>"1000000166865231",
            //     :purchase_date=>"2015-08-07T20:37:55Z",
            //     :original_purchase_date=>"2015-08-07T20:37:55Z"
            //   }, {
            //     :cancellation_date=>"",
            //     :quantity=>1,
            //     :product_id=>"monthly",
            //     :web_order_line_item_id=>1000000030274153,
            //     :transaction_id=>"1000000166965150",
            //     :original_transaction_id=>"1000000166965150",
            //     :purchase_date=>"2015-08-10T06:49:32Z",
            //     :original_purchase_date=>"2015-08-10T06:49:33Z",
            //     :expires_date=>"2015-08-10T06:54:32Z"
            //   }, {
            //     :cancellation_date=>"",
            //     :quantity=>1,
            //     :product_id=>"monthly",
            //     :web_order_line_item_id=>1000000030274154,
            //     :transaction_id=>"1000000166965327",
            //     :original_transaction_id=>"1000000166965150",
            //     :purchase_date=>"2015-08-10T06:54:32Z",
            //     :original_purchase_date=>"2015-08-10T06:53:18Z",
            //     :expires_date=>"2015-08-10T06:59:32Z"
            //   }, {
            //     :cancellation_date=>"",
            //     :quantity=>1,
            //     :product_id=>"monthly",
            //     :web_order_line_item_id=>1000000030274165,
            //     :transaction_id=>"1000000166965895",
            //     :original_transaction_id=>"1000000166965150",
            //     :purchase_date=>"2015-08-10T06:59:32Z",
            //     :original_purchase_date=>"2015-08-10T06:57:34Z",
            //     :expires_date=>"2015-08-10T07:04:32Z"
            //   }, {
            //     :cancellation_date=>"",
            //     :quantity=>1,
            //     :product_id=>"monthly",
            //     :web_order_line_item_id=>1000000030274192,
            //     :transaction_id=>"1000000166967152",
            //     :original_transaction_id=>"1000000166965150",
            //     :purchase_date=>"2015-08-10T07:04:32Z",
            //     :original_purchase_date=>"2015-08-10T07:02:33Z",
            //     :expires_date=>"2015-08-10T07:09:32Z"
            //   }, {
            //     :cancellation_date=>"",
            //     :quantity=>1,
            //     :product_id=>"monthly",
            //     :web_order_line_item_id=>1000000030274219,
            //     :transaction_id=>"1000000166967484",
            //     :original_transaction_id=>"1000000166965150",
            //     :purchase_date=>"2015-08-10T07:09:32Z",
            //     :original_purchase_date=>"2015-08-10T07:08:30Z",
            //     :expires_date=>"2015-08-10T07:14:32Z"
            //   }, {
            //     :cancellation_date=>"",
            //     :quantity=>1,
            //     :product_id=>"monthly",
            //     :web_order_line_item_id=>1000000030274249,
            //     :transaction_id=>"1000000166967782",
            //     :original_transaction_id=>"1000000166965150",
            //     :purchase_date=>"2015-08-10T07:14:32Z",
            //     :original_purchase_date=>"2015-08-10T07:12:34Z",
            //     :expires_date=>"2015-08-10T07:19:32Z"
            //   }]
            // }

            #endregion
            #region Receipt Encoded

            const string appleAppReceipt =
                "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";

            #endregion
            #region Receipt Object
            AppleAppReceipt ideal = new AppleAppReceipt
            {
                ApplicationVersion = "1",
                OriginalApplicationVersion = "1.0",
                ReceiptCreationDateMs = "1447866828000",
                BundleId = "com.mbaasy.ios.demo",
                OriginalPurchaseDateMs = "1375340400000",
                PurchaseReceipts = new List<AppleInAppPurchaseReceipt>
                {
                    new AppleInAppPurchaseReceipt()
                    {
                        OriginalTransactionId = "1000000180842733",
                        TransactionId = "1000000180842733",
                        Quantity = "1",
                        OriginalPurchaseDateMs = "1447866826000",
                        PurchaseDateMs = "1447866826000",
                        ProductId = "free_subscription",
                    },
                    new AppleInAppPurchaseReceipt()
                    {
                        OriginalTransactionId = "1000000180634175",
                        TransactionId = "1000000180634175",
                        Quantity = "1",
                        OriginalPurchaseDateMs = "1447777496000",
                        PurchaseDateMs = "1447777496000",
                        ProductId = "non_renewing_subscription",
                    },
                    new AppleInAppPurchaseReceipt()
                    {
                        OriginalTransactionId = "1000000180635846",
                        TransactionId = "1000000180635846",
                        Quantity = "1",
                        OriginalPurchaseDateMs = "1447777832000",
                        PurchaseDateMs = "1447777832000",
                        ProductId = "non_renewing_subscription"
                    },
                    new AppleInAppPurchaseReceipt()
                    {
                        OriginalTransactionId = "1000000180636226",
                        TransactionId = "1000000180703127",
                        Quantity = "1",
                        OriginalPurchaseDateMs = "1447820592000",
                        PurchaseDateMs = "1447821281000",
                        ProductId = "yearly",
                        ExpirationDateMs = "1448346881000"
                    },
                    new AppleInAppPurchaseReceipt()
                    {
                        OriginalTransactionId = "1000000180636226",
                        TransactionId = "1000000180636226",
                        Quantity = "1",
                        OriginalPurchaseDateMs = "1447778081000",
                        PurchaseDateMs = "1447778081000",
                        ProductId = "yearly",
                        ExpirationDateMs = "1447821281000"
                    },
                }
            };
            #endregion
            
            var data = Convert.FromBase64String(appleAppReceipt);
            var parserService = _container.GetRequiredService<IAppleReceiptParserService>();
            AppleAppReceipt applicant = parserService.GetAppleReceiptFromBytes(data);

            BasicResultCheck(ideal, applicant);
        }
        
        [Fact]
        public void ParsingTestTwo()
        {
            #region Receipt JSON

            // {
            //   "original_purchase_date_pst": "2015-11-17 08:34:41 America/Los_Angeles",
            //   "purchase_date_ms": "1447821281000",
            //   "unique_identifier": "dd0e33ebf56bb1f3ad4c2dbaa7a0b33ac68b0e85",
            //   "original_transaction_id": "1000000180636226",
            //   "expires_date": "1448346881000",
            //   "transaction_id": "1000000181298734",
            //   "original_purchase_date_ms": "1447778081000",
            //   "web_order_line_item_id": "1000000030938713",
            //   "bvrs": "1",
            //   "unique_vendor_identifier": "4E5E1A3B-F01A-4855-820F-F349F1242148",
            //   "expires_date_formatted_pst": "2015-11-23 22:34:41 America/Los_Angeles",
            //   "item_id": "1028950797",
            //   "expires_date_formatted": "2015-11-24 06:34:41 Etc/GMT",
            //   "product_id": "yearly",
            // {
            //   :application_version=>"1",
            //   :original_application_version=>"1.0",
            //   :environment=>"ProductionSandbox",
            //   :bundle_id=>"com.mbaasy.ios.demo",
            //   :creation_date=>"2015-08-13T07:50:46Z",
            //   :in_app=> [{
            //     :expires_date=>"",
            //     :cancellation_date=>"",
            //     :quantity=>1,
            //     :web_order_line_item_id=>0,
            //     :product_id=>"consumable",
            //     :transaction_id=>"1000000166865231",
            //     :original_transaction_id=>"1000000166865231",
            //     :purchase_date=>"2015-08-07T20:37:55Z",
            //     :original_purchase_date=>"2015-08-07T20:37:55Z"
            //   }, {
            //     :cancellation_date=>"",
            //     :quantity=>1,
            //     :product_id=>"monthly",
            //     :web_order_line_item_id=>1000000030274153,
            //     :transaction_id=>"1000000166965150",
            //     :original_transaction_id=>"1000000166965150",
            //     :purchase_date=>"2015-08-10T06:49:32Z",
            //     :original_purchase_date=>"2015-08-10T06:49:33Z",
            //     :expires_date=>"2015-08-10T06:54:32Z"
            //   }, {
            //     :cancellation_date=>"",
            //     :quantity=>1,
            //     :product_id=>"monthly",
            //     :web_order_line_item_id=>1000000030274154,
            //     :transaction_id=>"1000000166965327",
            //     :original_transaction_id=>"1000000166965150",
            //     :purchase_date=>"2015-08-10T06:54:32Z",
            //     :original_purchase_date=>"2015-08-10T06:53:18Z",
            //     :expires_date=>"2015-08-10T06:59:32Z"
            //   }, {
            //     :cancellation_date=>"",
            //     :quantity=>1,
            //     :product_id=>"monthly",
            //     :web_order_line_item_id=>1000000030274165,
            //     :transaction_id=>"1000000166965895",
            //     :original_transaction_id=>"1000000166965150",
            //     :purchase_date=>"2015-08-10T06:59:32Z",
            //     :original_purchase_date=>"2015-08-10T06:57:34Z",
            //     :expires_date=>"2015-08-10T07:04:32Z"
            //   }, {
            //     :cancellation_date=>"",
            //     :quantity=>1,
            //     :product_id=>"monthly",
            //     :web_order_line_item_id=>1000000030274192,
            //     :transaction_id=>"1000000166967152",
            //     :original_transaction_id=>"1000000166965150",
            //     :purchase_date=>"2015-08-10T07:04:32Z",
            //     :original_purchase_date=>"2015-08-10T07:02:33Z",
            //     :expires_date=>"2015-08-10T07:09:32Z"
            //   }, {
            //     :cancellation_date=>"",
            //     :quantity=>1,
            //     :product_id=>"monthly",
            //     :web_order_line_item_id=>1000000030274219,
            //     :transaction_id=>"1000000166967484",
            //     :original_transaction_id=>"1000000166965150",
            //     :purchase_date=>"2015-08-10T07:09:32Z",
            //     :original_purchase_date=>"2015-08-10T07:08:30Z",
            //     :expires_date=>"2015-08-10T07:14:32Z"
            //   }, {
            //     :cancellation_date=>"",
            //     :quantity=>1,
            //     :product_id=>"monthly",
            //     :web_order_line_item_id=>1000000030274249,
            //     :transaction_id=>"1000000166967782",
            //     :original_transaction_id=>"1000000166965150",
            //     :purchase_date=>"2015-08-10T07:14:32Z",
            //     :original_purchase_date=>"2015-08-10T07:12:34Z",
            //     :expires_date=>"2015-08-10T07:19:32Z"
            //   }]
            // }

            #endregion
            #region Receipt Encoded

            const string appleAppReceipt =
                "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";
            

            #endregion
            #region Receipt Object

            AppleAppReceipt ideal = new AppleAppReceipt
            {
                ApplicationVersion = "1",
                OriginalApplicationVersion = "1.0",
                ReceiptCreationDateMs = "1439452246000",
                BundleId = "com.mbaasy.ios.demo",
                OriginalPurchaseDateMs = "1375340400000",
                PurchaseReceipts = new List<AppleInAppPurchaseReceipt>
                {
                    new AppleInAppPurchaseReceipt()
                    {
                        OriginalTransactionId = "1000000166865231",
                        TransactionId = "1000000166865231",
                        Quantity = "1",
                        OriginalPurchaseDateMs = "1438979875000",
                        PurchaseDateMs = "1438979875000",
                        ProductId = "consumable"
                    },
                    new AppleInAppPurchaseReceipt()
                    {
                        OriginalTransactionId = "1000000166965150",
                        TransactionId = "1000000166965150",
                        Quantity = "1",
                        OriginalPurchaseDateMs = "1439189373000",
                        PurchaseDateMs = "1439189372000",
                        ProductId = "monthly",
                        ExpirationDateMs = "1439189672000"
                    },
                    new AppleInAppPurchaseReceipt()
                    {
                        OriginalTransactionId = "1000000166965150",
                        TransactionId = "1000000166965327",
                        Quantity = "1",
                        OriginalPurchaseDateMs = "1439189598000",
                        PurchaseDateMs = "1439189672000",
                        ProductId = "monthly",
                        ExpirationDateMs = "1439189972000"
                    },
                    new AppleInAppPurchaseReceipt()
                    {
                        OriginalTransactionId = "1000000166965150",
                        TransactionId = "1000000166965895",
                        Quantity = "1",
                        OriginalPurchaseDateMs = "1439189854000",
                        PurchaseDateMs = "1439189972000",
                        ProductId = "monthly",
                        ExpirationDateMs = "1439190272000"
                    },
                    new AppleInAppPurchaseReceipt()
                    {
                        OriginalTransactionId = "1000000166965150",
                        TransactionId = "1000000166967152",
                        Quantity = "1",
                        OriginalPurchaseDateMs = "1439190153000",
                        PurchaseDateMs = "1439190272000",
                        ProductId = "monthly",
                        ExpirationDateMs = "1439190572000"
                    },
                    new AppleInAppPurchaseReceipt()
                    {
                        OriginalTransactionId = "1000000166965150",
                        TransactionId = "1000000166967484",
                        Quantity = "1",
                        OriginalPurchaseDateMs = "1439190510000",
                        PurchaseDateMs = "1439190572000",
                        ProductId = "monthly",
                        ExpirationDateMs = "1439190872000"
                    },
                    new AppleInAppPurchaseReceipt()
                    {
                        OriginalTransactionId = "1000000166965150",
                        TransactionId = "1000000166967782",
                        Quantity = "1",
                        OriginalPurchaseDateMs = "1439190754000",
                        PurchaseDateMs = "1439190872000",
                        ProductId = "monthly",
                        ExpirationDateMs = "1439191172000"
                    },
                }
            };

            #endregion
            
            var data = Convert.FromBase64String(appleAppReceipt);
            var parserService = _container.GetRequiredService<IAppleReceiptParserService>();
            AppleAppReceipt applicant = parserService.GetAppleReceiptFromBytes(data);

            BasicResultCheck(ideal, applicant);
        }
        
        [Fact]
        public void ParsingTestThree()
        {
            #region Receipt JSON

            // {
            //     "receipt": {
            //         "receipt_type": "ProductionSandbox",
            //         "adam_id": 0,
            //         "app_item_id": 0,
            //         "bundle_id": "com.belive.app.ios",
            //         "application_version": "3",
            //         "download_id": 0,
            //         "version_external_identifier": 0,
            //         "receipt_creation_date": "2018-11-13 16:46:31 Etc/GMT",
            //         "receipt_creation_date_ms": "1542127591000",
            //         "receipt_creation_date_pst": "2018-11-13 08:46:31 America/Los_Angeles",
            //         "request_date": "2018-11-13 17:10:31 Etc/GMT",
            //         "request_date_ms": "1542129031280",
            //         "request_date_pst": "2018-11-13 09:10:31 America/Los_Angeles",
            //         "original_purchase_date": "2013-08-01 07:00:00 Etc/GMT",
            //         "original_purchase_date_ms": "1375340400000",
            //         "original_purchase_date_pst": "2013-08-01 00:00:00 America/Los_Angeles",
            //         "original_application_version": "1.0",
            //         "in_app": [{
            //             "quantity": "1",
            //             "product_id": "test2",
            //             "transaction_id": "1000000472106082",
            //             "original_transaction_id": "1000000472106082",
            //             "purchase_date": "2018-11-13 16:46:31 Etc/GMT",
            //             "purchase_date_ms": "1542127591000",
            //             "purchase_date_pst": "2018-11-13 08:46:31 America/Los_Angeles",
            //             "original_purchase_date": "2018-11-13 16:46:31 Etc/GMT",
            //             "original_purchase_date_ms": "1542127591000",
            //             "original_purchase_date_pst": "2018-11-13 08:46:31 America/Los_Angeles",
            //             "is_trial_period": "false"
            //         }]
            //     },
            //     "status": 0,
            //     "environment": "Sandbox"
            // }

            #endregion
            #region Receipt Encoded

            const string appleAppReceipt = 
                "MIITuAYJKoZIhvcNAQcCoIITqTCCE6UCAQExCzAJBgUrDgMCGgUAMIIDWQYJKoZIhvcNAQcBoIIDSgSCA0YxggNCMAoCAQgCAQEEAhYAMAoCARQCAQEEAgwAMAsCAQECAQEEAwIBADALAgEDAgEBBAMMATMwCwIBCwIBAQQDAgEAMAsCAQ4CAQEEAwIBWjALAgEPAgEBBAMCAQAwCwIBEAIBAQQDAgEAMAsCARkCAQEEAwIBAzAMAgEKAgEBBAQWAjQrMA0CAQ0CAQEEBQIDAYfPMA0CARMCAQEEBQwDMS4wMA4CAQkCAQEEBgIEUDI1MDAYAgEEAgECBBA04jSbC9Zi5OwSemv9EK8kMBsCAQACAQEEEwwRUHJvZHVjdGlvblNhbmRib3gwHAIBAgIBAQQUDBJjb20uYmVsaXZlLmFwcC5pb3MwHAIBBQIBAQQUJzhO1BR1kxOVGrCEqQLkwvUuZP8wHgIBDAIBAQQWFhQyMDE4LTExLTEzVDE2OjQ2OjMxWjAeAgESAgEBBBYWFDIwMTMtMDgtMDFUMDc6MDA6MDBaMD0CAQcCAQEENedAPSDSwFz7IoNyAPZTI59czwFA1wkme6h1P/iicVNxpR8niuvFpKYx1pqnKR34cdDeJIzMMFECAQYCAQEESfQpXyBVFno5UWwqDFaMQ/jvbkZCDvz3/6RVKPU80KMCSp4onID0/AWet6BjZgagzrXtsEEdVLzfZ1ocoMuCNTOMyiWYS8uJj0YwggFKAgERAgEBBIIBQDGCATwwCwICBqwCAQEEAhYAMAsCAgatAgEBBAIMADALAgIGsAIBAQQCFgAwCwICBrICAQEEAgwAMAsCAgazAgEBBAIMADALAgIGtAIBAQQCDAAwCwICBrUCAQEEAgwAMAsCAga2AgEBBAIMADAMAgIGpQIBAQQDAgEBMAwCAgarAgEBBAMCAQEwDAICBq4CAQEEAwIBADAMAgIGrwIBAQQDAgEAMAwCAgaxAgEBBAMCAQAwEAICBqYCAQEEBwwFdGVzdDIwGwICBqcCAQEEEgwQMTAwMDAwMDQ3MjEwNjA4MjAbAgIGqQIBAQQSDBAxMDAwMDAwNDcyMTA2MDgyMB8CAgaoAgEBBBYWFDIwMTgtMTEtMTNUMTY6NDY6MzFaMB8CAgaqAgEBBBYWFDIwMTgtMTEtMTNUMTY6NDY6MzFaoIIOZTCCBXwwggRkoAMCAQICCA7rV4fnngmNMA0GCSqGSIb3DQEBBQUAMIGWMQswCQYDVQQGEwJVUzETMBEGA1UECgwKQXBwbGUgSW5jLjEsMCoGA1UECwwjQXBwbGUgV29ybGR3aWRlIERldmVsb3BlciBSZWxhdGlvbnMxRDBCBgNVBAMMO0FwcGxlIFdvcmxkd2lkZSBEZXZlbG9wZXIgUmVsYXRpb25zIENlcnRpZmljYXRpb24gQXV0aG9yaXR5MB4XDTE1MTExMzAyMTUwOVoXDTIzMDIwNzIxNDg0N1owgYkxNzA1BgNVBAMMLk1hYyBBcHAgU3RvcmUgYW5kIGlUdW5lcyBTdG9yZSBSZWNlaXB0IFNpZ25pbmcxLDAqBgNVBAsMI0FwcGxlIFdvcmxkd2lkZSBEZXZlbG9wZXIgUmVsYXRpb25zMRMwEQYDVQQKDApBcHBsZSBJbmMuMQswCQYDVQQGEwJVUzCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAKXPgf0looFb1oftI9ozHI7iI8ClxCbLPcaf7EoNVYb/pALXl8o5VG19f7JUGJ3ELFJxjmR7gs6JuknWCOW0iHHPP1tGLsbEHbgDqViiBD4heNXbt9COEo2DTFsqaDeTwvK9HsTSoQxKWFKrEuPt3R+YFZA1LcLMEsqNSIH3WHhUa+iMMTYfSgYMR1TzN5C4spKJfV+khUrhwJzguqS7gpdj9CuTwf0+b8rB9Typj1IawCUKdg7e/pn+/8Jr9VterHNRSQhWicxDkMyOgQLQoJe2XLGhaWmHkBBoJiY5uB0Qc7AKXcVz0N92O9gt2Yge4+wHz+KO0NP6JlWB7+IDSSMCAwEAAaOCAdcwggHTMD8GCCsGAQUFBwEBBDMwMTAvBggrBgEFBQcwAYYjaHR0cDovL29jc3AuYXBwbGUuY29tL29jc3AwMy13d2RyMDQwHQYDVR0OBBYEFJGknPzEdrefoIr0TfWPNl3tKwSFMAwGA1UdEwEB/wQCMAAwHwYDVR0jBBgwFoAUiCcXCam2GGCL7Ou69kdZxVJUo7cwggEeBgNVHSAEggEVMIIBETCCAQ0GCiqGSIb3Y2QFBgEwgf4wgcMGCCsGAQUFBwICMIG2DIGzUmVsaWFuY2Ugb24gdGhpcyBjZXJ0aWZpY2F0ZSBieSBhbnkgcGFydHkgYXNzdW1lcyBhY2NlcHRhbmNlIG9mIHRoZSB0aGVuIGFwcGxpY2FibGUgc3RhbmRhcmQgdGVybXMgYW5kIGNvbmRpdGlvbnMgb2YgdXNlLCBjZXJ0aWZpY2F0ZSBwb2xpY3kgYW5kIGNlcnRpZmljYXRpb24gcHJhY3RpY2Ugc3RhdGVtZW50cy4wNgYIKwYBBQUHAgEWKmh0dHA6Ly93d3cuYXBwbGUuY29tL2NlcnRpZmljYXRlYXV0aG9yaXR5LzAOBgNVHQ8BAf8EBAMCB4AwEAYKKoZIhvdjZAYLAQQCBQAwDQYJKoZIhvcNAQEFBQADggEBAA2mG9MuPeNbKwduQpZs0+iMQzCCX+Bc0Y2+vQ+9GvwlktuMhcOAWd/j4tcuBRSsDdu2uP78NS58y60Xa45/H+R3ubFnlbQTXqYZhnb4WiCV52OMD3P86O3GH66Z+GVIXKDgKDrAEDctuaAEOR9zucgF/fLefxoqKm4rAfygIFzZ630npjP49ZjgvkTbsUxn/G4KT8niBqjSl/OnjmtRolqEdWXRFgRi48Ff9Qipz2jZkgDJwYyz+I0AZLpYYMB8r491ymm5WyrWHWhumEL1TKc3GZvMOxx6GUPzo22/SGAGDDaSK+zeGLUR2i0j0I78oGmcFxuegHs5R0UwYS/HE6gwggQiMIIDCqADAgECAggB3rzEOW2gEDANBgkqhkiG9w0BAQUFADBiMQswCQYDVQQGEwJVUzETMBEGA1UEChMKQXBwbGUgSW5jLjEmMCQGA1UECxMdQXBwbGUgQ2VydGlmaWNhdGlvbiBBdXRob3JpdHkxFjAUBgNVBAMTDUFwcGxlIFJvb3QgQ0EwHhcNMTMwMjA3MjE0ODQ3WhcNMjMwMjA3MjE0ODQ3WjCBljELMAkGA1UEBhMCVVMxEzARBgNVBAoMCkFwcGxlIEluYy4xLDAqBgNVBAsMI0FwcGxlIFdvcmxkd2lkZSBEZXZlbG9wZXIgUmVsYXRpb25zMUQwQgYDVQQDDDtBcHBsZSBXb3JsZHdpZGUgRGV2ZWxvcGVyIFJlbGF0aW9ucyBDZXJ0aWZpY2F0aW9uIEF1dGhvcml0eTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMo4VKbLVqrIJDlI6Yzu7F+4fyaRvDRTes58Y4Bhd2RepQcjtjn+UC0VVlhwLX7EbsFKhT4v8N6EGqFXya97GP9q+hUSSRUIGayq2yoy7ZZjaFIVPYyK7L9rGJXgA6wBfZcFZ84OhZU3au0Jtq5nzVFkn8Zc0bxXbmc1gHY2pIeBbjiP2CsVTnsl2Fq/ToPBjdKT1RpxtWCcnTNOVfkSWAyGuBYNweV3RY1QSLorLeSUheHoxJ3GaKWwo/xnfnC6AllLd0KRObn1zeFM78A7SIym5SFd/Wpqu6cWNWDS5q3zRinJ6MOL6XnAamFnFbLw/eVovGJfbs+Z3e8bY/6SZasCAwEAAaOBpjCBozAdBgNVHQ4EFgQUiCcXCam2GGCL7Ou69kdZxVJUo7cwDwYDVR0TAQH/BAUwAwEB/zAfBgNVHSMEGDAWgBQr0GlHlHYJ/vRrjS5ApvdHTX8IXjAuBgNVHR8EJzAlMCOgIaAfhh1odHRwOi8vY3JsLmFwcGxlLmNvbS9yb290LmNybDAOBgNVHQ8BAf8EBAMCAYYwEAYKKoZIhvdjZAYCAQQCBQAwDQYJKoZIhvcNAQEFBQADggEBAE/P71m+LPWybC+P7hOHMugFNahui33JaQy52Re8dyzUZ+L9mm06WVzfgwG9sq4qYXKxr83DRTCPo4MNzh1HtPGTiqN0m6TDmHKHOz6vRQuSVLkyu5AYU2sKThC22R1QbCGAColOV4xrWzw9pv3e9w0jHQtKJoc/upGSTKQZEhltV/V6WId7aIrkhoxK6+JJFKql3VUAqa67SzCu4aCxvCmA5gl35b40ogHKf9ziCuY7uLvsumKV8wVjQYLNDzsdTJWk26v5yZXpT+RN5yaZgem8+bQp0gF6ZuEujPYhisX4eOGBrr/TkJ2prfOv/TgalmcwHFGlXOxxioK0bA8MFR8wggS7MIIDo6ADAgECAgECMA0GCSqGSIb3DQEBBQUAMGIxCzAJBgNVBAYTAlVTMRMwEQYDVQQKEwpBcHBsZSBJbmMuMSYwJAYDVQQLEx1BcHBsZSBDZXJ0aWZpY2F0aW9uIEF1dGhvcml0eTEWMBQGA1UEAxMNQXBwbGUgUm9vdCBDQTAeFw0wNjA0MjUyMTQwMzZaFw0zNTAyMDkyMTQwMzZaMGIxCzAJBgNVBAYTAlVTMRMwEQYDVQQKEwpBcHBsZSBJbmMuMSYwJAYDVQQLEx1BcHBsZSBDZXJ0aWZpY2F0aW9uIEF1dGhvcml0eTEWMBQGA1UEAxMNQXBwbGUgUm9vdCBDQTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAOSRqQkfkdseR1DrBe1eeYQt6zaiV0xV7IsZid75S2z1B6siMALoGD74UAnTf0GomPnRymacJGsR0KO75Bsqwx+VnnoMpEeLW9QWNzPLxA9NzhRp0ckZcvVdDtV/X5vyJQO6VY9NXQ3xZDUjFUsVWR2zlPf2nJ7PULrBWFBnjwi0IPfLrCwgb3C2PwEwjLdDzw+dPfMrSSgayP7OtbkO2V4c1ss9tTqt9A8OAJILsSEWLnTVPA3bYharo3GSR1NVwa8vQbP4++NwzeajTEV+H0xrUJZBicR0YgsQg0GHM4qBsTBY7FoEMoxos48d3mVz/2deZbxJ2HafMxRloXeUyS0CAwEAAaOCAXowggF2MA4GA1UdDwEB/wQEAwIBBjAPBgNVHRMBAf8EBTADAQH/MB0GA1UdDgQWBBQr0GlHlHYJ/vRrjS5ApvdHTX8IXjAfBgNVHSMEGDAWgBQr0GlHlHYJ/vRrjS5ApvdHTX8IXjCCAREGA1UdIASCAQgwggEEMIIBAAYJKoZIhvdjZAUBMIHyMCoGCCsGAQUFBwIBFh5odHRwczovL3d3dy5hcHBsZS5jb20vYXBwbGVjYS8wgcMGCCsGAQUFBwICMIG2GoGzUmVsaWFuY2Ugb24gdGhpcyBjZXJ0aWZpY2F0ZSBieSBhbnkgcGFydHkgYXNzdW1lcyBhY2NlcHRhbmNlIG9mIHRoZSB0aGVuIGFwcGxpY2FibGUgc3RhbmRhcmQgdGVybXMgYW5kIGNvbmRpdGlvbnMgb2YgdXNlLCBjZXJ0aWZpY2F0ZSBwb2xpY3kgYW5kIGNlcnRpZmljYXRpb24gcHJhY3RpY2Ugc3RhdGVtZW50cy4wDQYJKoZIhvcNAQEFBQADggEBAFw2mUwteLftjJvc83eb8nbSdzBPwR+Fg4UbmT1HN/Kpm0COLNSxkBLYvvRzm+7SZA/LeU802KI++Xj/a8gH7H05g4tTINM4xLG/mk8Ka/8r/FmnBQl8F0BWER5007eLIztHo9VvJOLr0bdw3w9F4SfK8W147ee1Fxeo3H4iNcol1dkP1mvUoiQjEfehrI9zgWDGG1sJL5Ky+ERI8GA4nhX1PSZnIIozavcNgs/e66Mv+VNqW2TAYzN39zoHLFbr2g8hDtq6cxlPtdk2f8GHVdmnmbkyQvvY1XGefqFStxu9k0IkEirHDx22TZxeY8hLgBdQqorV2uT80AkHN7B1dSExggHLMIIBxwIBATCBozCBljELMAkGA1UEBhMCVVMxEzARBgNVBAoMCkFwcGxlIEluYy4xLDAqBgNVBAsMI0FwcGxlIFdvcmxkd2lkZSBEZXZlbG9wZXIgUmVsYXRpb25zMUQwQgYDVQQDDDtBcHBsZSBXb3JsZHdpZGUgRGV2ZWxvcGVyIFJlbGF0aW9ucyBDZXJ0aWZpY2F0aW9uIEF1dGhvcml0eQIIDutXh+eeCY0wCQYFKw4DAhoFADANBgkqhkiG9w0BAQEFAASCAQCJ9ctD+7Yi9JWvl6G+1HOcDO++mhY6rc6japAgogVF4xmIdh275IKRwZKpQbhoJmxXwElbMjkIsXks/48/EzuaHDQBNIVowq8qQaSUb3msvfAZfi7RGnhaJGzkXf7azr9NLMxX29R2jTiw2oaz2ri49piggmrGfXsLjWs9zTHWHHNRN1fLTPtcWb95JbQNAiQqlecG5a95/+KZ7+joh8fQwbthe8oWs5Tla0DDwrEoIbc5yjFT18Dln5bndTvWQJZcsbI4xa7BAEhjg/nfwPhaL17tHZeW8mOcCtG9UcuAgXXC6usVAOSocenhmKUR8W+D6F/jhBn0k9ahApPDmpZh";

            #endregion
            #region Receipt Object

            AppleAppReceipt ideal = new AppleAppReceipt
            {
                ApplicationVersion = "3",
                OriginalApplicationVersion = "1.0",
                ReceiptCreationDateMs = "1542127591000",
                BundleId = "com.belive.app.ios",
                OriginalPurchaseDateMs = "1375340400000",
                PurchaseReceipts = new List<AppleInAppPurchaseReceipt>
                {
                    new AppleInAppPurchaseReceipt()
                    {
                        OriginalTransactionId = "1000000472106082",
                        TransactionId = "1000000472106082",
                        Quantity = "1",
                        OriginalPurchaseDateMs = "1542127591000",
                        PurchaseDateMs = "1542127591000",
                        ProductId = "test2"
                    }
                }
            };

            #endregion
            
            byte[] data = Convert.FromBase64String(appleAppReceipt);
            var parserService = _container.GetRequiredService<IAppleReceiptParserService>();
            AppleAppReceipt applicant = parserService.GetAppleReceiptFromBytes(data);
            
            BasicResultCheck(ideal, applicant);
        }

        private void BasicResultCheck(AppleAppReceipt ideal, AppleAppReceipt applicant)
        {
            if (applicant != null)
            {
                var idealStr = Newtonsoft.Json.JsonConvert.SerializeObject(ideal);
                var applicantStr = Newtonsoft.Json.JsonConvert.SerializeObject(applicant);
                ComparisonResult result = new CompareLogic().Compare(ideal, applicant);
                Assert.True(result.AreEqual);
            }
            else
            {
                throw new Exception("Receipt Data cannot be parsed from bytes");
            }
        }
    }
}
