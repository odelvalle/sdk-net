namespace SDK.Nimblepayments.Base
{
    using Newtonsoft.Json;

    internal class NimbleApiResult<T>
    {
        [JsonProperty(PropertyName = "result")]
        public Result Operation { get; set; }

        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
    }

    internal class NimbleApiResult
    {
        [JsonProperty(PropertyName = "result")]
        public Result Operation { get; set; }
    }
}
