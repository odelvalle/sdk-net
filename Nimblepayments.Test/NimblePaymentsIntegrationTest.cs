namespace SDK.Nimblepayments.Test
{
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Nimblepayments;
    using Auth;
    using Enviroment;
    using Payments;
    using RestClient;

    [TestClass]
    public class NimblePaymentsIntegrationTest
    {
        private readonly NimblePayments nimbleApi;

        public NimblePaymentsIntegrationTest()
        {
            this.nimbleApi = new NimblePayments(new NimbleAuth
            {
                ClientId = "6A6364ED06D813D376A61BB3F932B3BC",
                ClientSecret = "558OJksE6guNDcmc&%wQrEQL@$xByTHwwRxzyGI4uoGE5*41cj&mXbJc@PmxWLhA",

                // ATTENTION! Only use Sandbox Environment to run integration test.
                Environment = NimbleEnvironment.Sandbox
            });
        }

        [TestMethod]
        public void TestSandboxCredentials()
        {
            OperationResult validateResult = null;
            Task.Run(async () =>
            {
                validateResult = await this.nimbleApi.NimbleEnviroment.VerifyCredentialsAsync(NimbleEnvironment.Sandbox);

            }).GetAwaiter().GetResult();

            Assert.IsNotNull(validateResult);
            Assert.IsNull(validateResult.Exception);

            Assert.AreEqual(HttpStatusCode.OK, validateResult.HttpStatusCode);
        }

        [TestMethod]
        public void TestFailRealCredentialsUsingSandboxCredentials()
        {
            OperationResult validateResult = null;
            Task.Run(async () =>
            {
                validateResult = await this.nimbleApi.NimbleEnviroment.VerifyCredentialsAsync(NimbleEnvironment.Real);

            }).GetAwaiter().GetResult();

            Assert.IsNotNull(validateResult);
            Assert.IsNotNull(validateResult.Exception);
            Assert.AreEqual(validateResult.HttpStatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void TestPaymentUrl()
        {
            OperationResult<UrlPayment> operationResult = null;
            Task.Run(async () =>
            {
                operationResult = await this.nimbleApi.Payments.GetPaymentUrlAsync(new Payment
                {
                    UrlOk = "",
                    UrlKo = "",
                    Currency = "EUR",
                    Amount = 1000,
                    CustomerTransaction = "00-0000-00",
                    User = "111111"
                }, PaymentLanguageUi.Es);

            }).GetAwaiter().GetResult();

            Assert.IsNotNull(operationResult);
            Assert.IsNull(operationResult.Exception);

            Assert.IsNotNull(operationResult.Result.Id);
            Assert.IsNotNull(operationResult.Result.Url);
        }

        [TestMethod]
        public void TestUpdatePayment()
        {
            OperationResult<UrlPayment> payment = null;
            Task.Run(async () =>
            {
                payment = await this.nimbleApi.Payments.GetPaymentUrlAsync(new Payment
                {
                    UrlOk = "",
                    UrlKo = "",
                    Currency = "EUR",
                    Amount = 1000,
                    CustomerTransaction = "00-0000-00",
                    User = "111111"
                }, PaymentLanguageUi.Es);

            }).GetAwaiter().GetResult();

            OperationResult updateResult = null;
            Task.Run(async () =>
            {
                updateResult = await this.nimbleApi.Payments.UpdatePaymentAsync(payment.Result.Id, new Order
                {
                    CustomerTransaction = "11-1111-11"
                });

            }).GetAwaiter().GetResult();

            Assert.IsNotNull(updateResult);
            Assert.IsNull(updateResult.Exception);

            Assert.AreEqual(HttpStatusCode.OK, updateResult.HttpStatusCode);
        }
    }
}
