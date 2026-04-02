using System.ComponentModel.DataAnnotations;

namespace Lurta.Clean.Domain.Entities.Usuarios
{
    public partial class Usuario
    {
        public string Id { get; set; } = null;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; private set; } = string.Empty;

        public bool IsEnabled { get; private set; } = true;

        public DateTimeOffset When { get; private set; } = DateTimeOffset.UtcNow;

        public Usuario()
        {
        }

        public Usuario(string userName, string password, string confirmPassword)
        {
            if (password != confirmPassword)
                throw new ArgumentException("As senhas não coincidem.");

            Username = userName;
            Password = password; // Em um cenário real, a senha deve ser armazenada de forma segura (hash + salt).
        }

        public bool ValidatePassword(string password)
            => Password.Equals(password);

        public Usuario Enable()
        {
            IsEnabled = true;
            return this;
        }

        public Usuario Disable()
        {
            IsEnabled = false;
            return this;
        }
    }
}
