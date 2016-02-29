namespace SDK.Nimblepayments.Base
{
    using Newtonsoft.Json;

    internal class Result
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "info")]
        public string Info { get; set; }

        [JsonProperty(PropertyName = "internal_code")]
        public string InternalCode { get; set; }
    }
}
