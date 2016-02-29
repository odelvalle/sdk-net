namespace SDK.Nimblepayments.Exceptions
{
    using SDK.Nimblepayments.Base;

    public sealed class NimbleApiResulException : NimbleApiException
    {
        internal NimbleApiResulException(Result result) : base(result.Info)
        {
            this.HResult = result.Code;
            this.Data.Add("Internal code", result.InternalCode);
        }
    }
}
