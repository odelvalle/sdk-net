namespace SDK.Nimblepayments.RestClient
{
    using System.Threading.Tasks;

    public interface IRestClient
    {
        Task<OperationResult<T>> Execute<T>(RestRequest request) where T: class;
    }
}