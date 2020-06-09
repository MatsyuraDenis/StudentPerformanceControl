using System.Collections.Generic;

namespace Entity.Models.Dtos.StudentPerformance
{
    public class StudentSubjectPerformanceDto
    {
        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public int Module1Points { get; set; }
        public int Module1MaxPoints { get; set; }
        public int Module2Points { get; set; }
        public int Module2MaxPoints { get; set; }
        public int ExamPoints { get; set; }
        public int ExamMaxPoints { get; set; }

        public IEnumerable<StudentHomeworkPerformanceDto> Homeworks { get; set; }
    }
}