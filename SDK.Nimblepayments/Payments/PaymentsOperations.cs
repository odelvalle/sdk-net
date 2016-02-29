﻿using System.Threading.Tasks;

namespace SDK.Nimblepayments.Payments
{
    using SDK.Nimblepayments.Base;
    using SDK.Nimblepayments.RestClient;

    public class PaymentsOperations : NimbleBaseOperations
    {
        private readonly ApiContext apiContext;
        internal PaymentsOperations(ApiContext apiContext)
        {
            this.apiContext = apiContext;
        }

        public async Task<OperationResult<UrlPayment>> GetPaymentUrlAsync(Payment payment)
        {
            var request = new RestRequest(this.apiContext.EnviromentManager.SimplePayment, Method.POST, ContentType.Json);
            request.AddParameter(payment);

            request.Header.Add("Accept-Language", "es");
            request.Header.Add("Authorization", $"tsec {this.apiContext.NimbleAuth.ApplicationTsec}");

            var response = await this.apiContext.RestClient.Execute<NimbleApiResult<UrlPayment>>(request);
            return this.EnsureNimbleOperationResult(response);
        }

        public async Task<OperationResult> UpdatePaymentAsync(string id, Order order)
        {
            var request = new RestRequest(this.apiContext.EnviromentManager.UpdatePayment, Method.PUT, ContentType.Json);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            request.AddParameter(order);

            request.Header.Add("Authorization", $"tsec {this.apiContext.NimbleAuth.ApplicationTsec}");
            request.Header.Add("vnd.bbva.session-id", "SDK.NET");

            var response = await this.apiContext.RestClient.Execute<NimbleApiResult<NullContentResult>>(request);
            return this.EnsureNimbleOperationResult(response);
        }
    }
}