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
                ClientId = "EB648839EA8A49E695AD49F3865B27A9",
                ClientSecrect = "4BIfDXZm#pQxoBwW*kUS*nOR70wSsbTnhdbjhfy@90scweQWL7V@3195xIYvvmlG",

                // ATTENTION! Only use Sandbox Enviroment to run integration test.
                Enviroment = NimbleEnviroment.Sandbox
            });
        }

        [TestMethod]
        public void TestAccessToken()
        {
            OperationResult<HttpStatusCode> result = null;

            Task.Run(async () =>
            {
                result = await this.nimbleApi.Authorization.GetApplicationTsecAsync();
                
            }).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.HttpStatusCode);

            Assert.IsFalse(string.IsNullOrEmpty(this.nimbleApi.Auth.BasicAuthorization));
        }

        [TestMethod]
        public void TestSandboxCredentials()
        {
            OperationResult validateResult = null;
            Task.Run(async () =>
            {
                await this.nimbleApi.Authorization.GetApplicationTsecAsync();
                validateResult = await this.nimbleApi.NimbleEnviroment.VerifyCredentialsAsync(NimbleEnviroment.Sandbox);

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
                await this.nimbleApi.Authorization.GetApplicationTsecAsync();
                validateResult = await this.nimbleApi.NimbleEnviroment.VerifyCredentialsAsync(NimbleEnviroment.Real);

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
                await this.nimbleApi.Authorization.GetApplicationTsecAsync();
                operationResult = await this.nimbleApi.Payments.GetPaymentUrlAsync(new Payment
                {
                    UrlOk = "",
                    UrlKo = "",
                    Currency = "EUR",
                    Amount = 1000,
                    CustomerTransaction = "00-0000-00",
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
                await this.nimbleApi.Authorization.GetApplicationTsecAsync();
                payment = await this.nimbleApi.Payments.GetPaymentUrlAsync(new Payment
                {
                    UrlOk = "",
                    UrlKo = "",
                    Currency = "EUR",
                    Amount = 1000,
                    CustomerTransaction = "00-0000-00",
                }, PaymentLanguageUi.Es);

            }).GetAwaiter().GetResult();

            OperationResult updateResult = null;
            Task.Run(async () =>
            {
                updateResult = await this.nimbleApi.Payments.UpdatePaymentAsync(payment.Result.Id, new Order
                {
                    CustomerTransaction = "11-1111-11",
                });

            }).GetAwaiter().GetResult();

            Assert.IsNotNull(updateResult);
            Assert.IsNull(updateResult.Exception);

            Assert.AreEqual(HttpStatusCode.OK, updateResult.HttpStatusCode);
        }

    }
}
