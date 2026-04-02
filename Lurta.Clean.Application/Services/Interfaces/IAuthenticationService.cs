using Lurta.Clean.Application.Contracts.Authentications;

namespace Lurta.Clean.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<DTOLoginResponse> LoginAsync(DTOLoginRequest dtoRequest);
    }
}
