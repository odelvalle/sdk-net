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
                ClientId = "YOUR-CLIENT-ID",
                ClientSecrect = "YOUR-CLIENT-SECRECT",

                // If you are using real credentials, please... change Enviroment to NimbleEnviroment.Real
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
            if (validateResult.Exception != null) throw validateResult.Exception;

            Assert.AreEqual(HttpStatusCode.OK, validateResult.HttpStatusCode);
        }

        [TestMethod]
        public void TestPaymentUrl()
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
                });

            }).GetAwaiter().GetResult();

            Assert.IsNotNull(payment);
            if (payment.Exception != null) throw payment.Exception;

            Assert.IsNotNull(payment.Result.Id);
            Assert.IsNotNull(payment.Result.Url);
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
                });

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
            if (updateResult.Exception != null) throw updateResult.Exception;

            Assert.AreEqual(HttpStatusCode.OK, updateResult.HttpStatusCode);
        }

    }
}
