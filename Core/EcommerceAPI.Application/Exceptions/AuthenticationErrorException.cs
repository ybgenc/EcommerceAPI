namespace EcommerceAPI.Application.Exceptions
{
    public class AuthenticationErrorException : Exception
    {
        public AuthenticationErrorException() : base("Authentication failed. Please check your credentials and try again.")
        {
        }
    }
}
