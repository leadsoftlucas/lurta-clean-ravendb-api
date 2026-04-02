using System.ComponentModel.DataAnnotations;

namespace Lurta.Clean.Application.Contracts.Usuarios.Validations
{
    public class CreateUserValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DTOUserCreate dto = (DTOUserCreate)validationContext.ObjectInstance;

            if (!dto.Password.Equals(dto.ConfirmPassword))
                return new ValidationResult("As senhas não coincidem.", [nameof(dto.Password)]);

            return ValidationResult.Success;
        }
    }
}
