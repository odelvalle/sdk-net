namespace SDK.Nimblepayments.RestClient
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class RestClient : IRestClient
    {
        private readonly IJsonResolver resolver;

        public RestClient(IJsonResolver resolver)
        {
            this.resolver = resolver;
        }

        public async Task<OperationResult<object>> Execute(RestRequest request)
        {
            return await this.Execute<object>(request);
        }

        public async Task<OperationResult<T>> Execute<T>(RestRequest request) where T: class 
        {
            var http = new HttpClient();
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            foreach (var item in request.Header)
            {
                http.DefaultRequestHeaders.Add(item.Key, item.Value);
            }

            HttpResponseMessage resp = null;

            try
            {
                switch (request.Method)
                {
                    case Method.GET:
                        resp = await http.GetAsync(request.Url);
                        break;

                    case Method.DELETE:
                        resp = await http.DeleteAsync(request.Url);
                        break;

                    case Method.PUT:
                    case Method.POST:

                        var ms = new MemoryStream(await this.resolver.Serialize(request.PostJsonParameter)) {Position = 0};
                        var sc = new StreamContent(ms);

                        if (!request.Header.ContainsKey("Content-Type"))
                        {
                            sc.Headers.ContentType = request.RequestContentType;
                        }

                        resp = request.Method == Method.POST ? await http.PostAsync(request.Url, sc) : await http.PutAsync(request.Url, sc);
                        ms.Dispose();

                        break;
                }

                resp?.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                return new OperationResult<T> { Exception = ex, HttpStatusCode = resp?.StatusCode ?? HttpStatusCode.BadRequest };
            }

            return new OperationResult<T>
            {
                HttpStatusCode = resp?.StatusCode ?? HttpStatusCode.NoContent,
                Result = (resp != null) ? await this.resolver.Deserialize<T>(await resp.Content.ReadAsStreamAsync()) : null
            };
        }
    }
}
