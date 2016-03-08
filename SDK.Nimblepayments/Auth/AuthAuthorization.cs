namespace SDK.Nimblepayments.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using SDK.Nimblepayments.RestClient;
    using SDK.Nimblepayments.Exceptions;

    public class AuthAuthorization
    {
        private readonly ApiContext apiContext;

        internal AuthAuthorization(ApiContext apiContext)
        {
            this.apiContext = apiContext;
            this.apiContext.NimbleAuth.BasicAuthorization = Base64Encode($"{apiContext.NimbleAuth.ClientId}:{apiContext.NimbleAuth.ClientSecret}");
        }

        protected async Task<OperationResult<HttpStatusCode>> GetApplicationTsecAsync()
        {
            if (this.apiContext.NimbleAuth.TokenIsValid) return new OperationResult<HttpStatusCode> { HttpStatusCode = HttpStatusCode.OK };

            var request = new RestRequest(this.apiContext.EnviromentManager.Auth2, Method.POST, ContentType.Json);
            request.AddParameter("grant_type", "client_credentials", ParameterType.QueryString);
            request.AddParameter("scope", "PAYMENT", ParameterType.QueryString);

            request.Header.Add(new KeyValuePair<string, string>("Authorization", $"Basic {this.apiContext.NimbleAuth.BasicAuthorization}"));

            var res = await this.apiContext.RestClient.Execute<AccessResult>(request);
            if (res.Exception != null) return new OperationResult<HttpStatusCode> { Exception = res.Exception, HttpStatusCode = res.HttpStatusCode };

            if (!string.IsNullOrEmpty(res.Result?.Error)) return new OperationResult<HttpStatusCode>
            {
                Exception = new NimbleApiException(res.Result.ErrorDescription),
                HttpStatusCode = res.HttpStatusCode
            };

            if (res.Result == null) return new OperationResult<HttpStatusCode>
            {
                Exception = new NimbleApiEmptyResultException(),
                HttpStatusCode = res.HttpStatusCode
            };

            this.apiContext.NimbleAuth.ApplicationToken = res.Result.Token;
            this.apiContext.NimbleAuth.TokenExpirationTime = DateTime.Now.AddSeconds(res.Result.ExpireIn);

            return new OperationResult<HttpStatusCode> { HttpStatusCode = HttpStatusCode.OK };
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
