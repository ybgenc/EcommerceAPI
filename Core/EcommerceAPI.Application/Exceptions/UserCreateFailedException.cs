namespace EcommerceAPI.Application.Exceptions
{
    public class UserCreateFailedException : Exception
    {
        public UserCreateFailedException() : base("User creation failed due to an unexpected error.") { }
        public UserCreateFailedException(string message) : base(message) { }
        public UserCreateFailedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
