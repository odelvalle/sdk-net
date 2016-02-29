namespace SDK.Nimblepayments.RestClient
{
    public class GetRequest : RestRequest
    {
        public GetRequest(string url): base(url, Method.GET)
        {
        }
    }
}
