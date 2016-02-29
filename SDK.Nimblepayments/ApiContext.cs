namespace SDK.Nimblepayments
{
    using global::SDK.Nimblepayments.Auth;
    using global::SDK.Nimblepayments.Enviroment;

    using SDK.Nimblepayments.RestClient;

    internal class ApiContext
    {
        internal ApiContext(NimbleAuth nimbleAuth)
        {
            this.NimbleAuth = nimbleAuth;

            this.RestClient = new RestClient.RestClient(new JsonResolver());
            this.EnviromentManager = new EnviromentManager(this);
        }

        internal NimbleAuth NimbleAuth { get; }

        internal IRestClient RestClient { get; }
        internal EnviromentManager EnviromentManager { get; }

    }
}
