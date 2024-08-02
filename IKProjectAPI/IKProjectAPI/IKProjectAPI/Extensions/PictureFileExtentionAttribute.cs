using System.ComponentModel.DataAnnotations;

namespace IKProjectAPI.Extensions
{
    public class PictureFileExtentionAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile file = (IFormFile)value;  // value as IFormFile  tipini çevirdik.

            if (file is not null)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();

                string[] extensions = { ".jpg", ".jpeg", ".png" };


                bool result = extensions.Any(x => x.EndsWith(extension));

                if (!result)
                {
                    return new ValidationResult("Valid format is '.jpg', '.jpeg', '.png'");
                }
            }

            return ValidationResult.Success;
        }
    }
}
