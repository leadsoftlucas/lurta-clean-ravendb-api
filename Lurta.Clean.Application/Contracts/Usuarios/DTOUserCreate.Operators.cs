using Lurta.Clean.Domain.Entities.Usuarios;

namespace Lurta.Clean.Application.Contracts.Usuarios
{
    public partial record DTOUserCreate
    {
        public static implicit operator Usuario(DTOUserCreate dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            if (dto is null)
                throw new NullReferenceException();

            return new(dto.Email, dto.Password, dto.ConfirmPassword);
        }
    }
}
