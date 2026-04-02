using Lurta.Clean.Application.Contracts.Usuarios.Validations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Lurta.Clean.Application.Contracts.Usuarios
{
    [Serializable]
    [DataContract]
    [CreateUserValidation]
    public partial record DTOUserCreate
    {
        [DataMember]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [DataMember]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataMember]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
