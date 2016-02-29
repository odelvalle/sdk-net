namespace SDK.Nimblepayments.Payments
{
    using Newtonsoft.Json;

    public class Payment : Order
    {
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "paymentErrorUrl")]
        public string UrlKo { get; set; }

        [JsonProperty(PropertyName = "paymentSuccessUrl")]
        public string UrlOk { get; set; }
    }
}
