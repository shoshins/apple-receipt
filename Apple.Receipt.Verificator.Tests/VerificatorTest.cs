using Apple.Receipt.Verificator.Models;
using Apple.Receipt.Verificator.Models.IAPVerification;
using Apple.Receipt.Verificator.Modules;
using Apple.Receipt.Verificator.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace Apple.Receipt.Verificator.Tests
{
    // To check your own Receipt:
    // 1. Put Your own VerifyReceiptSharedSecret (Line 29)
    // 2. Put Your own App ID into AllowedBundleIds (Line 31)
    // 3. Put Your own Receipt String into appleAppReceipt (Line 76)
    // 4. Correct Assert events in the test according to your receipt (Line 83 - ...). 
    public class VerificatorTest : IDisposable
    {
        private readonly IServiceProvider _container;

        public VerificatorTest()
        {
            var services = new ServiceCollection();
            services.RegisterAppleReceiptVerificator(x =>
            {
                x.VerifyReceiptSharedSecret = "Your Shared Key";
                x.VerificationType = AppleReceiptVerificationType.Sandbox;
                x.AllowedBundleIds = new[] { "Your.App.Id", "com.mbaasy.ios.demo"};
            });

            services.AddScoped<IAppleReceiptCustomVerificatorService, AppleReceiptCustomVerificatorService>();
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
                    .Select(type => new object[] { type })
                    .GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private IEnumerable<Type> Types()
            {
                yield return typeof(IAppleReceiptVerificatorService);
            }
        }

        [Fact]
        public async Task CheckVerificator()
        {
            const string appleAppReceipt =
                "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";
            
            var verificator = _container.GetRequiredService<IAppleReceiptVerificatorService>();

            // Usage option 1. Apple recommends you use such behaviour: https://developer.apple.com/documentation/storekit/in-app_purchase/validating_receipts_with_the_app_store
            // Like 'Check on prod and in case of 21004 check on sandbox'. 
            // BUT I CANNOT RECOMMEND THAT WAY, because Production Server cannot switch to Sandbox based on Apple Response.
            // Intruder would be able to send Sandbox data to your Server and get the Success response.
            // I Recommend the second option.
            AppleReceiptVerificationResult result = await verificator.VerifyAppleProductionReceiptAsync(appleAppReceipt).ConfigureAwait(false);
            if (result.Status == IAPVerificationResponseStatus.TestReceiptOnProd)
            {
                result = await verificator.VerifyAppleSandBoxReceiptAsync(appleAppReceipt).ConfigureAwait(false);
            }
            CheckResult(result);
            
            // Usage option 2. Determine if the server was requested from Preview environment
            // Or App belongs to Not published apps (based on version for example).
            var isPreviewEnvironmentOrAppIsBelongsToUnpublishedBasedOnSomePattern = true;
            result = isPreviewEnvironmentOrAppIsBelongsToUnpublishedBasedOnSomePattern
                ? await verificator.VerifyAppleSandBoxReceiptAsync(appleAppReceipt).ConfigureAwait(false)
                : await verificator.VerifyAppleProductionReceiptAsync(appleAppReceipt).ConfigureAwait(false);
            CheckResult(result);
            
            // Usage option 3. Btw, you still has previous option to setup usage in the configuration during a Server Init step.
            result = await verificator.VerifyAppleReceiptAsync(appleAppReceipt).ConfigureAwait(false);
            CheckResult(result);
        }
        
        [Fact]
        public void CheckAppleResponseParser()
        {
            const string mohammadExample = "{\"receipt\":{\"original_purchase_date_pst\":\"2020-01-3104:14:01America/Los_Angeles\",\"quantity\":\"1\",\"unique_vendor_identifier\":\"666\",\"bvrs\":\"666\",\"expires_date_formatted\":\"2020-02-2912:14:00Etc/GMT\",\"is_in_intro_offer_period\":\"false\",\"purchase_date_ms\":\"1580472840802\",\"expires_date_formatted_pst\":\"2020-02-2904:14:00America/Los_Angeles\",\"is_trial_period\":\"false\",\"item_id\":\"666\",\"unique_identifier\":\"666\",\"original_transaction_id\":\"666\",\"subscription_group_identifier\":\"666\",\"app_item_id\":\"666\",\"transaction_id\":\"666\",\"web_order_line_item_id\":\"666\",\"version_external_identifier\":\"666\",\"purchase_date\":\"2020-01-3112:14:00Etc/GMT\",\"product_id\":\"666\",\"expires_date\":\"1582978440802\",\"original_purchase_date\":\"2020-01-3112:14:01Etc/GMT\",\"purchase_date_pst\":\"2020-01-3104:14:00America/Los_Angeles\",\"bid\":\"666\",\"original_purchase_date_ms\":\"1580472841559\"},\"auto_renew_product_id\":\"666\",\"auto_renew_status\":0,\"latest_receipt_info\":{\"original_purchase_date_pst\":\"2020-01-3104:14:01America/Los_Angeles\",\"quantity\":\"1\",\"unique_vendor_identifier\":\"666\",\"bvrs\":\"666\",\"expires_date_formatted\":\"2021-12-1211:34:15Etc/GMT\",\"is_in_intro_offer_period\":\"false\",\"purchase_date_ms\":\"1607772855000\",\"expires_date_formatted_pst\":\"2021-12-1203:34:15America/Los_Angeles\",\"is_trial_period\":\"false\",\"item_id\":\"666\",\"unique_identifier\":\"666\",\"original_transaction_id\":\"666\",\"subscription_group_identifier\":\"666\",\"app_item_id\":\"666\",\"transaction_id\":\"666\",\"in_app_ownership_type\":\"PURCHASED\",\"web_order_line_item_id\":\"666\",\"purchase_date\":\"2020-12-1211:34:15Etc/GMT\",\"product_id\":\"666\",\"expires_date\":\"1639308855000\",\"original_purchase_date\":\"2020-01-3112:14:01Etc/GMT\",\"purchase_date_pst\":\"2020-12-1203:34:15America/Los_Angeles\",\"bid\":\"666\",\"original_purchase_date_ms\":\"1580472841000\"},\"latest_receipt\":\"666\",\"status\":0}";

            var appleResponseObject = JsonConvert.DeserializeObject<IAPVerificationResponse>(mohammadExample);
            
            Assert.NotNull(appleResponseObject);
            Assert.NotNull(appleResponseObject.Receipt);
            Assert.Equal(666, appleResponseObject.Receipt.VersionExternalIdentifier);
            
            const string internetExample =
                "{\"status\":0,\"environment\":\"Sandbox\",\"receipt\":{\"receipt_type\":\"ProductionSandbox\",\"adam_id\":0,\"app_item_id\":0,\"bundle_id\":\"com.apphud.subscriptionstest\",\"application_version\":\"1\",\"download_id\":0,\"version_external_identifier\":0,\"receipt_creation_date\":\"2019-10-1422:00:55Etc/GMT\",\"receipt_creation_date_ms\":\"1571090455000\",\"receipt_creation_date_pst\":\"2019-10-1415:00:55America/Los_Angeles\",\"request_date\":\"2019-10-1512:54:10Etc/GMT\",\"request_date_ms\":\"1571144050441\",\"request_date_pst\":\"2019-10-1505:54:10America/Los_Angeles\",\"original_purchase_date\":\"2013-08-0107:00:00Etc/GMT\",\"original_purchase_date_ms\":\"1375340400000\",\"original_purchase_date_pst\":\"2013-08-0100:00:00America/Los_Angeles\",\"original_application_version\":\"1.0\",\"in_app\":[{\"quantity\":\"1\",\"product_id\":\"SixthWeekly\",\"transaction_id\":\"1000000579060971\",\"original_transaction_id\":\"1000000579060971\",\"purchase_date\":\"2019-10-1422:00:54Etc/GMT\",\"purchase_date_ms\":\"1571090454000\",\"purchase_date_pst\":\"2019-10-1415:00:54America/Los_Angeles\",\"original_purchase_date\":\"2019-10-1422:00:55Etc/GMT\",\"original_purchase_date_ms\":\"1571090455000\",\"original_purchase_date_pst\":\"2019-10-1415:00:55America/Los_Angeles\",\"expires_date\":\"2019-10-1422:03:54Etc/GMT\",\"expires_date_ms\":\"1571090634000\",\"expires_date_pst\":\"2019-10-1415:03:54America/Los_Angeles\",\"web_order_line_item_id\":\"1000000047532125\",\"is_trial_period\":\"true\",\"is_in_intro_offer_period\":\"false\"}]},\"latest_receipt_info\":[{\"quantity\":\"1\",\"product_id\":\"SixthWeekly\",\"transaction_id\":\"1000000579060971\",\"original_transaction_id\":\"1000000579060971\",\"purchase_date\":\"2019-10-1422:00:54Etc/GMT\",\"purchase_date_ms\":\"1571090454000\",\"purchase_date_pst\":\"2019-10-1415:00:54America/Los_Angeles\",\"original_purchase_date\":\"2019-10-1422:00:55Etc/GMT\",\"original_purchase_date_ms\":\"1571090455000\",\"original_purchase_date_pst\":\"2019-10-1415:00:55America/Los_Angeles\",\"expires_date\":\"2019-10-1422:03:54Etc/GMT\",\"expires_date_ms\":\"1571090634000\",\"expires_date_pst\":\"2019-10-1415:03:54America/Los_Angeles\",\"web_order_line_item_id\":\"1000000047532125\",\"is_trial_period\":\"true\",\"is_in_intro_offer_period\":\"false\",\"subscription_group_identifier\":\"20537620\"},{\"quantity\":\"1\",\"product_id\":\"SixthWeekly\",\"transaction_id\":\"1000000579061533\",\"original_transaction_id\":\"1000000579060971\",\"purchase_date\":\"2019-10-1422:03:54Etc/GMT\",\"purchase_date_ms\":\"1571090634000\",\"purchase_date_pst\":\"2019-10-1415:03:54America/Los_Angeles\",\"original_purchase_date\":\"2019-10-1422:00:55Etc/GMT\",\"original_purchase_date_ms\":\"1571090455000\",\"original_purchase_date_pst\":\"2019-10-1415:00:55America/Los_Angeles\",\"expires_date\":\"2019-10-1422:06:54Etc/GMT\",\"expires_date_ms\":\"1571090814000\",\"expires_date_pst\":\"2019-10-1415:06:54America/Los_Angeles\",\"web_order_line_item_id\":\"1000000047532126\",\"is_trial_period\":\"false\",\"is_in_intro_offer_period\":\"false\",\"subscription_group_identifier\":\"20537620\"}],\"latest_receipt\":\"...\",\"pending_renewal_info\":[{\"expiration_intent\":\"1\",\"auto_renew_product_id\":\"SixthWeekly\",\"original_transaction_id\":\"1000000579060971\",\"is_in_billing_retry_period\":\"0\",\"product_id\":\"SixthWeekly\",\"auto_renew_status\":\"0\"}]}";

            appleResponseObject = JsonConvert.DeserializeObject<IAPVerificationResponse>(internetExample);
            
            Assert.NotNull(appleResponseObject);
            Assert.NotNull(appleResponseObject.Receipt);
            Assert.Equal("ProductionSandbox", appleResponseObject.Receipt.ReceiptType);
            Assert.Equal("com.apphud.subscriptionstest", appleResponseObject.Receipt.BundleId);
            
            const string internetExample2 =
                "{\"receipt\":{\"original_purchase_date_pst\":\"2012-04-3008:05:55America/Los_Angeles\",\"original_transaction_id\":\"1000000046178817\",\"original_purchase_date_ms\":\"1335798355868\",\"transaction_id\":\"1000000046178817\",\"quantity\":\"1\",\"product_id\":\"br.com.jera.Example\",\"bvrs\":\"20120427\",\"purchase_date_ms\":\"1335798355868\",\"purchase_date\":\"2012-04-3015:05:55Etc/GMT\",\"original_purchase_date\":\"2012-04-3015:05:55Etc/GMT\",\"purchase_date_pst\":\"2012-04-3008:05:55America/Los_Angeles\",\"bid\":\"br.com.jera.Example\",\"item_id\":\"521129812\"},\"status\":0}";

            appleResponseObject = JsonConvert.DeserializeObject<IAPVerificationResponse>(internetExample2);

            Assert.NotNull(appleResponseObject);
            Assert.NotNull(appleResponseObject.Receipt);
            Assert.NotNull(appleResponseObject.Receipt.OriginalPurchaseDateDt);
            
            const string internetExample3 =
            "{\"receipt\":{\"in_app\":[{is_trial_period:\"false\",original_purchase_date_pst:\"2013-10-0920:55:27America/Los_Angeles\",original_purchase_date_ms:\"1386571707000\",original_purchase_date:\"2013-10-0904:55:27Etc/GMT\",purchase_date_pst:\"2013-10-0920:55:27America/Los_Angeles\",purchase_date_ms:\"1386571707000\",purchase_date:\"2013-10-0904:55:27Etc/GMT\",original_transaction_id:\"654888452251325\",transaction_id:\"654888452251325\",product_id:\"com.example.mygame.tool1\",quantity:\"1\"}],original_application_version:\"1.0\",original_purchase_date_pst:\"2013-10-0920:55:27America/Los_Angeles\",original_purchase_date_ms:\"1386569706000\",original_purchase_date:\"2013-10-0904:55:27Etc/GMT\",request_date_pst:\"2013-10-0920:55:27America/Los_Angeles\",request_date_ms:\"1386571710087\",request_date:\"2013-10-0904:55:27Etc/GMT\",download_id:215425636588954,application_version:\"1.0\",bundle_id:\"com.example.mygame\",adam_id:654225311,receipt_type:\"Sandbox\"},environment:\"Sandbox\",status:0}";

            appleResponseObject = JsonConvert.DeserializeObject<IAPVerificationResponse>(internetExample3);

            Assert.NotNull(appleResponseObject);
            Assert.NotNull(appleResponseObject.Receipt);
            Assert.NotEmpty(appleResponseObject.Receipt.PurchaseReceipts);
            var originalPurchaseDateDt = appleResponseObject.Receipt.PurchaseReceipts.First().OriginalPurchaseDateDt;
            if (originalPurchaseDateDt != null)
            {
                Assert.Equal(2013,
                    originalPurchaseDateDt.Value.Year);
                Assert.Equal(2013,
                    originalPurchaseDateDt.Value.Year);
            }
            else
            {
                throw new Exception("Date cannot be parsed");
            }
        }
        
        private void CheckResult(AppleReceiptVerificationResult result)
        {
            Assert.NotNull(result);
            // Not OK Result check. Default.
            Assert.True(result.AppleVerificationResponse.StatusCode == IAPVerificationResponseStatus.NotAuthenticatedReceipt);
            // Not OK Result check. Your own Check.
            // Assert.True(result.AppleVerificationResponse.StatusCode == IAPVerificationResponseStatus.Ok);
            // Assert.True(result.AppleVerificationResponse.Receipt != null || result.AppleVerificationResponse.LatestReceiptInfo != null || result.AppleVerificationResponse.PendingRenewalInfo != null);
            // ...
        }
    }
}
