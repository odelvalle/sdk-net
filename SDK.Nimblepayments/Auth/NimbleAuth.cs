namespace SDK.Nimblepayments.Auth
{
    using System;

    using SDK.Nimblepayments.Enviroment;

    public class NimbleAuth
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public NimbleEnvironment Environment { get; set; }

        public string ApplicationToken { get; set; } 
        public DateTime TokenExpirationTime { get; set; }

        public bool TokenIsValid => DateTime.Now.AddSeconds(-60) <= this.TokenExpirationTime;

        internal string BasicAuthorization { get; set; }
    }
}
