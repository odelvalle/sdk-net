namespace SDK.Nimblepayments.RestClient
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public class JsonResolver : IJsonResolver
    {
        public async Task<string> SerializeToString(object graph)
        {
            return await Task.Factory.StartNew(() => JsonConvert.SerializeObject(graph, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        public async Task<byte[]> Serialize(object graph)
        {
            if (graph is string) return Encoding.UTF8.GetBytes(graph as string);

            var json = await this.SerializeToString(graph);
            return Encoding.UTF8.GetBytes(json);
        }

        public async Task<T> Deserialize<T>(Stream respnseResult) where T : class
        {
            T result;

            using (var sr = new StreamReader(respnseResult))
            {
                var r = await sr.ReadToEndAsync();
                result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(r, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            }

            return result;
        }
    }
}
