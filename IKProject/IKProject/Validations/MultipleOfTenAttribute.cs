using System.ComponentModel.DataAnnotations;

namespace IKProject.Validations
{
    public class MultipleOfTenAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is decimal intValue && intValue % 10 == 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Değer 10'un katı olmalıdır.");
        }
    }
}
