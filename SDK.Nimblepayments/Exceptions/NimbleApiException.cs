namespace SDK.Nimblepayments.Exceptions
{
    using System;

    public class NimbleApiException : Exception
    {
        internal NimbleApiException(string msg) : base(msg)
        {
        }
    }
}
