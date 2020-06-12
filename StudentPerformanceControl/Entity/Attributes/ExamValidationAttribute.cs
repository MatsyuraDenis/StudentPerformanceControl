using System.ComponentModel.DataAnnotations;
using Entity.Models.Dtos.PerformanceInfos;

namespace Entity.Attributes
{
    public class ExamValidationAttribute : ValidationAttribute
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
            
            if (moduleResult > studentPerformance.ExamMax)
            {
                return new ValidationResult("Exam result more than limit!");
            }
            
            return ValidationResult.Success;
        }
    }
}