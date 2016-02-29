namespace SDK.Nimblepayments.Auth
{
    using System;

    using SDK.Nimblepayments.Enviroment;

    public class NimbleAuth
    {
        public string ClientId { get; set; }
        public string ClientSecrect { get; set; }
        public NimbleEnviroment Enviroment { get; set; }

        // Read only properties
        public string BasicAuthorization { get; internal set; }
        public string ApplicationTsec { get; internal set; } 
        internal DateTime TsecExpireDateTime { get; set; }

        public bool TsecIsValid => DateTime.Now.AddSeconds(-60) <= this.TsecExpireDateTime;
    }
}
