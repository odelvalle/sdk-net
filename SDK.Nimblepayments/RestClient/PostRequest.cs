namespace SDK.Nimblepayments.RestClient
{
    public class PostRequest : RestRequest
    {
        public PostRequest(string url): base(url, Method.POST, ContentType.FormUrlEncoded)
        {
        }

        public PostRequest(string url, ContentType contentType) : base(url, Method.POST, contentType)
        {
        }
    }
}
