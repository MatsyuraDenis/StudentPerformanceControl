using System.ComponentModel.DataAnnotations;
using Entity.Models.Dtos.PerformanceInfos;

namespace Entity.Attributes
{
    public class Module1ValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var studentPerformance = validationContext.ObjectInstance as StudentPerformanceDto;
            var moduleResult = (int)value;
            
            if (studentPerformance == null)
            {
                return new ValidationResult("Empty data");
            }

            if (moduleResult < 0)
            {
                return new ValidationResult("Module result can't be less than 0!");
            }
            
            if (moduleResult > studentPerformance.Module1Max)
            {
                return new ValidationResult("Module 1 result more than limit!");
            }
            
            return ValidationResult.Success;
        }
    }
}