using System.ComponentModel.DataAnnotations;
using Entity.Models.Dtos.PerformanceInfos;

namespace Entity.Attributes
{
    public class HomeworkPointValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var homework = validationContext.ObjectInstance as HomeworkResultDto;

            if (homework == null || !(value is int?))
            {
                return new ValidationResult("Homework data is null");
            }
            
            if ((int) value < 0)
            {
                return new ValidationResult("Points can't be less then 0!");
            }
            
            if ((int) value > homework.MaxPoints)
            {
                return new ValidationResult("Points counted more than the limit");
            }
            
            return ValidationResult.Success;
        }
    }
}