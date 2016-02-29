namespace SDK.Nimblepayments.Base
{
    using Newtonsoft.Json;

    internal class NimbleApiResult<T> : NimbleApiResult
    {
        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
    }

    internal class NimbleApiResult
    {
        [JsonProperty(PropertyName = "result")]
        public Result Operation { get; set; }
    }
}
