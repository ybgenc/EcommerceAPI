namespace EcommerceAPI.Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("Username or Email not found") { }

    }
}
