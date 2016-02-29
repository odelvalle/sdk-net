namespace SDK.Nimblepayments.Enviroment
{
    using System.Threading.Tasks;

    using Base;
    using RestClient;

    public enum NimbleEnviroment
    {
        Sandbox,
        Real    
    }

    public class EnviromentManager : NimbleBaseOperations
    {
        private const string BaseUrlNimblepayment = "https://www.nimblepayments.com";

        private static readonly string BaseUrlNimblepaymentSandbox = $"{BaseUrlNimblepayment}/sandbox-api";
        private static readonly string BaseUrlNimblepaymentReal = $"{BaseUrlNimblepayment}/api";
        private static readonly string AuthUrlNimblepayment = $"{BaseUrlNimblepayment}/auth";

        private readonly string baseUrl;

        private readonly ApiContext apiContext;

        internal EnviromentManager(ApiContext apiContext)
        {
            var currentEnviroment = apiContext.NimbleAuth.Enviroment;
            this.apiContext = apiContext;

            this.baseUrl = $"{(currentEnviroment == NimbleEnviroment.Real ? BaseUrlNimblepaymentReal : BaseUrlNimblepaymentSandbox)}";
        }

        internal string Auth2 => $"{AuthUrlNimblepayment}/tsec/token";
        internal string SimplePayment => $"{this.baseUrl}/payments";
        internal string UpdatePayment => $"{this.baseUrl}/payments/{{id}}";

        public async Task<OperationResult> VerifyCredentialsAsync(NimbleEnviroment nimbleEnviroment)
        {
            var enviromentUrl = $"{(nimbleEnviroment == NimbleEnviroment.Real ? BaseUrlNimblepaymentReal : BaseUrlNimblepaymentSandbox)}";

            var request = new RestRequest($"{enviromentUrl}/check", Method.GET);
            request.Header.Add("Authorization", $"tsec {this.apiContext.NimbleAuth.ApplicationTsec}");

            var res = await this.apiContext.RestClient.Execute<NimbleApiResult<NullContentResult>>(request);
            return this.EnsureNimbleOperationResult(res);
        }
    }
}
