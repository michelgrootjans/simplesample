using DTO;

namespace Logics
{
    public interface IAuthenticationService
    {
        User Login(string barcode);
    }
}