namespace SDK.Nimblepayments.RestClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;

    public enum ContentType
    {
        FormUrlEncoded,
        Json
    }

    ///<summary>
    /// Types of parameters that can be added to requests
    ///</summary>
    public enum ParameterType
    {
        QueryString,
        Body,
        UrlSegment
    }

    /// <summary>
	/// HTTP method to use when making requests
	/// </summary>
	public enum Method
	{
        // ReSharper disable InconsistentNaming
		GET,
		POST,
		PUT,
		DELETE,
		HEAD,
		OPTIONS,
		PATCH
        // ReSharper restore InconsistentNaming
    }

    public enum ParameterEncodeType { Plain, Json, UrlEncoded }

    public class RestRequest
    {
        struct Parameter
        {
            public string Name { get; set; }
            public object Value { get; set; }

            public ParameterType ParameterType { get; set; }
        }

        private readonly IList<Parameter> parameters;
        private string url;
        protected readonly ContentType ContentType;

        public RestRequest(string url, Method method) : this(url, method, ContentType.FormUrlEncoded) { }

        public RestRequest(string url, Method method, ContentType contentType)
        {
            this.parameters = new List<Parameter>();

            this.ContentType = contentType;
            this.url = url;

            this.Header = new Dictionary<string, string>();
            this.Method = method;

            this.RequestContentType = new MediaTypeHeaderValue(this.GetContentTypeString());
        }

        private string GetContentTypeString()
        {
            switch (this.ContentType)
            {
                case ContentType.FormUrlEncoded:
                    return "application/x-www-form-urlencoded";

                case ContentType.Json:
                    return "application/json";
            }

            return null;
        }

        public Uri Url
        {
            get
            {
                var buildUri = new UriBuilder(this.url) { Query = this.BuildQueryString(ParameterType.QueryString) };
                return buildUri.Uri;
            }
        }

        internal string PostParameter => this.BuildQueryString(ParameterType.Body);

        internal object PostJsonParameter
        {
            get
            {
                return this.parameters.SingleOrDefault(p => p.Name == "json").Value;
            }
        }
        internal MediaTypeHeaderValue RequestContentType { get; private set; }

        public IDictionary<string, string> Header { get; }
        public Method Method { get; }

        public string Accept => "application/json";

        /// <summary>
        /// Add Json Parameter to request. Value is serialized to json. 
        /// Throw exception if method isn´t POST or PUT.
        /// </summary>
        /// <param name="value">Parameter value</param>
        public virtual void AddParameter<T>(T value)
        {
            if (this.Method != Method.POST && this.Method != Method.PUT) throw new FormatException("Only POST or PUT method are allowed.");
            this.AddParameter("json", value, ParameterEncodeType.Json, ParameterType.Body);
        }

        /// <summary>
        /// Add Parameter to URL. Value is not encode. If Method is POST, parameter is pass in body, if Method is Get, parameter is pass in URL.
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        public virtual void AddParameter<T>(string name, T value)
        {
            this.AddParameter(name, value, ParameterEncodeType.Plain);
        }


        /// <summary>
        /// Add Parameter to URL. This method encode parameter value by default
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        /// <param name="pType"> </param>
        public virtual void AddParameter<T>(string name, T value, ParameterType pType)
        {
            this.AddParameter(name, value, ParameterEncodeType.Plain, pType);
        }

        /// <summary>
        /// Add Parameter to URL. This method encode parameter value by default
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        /// <param name="encode"> </param>
        public virtual void AddParameter<T>(string name, T value, ParameterEncodeType encode)
        {
            this.AddParameter(name, value, encode, (this.Method == Method.GET) ? ParameterType.QueryString : ParameterType.Body );
        }

        /// <summary>
        /// Add Parameter to URL. 
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        /// <param name="encode">if true, encode parameter value</param>
        /// <param name="pType"></param>
        public virtual void AddParameter<T>(string name, T value, ParameterEncodeType encode, ParameterType pType)
        {
            var strVal = value as string;
            object val = null;

            switch (encode)
            {
                case ParameterEncodeType.Plain:
                    if (strVal == null) throw new FormatException("Parameter must be string type");

                    val = strVal;
                    break;
                case ParameterEncodeType.UrlEncoded:
                    if (strVal == null) throw new FormatException("Parameter must be string type");

                    val = Uri.EscapeDataString(strVal);
                    break;
                case ParameterEncodeType.Json:
                    if (pType != ParameterType.Body) throw new FormatException("Parameter serialized to JSON Content can only be used in Post type");

                    val = value;
                    break;
            }

            if (pType == ParameterType.UrlSegment) this.url = this.url.Replace($"{{{name}}}", strVal);
            else this.parameters.Add(new Parameter { Name = name, Value = val, ParameterType = pType});
        }

        private string BuildQueryString(ParameterType pType)
        {
            var plist = this.parameters.Where(p=> p.ParameterType == pType).Select(p => string.Concat(p.Name, "=", p.Value));
            return plist == null ? null : string.Join("&", plist.ToArray());
        }
    }
}
