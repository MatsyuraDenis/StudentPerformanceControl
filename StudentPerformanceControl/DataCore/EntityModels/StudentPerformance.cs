using System.Collections;
using System.Collections.Generic;

namespace DataCore.EntityModels
{
    public class StudentPerformance
    {
        public int StudentPerformanceId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int ExamPoints { get; set; }
        public int Module1TestPoints { get; set; }
        public int Module2TestPoints { get; set; }

        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public IList<HomeworkResult> HomeworkResults { get; set; }
    }
}