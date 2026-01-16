using System.ComponentModel.DataAnnotations;

namespace URIS2025_ExamRegistration_Grupa4.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date <= DateTime.Now)
                {
                    return new ValidationResult("The exam date must be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
