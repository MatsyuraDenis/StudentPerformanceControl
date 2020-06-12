using System.Collections.Generic;
using Entity.Attributes;

namespace Entity.Models.Dtos.PerformanceInfos
{
    public class StudentPerformanceDto
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string StudentName { get; set; }
        public string StudentSecondName { get; set; }
        [Module1Validation]
        public int Module1Result { get; set; }
        [Module2Validation]
        public int Module2Result { get; set; }
        public int Module1Max { get; set; }
        public int Module2Max { get; set; }
        [ExamValidation]
        public int ExamMax { get; set; }
        public int ExamResult { get; set; }
        public int TotalPoints { get; set; }

        public IEnumerable<HomeworkResultDto> Homeworks { get; set; }
        public IList<HomeworkResultDto> EditableHomeworks { get; set; }

        public StudentPerformanceDto()
        {
            Homeworks = new List<HomeworkResultDto>();
            EditableHomeworks = new List<HomeworkResultDto>();
        }
    }
}