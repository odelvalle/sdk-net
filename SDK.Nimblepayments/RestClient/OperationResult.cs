namespace SDK.Nimblepayments.RestClient
{
    using System;
    using System.Net;

    public class OperationResult
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public Exception Exception { get; internal set; }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Result { get; internal set; }
        
    }
}
