using System.ComponentModel.DataAnnotations;

namespace Entity.Attributes
{
    public class HomeworkPointsValidationFilterAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }
    }
}