using System;

namespace SDK.Nimblepayments.Payments
{
    using Newtonsoft.Json;

    public class UrlPayment
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "paymentUrl")]
        public Uri Url { get; set; }
    }
}
