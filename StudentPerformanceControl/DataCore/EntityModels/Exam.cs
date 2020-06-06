using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class Exam
    {
        public int ExamId { get; set; }
        public int SubjectId { get; set; }

        public Subject Subject { get; set; }
        public IList<StudentGrade> StudentGrades { get; set; }
    }
}