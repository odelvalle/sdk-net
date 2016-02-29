namespace SDK.Nimblepayments.RestClient
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IJsonResolver
    {
        Task<byte[]> Serialize(object graph);
        Task<string> SerializeToString(object graph);

        Task<T> Deserialize<T>(Stream respnseResult) where T : class;
    }
}
