namespace SDK.Nimblepayments.Base
{
    using System.Net;

    using SDK.Nimblepayments.Auth;
    using SDK.Nimblepayments.Exceptions;
    using SDK.Nimblepayments.RestClient;

    public class NimbleBaseOperations: AuthAuthorization
    {
        internal NimbleBaseOperations(ApiContext apiContext) : base(apiContext)
        {
        }

        internal OperationResult<T> EnsureNimbleOperationResult<T>(OperationResult<NimbleApiResult<T>> apiresponse)
        {
            if (apiresponse.Exception != null)
            {
                return new OperationResult<T>
                {
                    Exception = apiresponse.Exception,
                    HttpStatusCode = apiresponse.HttpStatusCode
                };
            }

            if (apiresponse.Result.Operation.Code != 200)
            {
                return new OperationResult<T>
                {
                    Exception = new NimbleApiResulException(apiresponse.Result.Operation),
                    HttpStatusCode = (HttpStatusCode)apiresponse.Result.Operation.Code
                };
            }

            return new OperationResult<T>
            {
                HttpStatusCode = apiresponse.HttpStatusCode,
                Result = apiresponse.Result.Data
            };
        }

        internal OperationResult EnsureNimbleOperationResult(OperationResult<NimbleApiResult<NullContentResult>> apiresponse)
        {
            var result = this.EnsureNimbleOperationResult<NullContentResult>(apiresponse);
            return new OperationResult
            {
                HttpStatusCode = result.HttpStatusCode,
                Exception = result.Exception
            };
        }
    }
}
