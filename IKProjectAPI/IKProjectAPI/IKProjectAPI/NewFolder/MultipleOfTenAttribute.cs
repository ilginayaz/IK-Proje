using System.ComponentModel.DataAnnotations;

namespace IKProjectAPI.NewFolder
{
    public class MultipleOfTenAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int intValue && intValue % 10 == 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Değer 10'un katı olmalıdır.");
        }
    }

}
