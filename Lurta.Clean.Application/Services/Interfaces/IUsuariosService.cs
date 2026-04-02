using Lurta.Clean.Application.Contracts.Usuarios;

namespace Lurta.Clean.Application.Services.Interfaces
{
    public interface IUsuariosService
    {
        Task<DTOUserResponse> LoadAsync(string id);
        Task<DTOUserResponse> CreateAsync(DTOUserCreate dtoCreate);
    }
}
