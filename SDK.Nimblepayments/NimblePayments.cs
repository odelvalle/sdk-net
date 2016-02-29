namespace SDK.Nimblepayments
{
    using global::SDK.Nimblepayments.Auth;

    using SDK.Nimblepayments.Enviroment;
    using SDK.Nimblepayments.Payments;

    public class NimblePayments
    {
        private readonly ApiContext apiContext;

        public NimblePayments(NimbleAuth nimbleAuth)
        {
            this.Auth = nimbleAuth;
            this.apiContext = new ApiContext(this.Auth);

            this.Payments = new PaymentsOperations(this.apiContext);
        }

        public PaymentsOperations Payments { get; }
        public EnviromentManager NimbleEnviroment => this.apiContext.EnviromentManager;

        public NimbleAuth Auth { get; }
    }
}
