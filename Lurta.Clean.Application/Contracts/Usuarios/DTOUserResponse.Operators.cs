using Lurta.Clean.Domain.Entities.Usuarios;

namespace Lurta.Clean.Application.Contracts.Usuarios
{
    public partial class DTOUserResponse
    {
        public static implicit operator DTOUserResponse(Usuario usuario)
        {
            if (usuario is null)
                return null;

            return new()
            {
                Id = usuario.Id,
                Username = usuario.Username
            };
        }
    }
}
