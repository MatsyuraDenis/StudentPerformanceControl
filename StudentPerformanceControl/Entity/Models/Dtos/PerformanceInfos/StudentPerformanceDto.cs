using System.Collections.Generic;

namespace Entity.Models.Dtos.PerformanceInfos
{
    public class StudentPerformanceDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentSecondName { get; set; }
        public int? Module1Result { get; set; }
        public int? Module2Result { get; set; }
        public int? ExamResult { get; set; }
        public IEnumerable<HomeworkResultDto> Homeworks { get; set; }
    }
}