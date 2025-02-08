namespace EcommerceAPI.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAccesstoken(int minute);
    }
}
