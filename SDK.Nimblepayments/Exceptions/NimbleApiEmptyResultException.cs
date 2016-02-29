namespace SDK.Nimblepayments.Exceptions
{
    public class NimbleApiEmptyResultException : NimbleApiException
    {
        public NimbleApiEmptyResultException() : base("Empty result in API response.")
        {
        }
    }
}
