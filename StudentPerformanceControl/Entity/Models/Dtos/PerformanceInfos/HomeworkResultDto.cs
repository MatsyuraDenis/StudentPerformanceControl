using System.ComponentModel.DataAnnotations;
using Entity.Attributes;

namespace Entity.Models.Dtos.PerformanceInfos
{
    public class HomeworkResultDto
    {
        public int HomeworkId { get; set; }
        public int HomeworkResultId { get; set; }
        public int HomeworkNumber { get; set; }
        [HomeworkPointValidation]
        public int Points { get; set; }
        public int MaxPoints { get; set; }
    }
}