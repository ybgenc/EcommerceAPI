namespace EcommerceAPI.Application.Exceptions
{
    public class ExternalLoginErrorException : Exception
    {
        public ExternalLoginErrorException() : base("External login failed. Please try again.")
        {
        }
    }
}
