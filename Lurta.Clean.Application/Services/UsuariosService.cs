using Lurta.Clean.Application.Contracts.Usuarios;
using Lurta.Clean.Application.Services.Interfaces;
using Lurta.Clean.Domain.Entities.Usuarios;
using Raven.Client.Documents;

namespace Lurta.Clean.Application.Services
{
    public class UsuariosService(IDocumentStore ravenDB) : IUsuariosService
    {
        public async Task<DTOUserResponse> LoadAsync(string id)
        {
            using var session = ravenDB.OpenAsyncSession();

            return await session.LoadAsync<Usuario>(id);
        }

        public async Task<DTOUserResponse> CreateAsync(DTOUserCreate dtoCreate)
        {
            using var session = ravenDB.OpenAsyncSession();

            Usuario usuario = new(dtoCreate.Email, dtoCreate.Password, dtoCreate.ConfirmPassword);

            await session.StoreAsync(usuario);
            await session.SaveChangesAsync();

            return usuario;
        }
    }
}
